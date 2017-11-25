using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IUsableItem, IInteractable
{
	[SerializeField] protected GameObject weaponObject;
	public int attacks;

	[SerializeField] protected Animator charAnimator;
	[SerializeField] protected Animator myAnimator;
	[SerializeField] protected float animationSpeed = 100.0f;

	[SerializeField] protected List<string> animationTriggers = new List<string>();

	[SerializeField] protected LayerMask enemyLayer;
	[SerializeField] protected List<HitBox> hitBoxes = new List<HitBox> ();
	[SerializeField] protected int currentHitBox = 0;
	[SerializeField] private bool checkForHit = false;
	[SerializeField] private List<Collider2D> hitColliders = new List<Collider2D> ();



	public Animator animator
	{
		get{ return charAnimator; }
		set{ charAnimator = value; }
	}

	[System.Serializable] public class HitBox
	{
		//[SerializeField] protected Shape shape Shape.Shape;

		[SerializeField] public Vector2 center = Vector2.zero;
		[SerializeField] public Vector2 size = new Vector2(1,1);
		[SerializeField] public float angle = 0.0f;
	}

	protected void Start () 
	{
		myAnimator = gameObject.GetComponent<Animator> ();
	}

	public void Equip(Animator equipAnimator = null, Transform attachmentPoint = null)
	{
		//TODO create an Equip/attachment Point class that is a set of different transforms. have it stored in the Character, and pass it to Equip.
		//  the Item will have an EquipPoint.EquipTo.enum that will specify where it should be attached to when Equip is run.

		//assign Animator
		charAnimator = equipAnimator;

		// Equip the weapon to the attachPoint
		if (attachmentPoint != null)
		{
			transform.position = attachmentPoint.position;
			transform.SetParent (attachmentPoint);
		}

	}

    //Need to have a way call the animation and set the speed.  probably should do it in this script and just have the character give it it's AnimationController

    //===== IInteractable functions =====//
    public void StartInteracting() {

    }

    public void Interacting() {

    }

    public void StopInteracting() {

    }

    //===== IUsableItem functions =====//
	public virtual void StartUsingItem()
	{
		Debug.Log ("WeaponStartUsing");
	}

	public virtual void UsingItem()
	{

	}

	public virtual void StopUsingItem()
	{

	}

	//TODO These Functions should be called from the animator.  They specify when the weapon should check for hits in an attack animation.  (This may have to go through the player)
	//  Possible to have the animator check against the currently used weapon.CheckForHit() 
	public virtual void StartHit()
	{
		checkForHit = true;
		StartCoroutine ("CheckingForHit", false);

	}

	public virtual void CheckHitOnce()
	{
		checkForHit = true;
		StartCoroutine ("CheckingForHit", true);
	}

	IEnumerator CheckingForHit(bool checkOnce = false)
	{
		while (checkForHit)
		{
			Collider2D[] hitObjects = Physics2D.OverlapBoxAll (hitBoxes[currentHitBox].center, hitBoxes[currentHitBox].size, hitBoxes[currentHitBox].angle, enemyLayer.value);

			foreach ( Collider2D hitObject in hitObjects )
			{
				if ( !hitColliders.Contains (hitObject) ) 
				{
					//TODO Character cs = GetComponent<CharacterScript>();
					//if( cs != null)
					//	cs.TakeDamage();

					hitColliders.Add(hitObject);
				}

			}

			if (checkOnce)
				checkForHit = false;

			yield return null;
		}

		hitColliders.Clear ();
	}

	public virtual void StopHit()
	{
		checkForHit = false;
	}


	public void OnDrawGizmos()
	{
		if (checkForHit)
		{	//points to the hit colliders
			Gizmos.color = Color.red;
			foreach (Collider2D hitCollider in hitColliders) 
			{
				Gizmos.DrawWireSphere (hitCollider.transform.position, 0.2f);
				Gizmos.DrawLine (this.transform.position, hitCollider.transform.position);
			}

			Gizmos.color = Color.yellow;
		}
		else
			Gizmos.color = Color.white;
		//Draws the hitBox for the attack
		Gizmos.DrawWireCube (hitBoxes [currentHitBox].center, hitBoxes [currentHitBox].size);
	}
}
