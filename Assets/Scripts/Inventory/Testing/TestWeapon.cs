using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
	public bool checkForHit = false;

	//TODO may need to gate via states as to what functions are able to run.
	public override void StartUsingItem()
	{
		charAnimator.SetTrigger("Action 1");
		myAnimator.SetTrigger ("Anim 1");
	}

	public override void UsingItem()
	{
		charAnimator.SetTrigger("Action 2");
		myAnimator.SetTrigger ("Anim 2");

	}

	public override void StopUsingItem()
	{
		charAnimator.SetTrigger("Action 3");
		myAnimator.SetTrigger ("Anim 3");
	}

	public override void StartHit()
	{
		checkForHit = true;
		StartCoroutine ("CheckingForHit");
	}

	IEnumerator CheckingForHit()
	{
		while (checkForHit)
		{
			Collider2D[] hitObjects = Physics2D.OverlapBoxAll (hitBoxes[0].center, hitBoxes[0].size, hitBoxes[0].angle, enemyLayer.value);
			//TODO
			//HitObjects.Add( if not in hitObjects already)
			//hitObject.TakeDamage
			//
			yield return null;
		}

		//hitObjects.Clear ();
	}

	public override void StopHit()
	{
		checkForHit = false;
	}

	public void OnDrawGizmos()
	{
		if (checkForHit)
			Gizmos.DrawWireCube (hitBoxes [0].center, hitBoxes [0].size);
		//Gizmos.DrawWireCube();
	}
}
