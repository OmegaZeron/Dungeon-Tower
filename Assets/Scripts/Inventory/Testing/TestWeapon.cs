using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
	//TODO may need to gate via states as to what functions are able to run.
	public override void StartUsingItem()
	{
		charAnimator.SetTrigger("Action 1");
	}

	public override void UsingItem()
	{
		charAnimator.SetTrigger("Action 2");

	}

	public override void StopUsingItem()
	{
		charAnimator.SetTrigger("Action 3");
	}

	public override void CheckForHit()
	{
		Vector2 hitBox = hitBoxes [0];
		Collider2D[] hitObjects = Physics2D.OverlapBoxAll (new Vector2(this.transform.position.x, this.transform.position.y), hitBox, 0f);
	/*Left*/	Debug.DrawLine(new Vector3(hitBox.x - hitBox.y/2, hitBox.x - hitBox.y/2, 0), new Vector3(hitBox.x - hitBox.y/2, hitBox.x + hitBox.y/2, 0), Color.yellow);
	/*Right*/	Debug.DrawLine(new Vector3(hitBox.x + hitBox.y/2, hitBox.x + hitBox.y/2, 0), new Vector3(hitBox.x + hitBox.y/2, hitBox.x - hitBox.y/2, 0), Color.yellow);
	/*Up*/		Debug.DrawLine(new Vector3(hitBox.x - hitBox.y/2, hitBox.x + hitBox.y/2, 0), new Vector3(hitBox.x + hitBox.y/2, hitBox.x + hitBox.y/2, 0), Color.yellow);
	/*Down*/	Debug.DrawLine(new Vector3(hitBox.x - hitBox.y/2, hitBox.x - hitBox.y/2, 0), new Vector3(hitBox.x + hitBox.y/2, hitBox.x - hitBox.y/2, 0), Color.yellow);
	}

	public void OnDrawGizmos()
	{
		
	}
}
