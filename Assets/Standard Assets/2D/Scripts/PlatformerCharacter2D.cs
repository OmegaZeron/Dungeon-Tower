using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private float wallJumpHeight = .7f;
        [SerializeField] private float wallJumpBackwardForce = -200f;
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private LayerMask whatIsPlatform;
        [SerializeField] private bool canDoubleJump = false;
        [SerializeField] private float doubleJumpHeight = .8f;
        [SerializeField] private bool tryingToFall = false;
        private List<Collider2D> ignoredColliders = new List<Collider2D>();
        Collider2D[] playerColliders = new Collider2D[0];
        Collider2D[] ignoreCircleColliders = new Collider2D[0];
        Collider2D[] ignoreBoxColliders = new Collider2D[0];
        private LayerMask checkLayerMask;
        [SerializeField] private bool isJumping = false;
        private bool colliderIsIgnored = false;


        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        private Transform wallCheck;
        const float wallRadius = .2f;
        [SerializeField] private bool onWall;
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        [SerializeField] private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        // TODO add functionality to check for items (use tools and check if double jump is acquired)

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            wallCheck = transform.Find("WallCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            playerColliders = gameObject.GetComponentsInChildren<Collider2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            onWall = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            // TODO check for circle collider's collision, not overlap sphere

			checkLayerMask = tryingToFall ? m_WhatIsGround.value : m_WhatIsGround + whatIsPlatform;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, checkLayerMask); // tryingToFall ? m_WhatIsGround : m_WhatIsGround + whatIsPlatform
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    if (!isJumping)
                    {
                        m_Grounded = true;
                    }
                    canDoubleJump = true;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            isJumping = false;

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallRadius, m_WhatIsGround + whatIsPlatform);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    onWall = true;
                }
            }

            if (m_Rigidbody2D.velocity.y > 0)
            {
                isJumping = true;
            }
        }
        // TODO finish OnCollisionEnter first
        private void OnCollisionExit2D(Collision2D other)
        {
            if (colliderIsIgnored)
            {
                foreach (Collider2D playerCollider in playerColliders)
                {
                    Physics2D.IgnoreCollision(playerCollider, other.collider, false);
                }
                colliderIsIgnored = false;
            }
        }

        // TODO do actually anything with this
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (ignoreCircleColliders.Length == 0)
            {
                ignoreCircleColliders = Physics2D.OverlapCircleAll(GetComponent<CircleCollider2D>().offset, GetComponent<CircleCollider2D>().radius, whatIsPlatform);
            }
            if (ignoreBoxColliders.Length == 0)
            {
                // 99% sure magnitude won't work
                ignoreBoxColliders = Physics2D.OverlapCircleAll(GetComponent<BoxCollider2D>().offset, GetComponent<BoxCollider2D>().size.magnitude, whatIsPlatform);
            }
        }

        public void Move(float move, bool crouch, bool jump, bool jumpHeld)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            // TODO fix horizontal velocity

            // Wall jump
            if (m_Grounded == false && jump && onWall)
            {
                if (m_FacingRight)
                {
                    m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * wallJumpHeight));
                    m_Rigidbody2D.AddForce(new Vector2(wallJumpBackwardForce, 0f));
                }
                if (!m_FacingRight)
                {
                    m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * wallJumpHeight));
                    m_Rigidbody2D.AddForce(new Vector2(Mathf.Abs(wallJumpBackwardForce), 0f));
                }
            }

            // Double jump
            if (m_Grounded == false && jump && !onWall)
            {
                if (canDoubleJump == true)
                {
                    m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * doubleJumpHeight));
                    canDoubleJump = false;
                }
            }

            // TEST - fall down "jump"
            if (jumpHeld && Input.GetKey(KeyCode.S))
            {
                jump = false;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, whatIsPlatform);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        ignoredColliders.Add(colliders[i]);
                        {
                            foreach (Collider2D playerCollider in playerColliders)
                            {
                                Physics2D.IgnoreCollision(playerCollider, colliders[i]);
                            }
                        }
                        tryingToFall = true;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.S) || !jumpHeld)
            {
                if (tryingToFall)
                {
                    tryingToFall = false;
                    foreach (Collider2D collider in ignoredColliders)
                    {
                        foreach (Collider2D playerCollider in playerColliders)
                        {

                            Physics2D.IgnoreCollision(playerCollider, collider, false);
                        }
                    }
                    ignoredColliders.Clear();
                }
            }



            // normal jump
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
