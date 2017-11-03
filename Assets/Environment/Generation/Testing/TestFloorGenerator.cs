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

			//TODO Limit the y result within the height of the floor (should establish the exit for the room first


		}
	}
}
