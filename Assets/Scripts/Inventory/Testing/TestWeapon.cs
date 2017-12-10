using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{

	//TODO may need to gate via states as to what functions are able to run.
	public override void StartUsingItem()
	{
		if(charAnimator != null)
			charAnimator.SetTrigger("Action 1");
		if(weaponAnimator != null)
			weaponAnimator.SetTrigger ("Anim 1");
	}

	public override void StopUsingItem()
	{
		if(charAnimator != null)
			charAnimator.SetTrigger("Action 3");
		if(weaponAnimator != null)
			weaponAnimator.SetTrigger ("Anim 3");
	}


}
