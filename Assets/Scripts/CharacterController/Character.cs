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

	[SerializeField] protected InteractableCheck interactCheck;

	[SerializeField] protected Transform frontWeapon;
	[SerializeField] protected Transform backWeapon;

	[SerializeField] protected Weapon frontEquippedWeapon;
	[SerializeField] protected Weapon backEquippedWeapon;

    //public void Move(float move, float verticalAxis, bool crouch, bool jump, bool jumpHeld)
    //{

    //}

    protected void Awake()
    {
        if (playerColliders.Count < 1)
        {
            playerColliders.Add(GetComponent<Collider2D>());
        }

		if (interactCheck == null)
		{
			interactCheck = GetComponentInChildren<InteractableCheck> ();
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

	public void Interact()
	{
		if (interactCheck == null)
		{
			Debug.LogError ("Interact is being called by " + transform.root.name + ", but does not have an InteractableCheck assigned");
			return;
		}	//Debug.LogWarning ("Character.Interact() ");

		if(interactCheck.closest_interactable != null)
			interactCheck.closestInteractable.StartInteracting(this); 
	}
}
