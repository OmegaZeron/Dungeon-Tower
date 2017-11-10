using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
	
	new public void StartUsingItem()
	{
		//myAnimator turn blue
		myAnimator.SetTrigger("Action 1");

	}

	new public void UsingItem()
	{
		//myAnimator turn Yellow
		myAnimator.SetTrigger("Action 2");

	}

	new public void StopUsingItem()
	{
		//myAnimator turn Red
		myAnimator.SetTrigger("Action 3");

	}

}
