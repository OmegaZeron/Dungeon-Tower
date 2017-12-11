using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCharacter2D : CombatCharacter//, IDamageable // Tandy: IDamageable realization moved to CombatCharacter.cs
{
    private enum PlayerState { idle, jumping, attacking, etc };

    [SerializeField] private float wallJumpHeight = .7f;
    [SerializeField] private float wallJumpVelocity = -20f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private float doubleJumpHeight = .8f;
    const float wallRadius = .2f;
    const float k_GroundedRadius = .15f;                                // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .01f;                                 // Radius of the overlap circle to determine if the player can stand up
    private Vector3 onWallPos = Vector3.zero;

    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private LayerMask whatIsWall;
    private LayerMask checkLayerMask;
    private Transform m_GroundCheck;                                    // A position marking where to check if the player is grounded.
    private Transform wallCheck;
    private Transform m_CeilingCheck;                                   // A position marking where to check for ceilings

    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool tryingToFall = false;
    [SerializeField] private bool wallJumping = false;
    [SerializeField] private bool isJumping = false;
    private bool colliderIsIgnored = false;
    [SerializeField] private bool unsteady;                              
    [SerializeField] private bool onWall;
    [SerializeField] private bool m_Grounded;                           // Whether or not the player is grounded.

    [SerializeField] private List<Collider2D> ignoredColliders = new List<Collider2D>();

    private Transform bodyArmor;
    //public Inventory inventory = new Inventory();

    [SerializeField] private ParticleSystem doubleJumpParticles;
                                        // TODO add functionality to check for items (use tools and check if double jump is acquired

    private new void Awake()
    {
        base.Awake();
        // Setting up references.
        m_MaxSpeed = 10f;       // The fastest the player can travel in the x axis.
        //m_JumpForce = 600f;     // Amount of force added when the player jumps.
        m_FacingRight = true;
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        wallCheck = transform.Find("WallCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        health = 1000;
        maxHealth = 1000;
        if (frontWeapon == null)
        {
			frontWeapon = transform.Find ("Front weapon");

			if (frontWeapon == null)
				Debug.LogError ("FrontWeapon is null.  Assign it in the Inspector");
        }
        if (backWeapon == null)
        {
            backWeapon = transform.Find("Back weapon");

			if (backWeapon == null)
				Debug.LogError ("BackWeapon is null.  Assign it in the Inspector");
        }
        if (bodyArmor == null)
        {
            bodyArmor = transform.Find("Body");
        }
    }


    private void FixedUpdate()
    {
        m_Grounded = false;
        onWall = false;
        isJumping = false;
        wallJumping = false;
        unsteady = false;

        if (m_Rigidbody2D.velocity.y > 0)
        {
            isJumping = true;
        }

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        checkLayerMask = tryingToFall ? m_WhatIsGround.value : m_WhatIsGround.value + whatIsPlatform.value;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, checkLayerMask); // tryingToFall ? m_WhatIsGround : m_WhatIsGround + whatIsPlatform
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && !ignoredColliders.Contains(colliders[i]))
            {
                if (!isJumping)
                {
                    m_Grounded = true;

                }
                if (m_Rigidbody2D.velocity.y == 0)
                {
                    canDoubleJump = true;
                }
            }
        }
        m_Anim.SetBool("Ground", m_Grounded);



        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        if (m_Anim.GetFloat("vSpeed") == 0 && !m_Grounded && !tryingToFall)
        {
            unsteady = true;
        }
        m_Anim.SetBool("Unsteady", unsteady);

        colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallRadius, whatIsWall);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                onWallPos = colliders[i].gameObject.transform.position;
                onWall = true;
            }
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PickupItem item = collision.GetComponent<PickupItem>();
        if (item != null)
        {
            item.Pickup();
        }
    }

    public void HealPlayer(uint healAmount)
    {
        if (healAmount >= maxHealth - health)
        {
            health = maxHealth;
        }
        else
        {
            health += (int)healAmount;
        }
    }

    public void AddMuns(uint munAmount)
    {
        inventory.currency += munAmount;
    }

    public void Move(float move, float verticalAxis, bool crouch, bool jump, bool jumpHeld)
    {
        // If crouching, check to see if the character can stand up
        //if (!crouch && m_Anim.GetBool("Crouch"))
        //{
        //    // If the character has a ceiling preventing them from standing up, keep them crouching
        //    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
        //    {
        //        crouch = true;
        //    }
        //}

        // Set whether or not the character is crouching in the animator
        //m_Anim.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            //move = (crouch ? move*m_CrouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

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

        // Wall jump
        if (m_Grounded == false && jump && onWall)
        {
            wallJumping = true;
            onWallPos.y = transform.position.y;
            Vector3 wallJumpVector = transform.position - onWallPos;
            wallJumpVector = wallJumpVector.normalized;
            wallJumpVector.y = 1.4f;
            m_Rigidbody2D.velocity = wallJumpVector.normalized * wallJumpVelocity;
            Flip();
            StartCoroutine("WallJumpControl");
        }

        // Double jump
        if (!m_Grounded && !unsteady && jump && !onWall && canDoubleJump && !wallJumping)
        {
            m_Rigidbody2D.velocity = new Vector2(0f, .01f);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * doubleJumpHeight));
            canDoubleJump = false;
            doubleJumpParticles.Play();
        }

        // TEST - fall down "jump"
        if (jumpHeld && verticalAxis < -0.1)
        {
            jump = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, whatIsPlatform);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject && !ignoredColliders.Contains(colliders[i]))
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
        else if ((verticalAxis >= 0 || !jumpHeld) && tryingToFall)
        {
            tryingToFall = false;
        }

        if ((!tryingToFall || !isJumping) && ignoredColliders.Count != 0)
            UnignoreColliders();

        // normal jump
        if ((m_Grounded || unsteady) && jump && (m_Anim.GetBool("Ground") || m_Anim.GetBool("Unsteady")))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public new void TakeDamage(uint damageTaken = 0, float knockback = 0)
    {
        if (health <= damageTaken)
        {
            health = 0;
            Die();
        }
        else
        {
            health -= (int)damageTaken;
            TakeKnockback(knockback);
            Debug.Log("Knockback called");
            if (knockback > 0)
            {
                StartCoroutine("WallJumpControl");
            }
        }
    }

    IEnumerator WallJumpControl()
    {
        m_AirControl = false;
        yield return new WaitForSeconds(.3f);
        m_AirControl = true;
    }

    private void UnignoreColliders()
    {
        List<Collider2D> overlappedColliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsPlatform);

        // Get all Platform colliders that are overlapping the player Colliders
        foreach (Collider2D playerCollider in playerColliders)
        {
            Collider2D[] cArray = new Collider2D[10];

            playerCollider.OverlapCollider(filter, cArray);
            overlappedColliders.AddRange(cArray);
        }

        //remove player Colliders in overlappedColliders	//Currently loops through  a number of times equal to the size of playerColliuders, as it is possibl each one overlapped a single
        for (int i = 1; i <= playerColliders.Count; i++)
        {
            foreach (Collider2D playerCollider in playerColliders)
            {
                if (overlappedColliders.Contains(playerCollider))
                    overlappedColliders.Remove(playerCollider);
            }
        }

        //Are we still touching any ignored colliders, if not , remove and unignore those colliders
        //checks for any ignored colliders in the overlappColliders, if they are not there, remove them from ignored Colliders and unignore their collison.
        for (int i = ignoredColliders.Count - 1; i >= 0; i--)
        {
            if (!overlappedColliders.Contains(ignoredColliders[i]))
            {
                foreach (Collider2D playerCollider in playerColliders)
                {
                    Physics2D.IgnoreCollision(playerCollider, ignoredColliders[i], false);
                }
                ignoredColliders.RemoveAt(i);
            }
        }
    }

    //private void Flip()
    //{
    //    // Switch the way the player is labelled as facing.
    //    m_FacingRight = !m_FacingRight;

    //    // Multiply the player's x local scale by -1.
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}

    /* // Tandy: Greg and I talked about moving TakeDamage() and Die() into the CombatCharacter.cs base class,
     * // so that Enemy can inherit them
        public void TakeDamage(int damageTaken = 0, float knockback = 0)
        {
            if (health <= damageTaken)
            {
                health = 0;
                Die();
            }
            else
            {
                health -= damageTaken;
            }
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    */
}
