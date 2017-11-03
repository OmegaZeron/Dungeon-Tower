using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ATTENTION!!!	Inventory should probably be stored here, or in a seperate Player Inventory/Manager script

public class GameManager : MonoBehaviour {

	private static GameManager gameManager;

	private GameObject player;

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
	}

	void Start()
	{

	}
	

	void Update ()
	{
		
	}
}
