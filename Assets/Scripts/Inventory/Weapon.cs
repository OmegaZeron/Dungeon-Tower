using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IUsableItem
{
	[SerializeField] protected Animator charAnimator;
	[SerializeField] protected Animator myAnimator;

	[SerializeField] protected GameObject weaponObject;
	[SerializeField] protected List<string> animationTriggers = new List<string>();

	public Animator animator
	{
		get{ return charAnimator; }
		set{ charAnimator = value; }
	}

	protected void Start () 
	{
		myAnimator = gameObject.GetComponent<Animator> ();
	}

	public void Equip()
	{
		//places the object on the equip or attachment point of the character calling it (If it should be attached)
		//TODO create an Equip/attachment Point class that is a set of different transforms. have it stored in the Character, and pass it to Equip.
		//  the Item will have an EquipPoint.EquipTo.enum that will specify where it should be attached to when Equip is run.
	}

	public void EquipTo(Transform attachmentPoint)
	{
		//this will attach the Weapon.gameObject to the specified Transform.
	}

	//Need to have a way call the animation and set the speed.  probably should do it in this script and just have the character give it it's AnimationController

	public void StartUsingItem()
	{

	}

	public void UsingItem()
	{

	}

	public void StopUsingItem()
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
