using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour
{
	enum State {IDLE, USINGABILITY};

	[SerializeField] private State state;

	public Animator myAnimator;
	public Weapon weaponOne;
	public Weapon weaponTwo;
	private IUsableItem iUseOne;
	private IUsableItem iUseTwo;



	void Start () 
	{
		iUseOne = weaponOne.GetComponent<IUsableItem> ();
		iUseTwo = weaponTwo.GetComponent<IUsableItem> ();

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
			else if (Input.GetMouseButtonDown(0))
			{
				iUseOne.UsingItem ();
			}

			if (Input.GetMouseButtonDown (1))
			{
				iUseTwo.StartUsingItem ();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				iUseTwo.UsingItem ();
			}

		}

	}
}
