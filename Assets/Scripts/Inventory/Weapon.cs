using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : UsableItem, IInteractable, IUsableItem
{
	[SerializeField] protected Animator charAnimator;
	[SerializeField] protected Animator myAnimator;

	[SerializeField] protected GameObject weaponObject;
	[SerializeField] protected List<string> animationTriggers = new List<string>();

	[SerializeField] protected List<Vector2> hitBoxes = new List<Vector2> ();

    public string name;
    public int attack;

	public Animator animator
	{
		get{ return charAnimator; }
		set{ charAnimator = value; }
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
	public override void StartUsingItem()
	{
		Debug.Log ("WeaponStartUsing");
	}

	public override void UsingItem()
	{

	}

	public override void StopUsingItem()
	{

	}

	//TODO These Functions should be called from the animator.  They specify when the weapon should check for hits in an attack animation.  (This may have to go through the player)
	//  Possible to have the animator check against the currently used weapon.CheckForHit() 
	public virtual void CheckForHit()
	{

	}

	public virtual void StopCheckForHit()
	{

	}

}
