using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ATTENTION!!!	Inventory should probably be stored here, or in a seperate Player Inventory/Manager script

public class GameManager : MonoBehaviour {

	private static GameManager gameManager;

	[SerializeField] private GameObject mainCamera;
	private GameObject mainCameraInstance;

	[SerializeField] private GameObject player;
    private GameObject playerInstance;
    [SerializeField] private Transform spawnPoint;
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
		get{ return playerInstance; }
	}

	public GameObject MainCamera
	{
		get{ return mainCamera; }
	}

	void Awake () 
	{
		gameManager = this;
        controllerState = Platformer2DUserControl.ControllerState.menuControl;
        playerInstance = Instantiate(player, spawnPoint.position, Quaternion.Euler(Vector3.zero));
		mainCameraInstance = Instantiate (mainCamera);
		mainCameraInstance.GetComponent<Camera2DFollow> ().target = playerInstance.transform;
	}

	void Start()
	{
        control = player.GetComponent<Platformer2DUserControl>();
        //control.State = controllerState; // Tandy: do not use this line, it freezes Player controls
    }


    void Update ()
	{
		
	}
}
