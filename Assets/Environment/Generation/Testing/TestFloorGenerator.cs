using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFloorGenerator : MonoBehaviour 
{
	[SerializeField] private GameObject roomSegment;
	[SerializeField] private float roomSizeUnitK = 3.0f;

	public Vector2 maxRoomSize = new Vector2 (2, 2);
	[SerializeField] private Floor floor;

	void Start () 
	{
		ContstrainRoomSize ();
		BuildRooms ();
	}

	void ContstrainRoomSize()
	{
		if (maxRoomSize.x > floor.w)
			maxRoomSize.x = floor.w;
		if (maxRoomSize.y > floor.h)
			maxRoomSize.y = floor.h;
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

		Room oldRoom = new Room();
		Room newRoom = new Room();

		oldRoom.position = new Vector2 (0, 1);;

		float upperHeightLimit = 1;
		float lowerHeightLimit = 1;

	// HORIZONTAL ROOM DISTRIBUTION ONLY
		while (!floorComplete) 
		{
			newRoom.size.x = Random.Range (1, maxRoomSize.x + 1);
			newRoom.size.y = Random.Range (1, maxRoomSize.y + 1);

			//limit the x by the width of the floor
			//TODO check later if this room is at the end of the floor (needing to connect to that entrance) and 
			if (floor.w < oldRoom.rightBounds + newRoom.size.x) 
			{
				newRoom.size.x = (floor.w - oldRoom.rightBounds);
			}

			//set room position

			//set Random Range limits newRoom.position
			upperHeightLimit = oldRoom.position.y + (oldRoom.size.y - 1);
			lowerHeightLimit = oldRoom.position.y - (newRoom.size.y - 1);

			//limit Ranges by floor dimension
			if (upperHeightLimit > floor.h)
				newRoom.position.y -= upperHeightLimit - floor.h;
			if (lowerHeightLimit < 1)
				newRoom.position.y = 1;





			//find the range of points that the room is still attached to the previous room (startLocatoin)

			//if the room is attached to the opposite edge of the floor, it will need to have it's height set to AT LEAST the distance between the startLocation and the entrance
			//set the room height to one that is within the startLocation, but not above or below the floor Height

			//set exits for the newRoom (which is the same as the startLocation + another random exit on the other side of the room


			oldRoom.position.x += newRoom.position.x + 1;
			//oldRoom.position.y = newExit.y

		}
	}
}
