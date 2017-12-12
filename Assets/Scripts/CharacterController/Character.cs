using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Tandy: need this for Die() NotImplementedException()

public abstract class Character : MonoBehaviour, IDamageable { // Tandy: CombatCharacter now realizes IDamageable,
                                                               // which both Enemy and PlatformerCharacter2D can inherit
    [SerializeField] protected Transform character;
    [SerializeField] protected Rigidbody2D m_Rigidbody2D; // Tandy: moved up to base class for Enemy and Player

    protected float m_MaxSpeed;
    [SerializeField] protected float m_JumpForce;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    protected bool m_FacingRight;
    [SerializeField] protected List<Collider2D> playerColliders = new List<Collider2D>();
    protected Animator m_Anim;

    [SerializeField] protected InteractableCheck interactCheck;
    [SerializeField] public Inventory inventory = new Inventory();
    [SerializeField] private PoolManager poolManager;

    public virtual void Move(float move, float verticalAxis, bool jump)
    {

    }

    protected void Awake()
    {
        if (playerColliders.Count < 1)
        {
            playerColliders.AddRange(GetComponents<Collider2D>());
        }

		if (interactCheck == null)
		{
			interactCheck = GetComponentInChildren<InteractableCheck> ();
		}
        poolManager = FindObjectOfType<PoolManager>();

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

    // Tandy: TakeDamage moved from Platformer2DUserControl
    public void TakeDamage(uint damageTaken = 0, float knockback = 0) {
        if (health <= damageTaken) {
            health = 0;
            Die();
        }
        else {
            health -= (int)damageTaken;
            TakeKnockback(knockback);
            Debug.Log("Knockback called");
        }
    }

    // Tandy: Die moved from Platformer2DUserControl
    public void Die() 
	{
		if(m_Rigidbody2D != null)
			m_Rigidbody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
        for(int i = 0; i < inventory.currency; i++)
        {
			if (poolManager != null) 
			{
				GameObject drop = poolManager.GetObject(poolManager.muns);
				drop.transform.position = transform.position;
			}

        }
        foreach(Item item in inventory.equippedItems) {
            inventory.equippedItems.Remove(item);
            item.transform.position = transform.position;
        }
        //throw new NotImplementedException(); // Tandy: "using System;" makes this work
    }

    public void TakeKnockback(float knockback) 
	{
        if(m_Rigidbody2D != null) {
            Debug.Log("TakeKnockback: " + knockback);
            if (m_FacingRight) {
                m_Rigidbody2D.AddForce(Vector2.left * knockback + Vector2.up * (knockback / 2.1f));
            }
            else {
                m_Rigidbody2D.AddForce(Vector2.right * knockback + Vector2.up * (knockback / 2.1f));
            }
        }
        else {
            Debug.LogWarning(name + "does not have a Rigidbody2D for Knockback!");
        }
    }
    //// Tandy: for debugging Knockback only while taking damage
    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    //Debug.Log("Trigger!");
    //    //if(collider.gameObject.GetType() == typeof(Weapon)) {
    //    if (collider.gameObject.GetComponent<Weapon>())
    //    {
    //        Weapon weapon = collider.gameObject.GetComponent<Weapon>();
    //        Debug.Log(name + ": felt a Weapon trigger!");
    //        TakeDamage(weapon.damage, weapon.knockBack);
    //    }
    //}

}
