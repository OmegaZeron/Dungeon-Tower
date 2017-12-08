using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected float m_MaxSpeed;
    [SerializeField] protected float m_JumpForce;
    protected int health;
    protected bool m_FacingRight;
    [SerializeField] protected List<Collider2D> playerColliders = new List<Collider2D>();
    protected Animator m_Anim;

    protected Transform frontWeapon;
    protected Transform backWeapon;

    protected Weapon frontEquippedWeapon;
    protected Weapon backWeaponEquipped;

    //public void Move(float move, float verticalAxis, bool crouch, bool jump, bool jumpHeld)
    //{

    //}

    protected void Awake()
    {
        if (playerColliders.Count < 1)
        {
            playerColliders.Add(GetComponent<Collider2D>());
        }
    }

    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetFrontWeapon(Weapon equip)
    {
        frontEquippedWeapon = equip;
        frontEquippedWeapon.Equip(frontWeapon, backWeapon, m_Anim);
    }

    //pickupWeapon set curretn weapon to this, and drop old weapon.
    //Then run Equip on picked up Weapon
}
