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

}
