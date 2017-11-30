using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ATTENTION!!!	Inventory should probably be stored here, or in a seperate Player Inventory/Manager script

public class GameManager : MonoBehaviour {

	private static GameManager gameManager;

	[SerializeField] private GameObject player;
    Platformer2DUserControl control;
    Platformer2DUserControl.ControllerState controllerState;

	public static GameManager instance
	{
		get
		{ 
			if (gameManager == null)
				gameManager = new GameManager ();
			
			return gameManager;
		}
	}

	public GameObject Player
	{
		get{ return player; }
	}

	void Awake () 
	{
		gameManager = this;
        controllerState = Platformer2DUserControl.ControllerState.menuControl;
	}

	void Start()
	{
        control.State = controllerState;
	}
	

	void Update ()
	{
		
	}
}
