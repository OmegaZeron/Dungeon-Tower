using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorGenerator : MonoBehaviour 
{
	[SerializeField] private GameObject roomSegment;
	[SerializeField] private float roomUnitK = 3.0f;

	public Vector2 maxRoomSize = new Vector2 (2, 2);
	[SerializeField] private Floor floor;



	void Start () 
	{
		BuildRooms ();
	}
	
	void BuildRooms ()
	{	//TODO currently only goes from one end of the floor to the other, no accounting for vertical room building

		// establish starting point for next room (this is the exit that it is attached to)

		// establish room dimension
			//room size should not be larger than the floor bounds OR the area that is left in the floor

		// place room at a height that does not put it higher than the entry/exit it is attached to, OR outside the area of the floor
		// create the rooms exit points
			//if the room needs to attach to the other entry points, ensure that it's size is big enough to reach it.

		// ^^ repeat these steps to create next room

		bool floorComplete = false;
		Vector2 startLocation = floor.entrances [0].location;

		while (!floorComplete) 
		{


			Vector2 newRoomSize = new Vector2();
			newRoomSize.x = Random.Range (1, maxRoomSize.x + 1);
			newRoomSize.y = Random.Range (1, maxRoomSize.y + 1);

			//limit the x by the width of the floor
			if (floor.w < startLocation.x + newRoomSize.x -1) 
			{
				newRoomSize.x = 1 + (floor.w - startLocation.x);
			}

			//Limit the y result within the height of the floor
			if (floor.h < newRoomSize.y) 
			{
				newRoomSize.y = floor.h;
			}

			//if the room is attached to the opposite edge of the floor, it will need to have it's height set to AT LEAST the distance between the startLocation and the entrance
			//set the room height to one that is within the startLocation, but not above or below the floor Height

			//set exits for the newRoom (which is the same as the startLocation + another random exit on the other side of the room



		}
	}
}
