using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager gameManager;

	public static GameManager instance
	{
		get
		{ 
			if (gameManager == null)
				gameManager = new GameManager ();
			
			return gameManager;
		}
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
