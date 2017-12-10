using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour
{
	enum State {IDLE, USINGABILITY};

	[SerializeField] private State state;

	public Animator myAnimator;
	public Transform frontWeapon;
	public Transform backWeapon;
	public Weapon weaponOne;
	public Weapon weaponTwo;
	private IUsableItem iUseOne;
	private IUsableItem iUseTwo;



	void Start () 
	{
		myAnimator = GetComponent<Animator> ();

		iUseOne = weaponOne.GetComponent<IUsableItem> ();
		iUseTwo = weaponTwo.GetComponent<IUsableItem> ();

		EquipWeapon (weaponOne);
		EquipWeapon (weaponTwo);

	}
	
	void Update ()
	{
		if (state == State.IDLE) 
		{
			
			if (Input.GetMouseButtonDown (0)) 
			{
				iUseOne.StartUsingItem ();
				state = State.USINGABILITY;
			}

			if (Input.GetMouseButtonDown (1))
			{
				iUseTwo.StartUsingItem ();
				state = State.USINGABILITY;
			}

		}

		if (state == State.USINGABILITY)
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				iUseOne.StartUsingItem ();
			} 
			else if (Input.GetMouseButtonUp (0)) 
			{
				iUseOne.StopUsingItem ();
			}



			if (Input.GetMouseButtonDown (1))
			{
				iUseTwo.StartUsingItem ();
			}
			else if (Input.GetMouseButtonUp (1)) 
			{
				iUseTwo.StopUsingItem ();
			}

		}

	}

	void EquipWeapon(Weapon weapon)
	{
		weapon.Equip(frontWeapon, backWeapon, myAnimator);
	}
}
