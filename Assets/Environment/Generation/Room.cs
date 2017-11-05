using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]		//This stores the size of the room and all related tile objects of the room
public class Room
{
	private Vector2 roomPosition = new Vector2();
	private Vector2 roomSize = new Vector2();

	private List<Exit> roomExits = new List<Exit> ();

	private List<GameObject> segments = new List<GameObject> ();

	public Room()
	{
		roomPosition = new Vector2(0,0);
		size = new Vector2 (1, 1);
		roomExits = new List<Exit> ();
		segments = new List<GameObject> ();
	}

	public Vector2 size
	{
		get { return size; }
		set
		{
			roomSize.x = (int)value.x;
			roomSize.y = (int)value.y;

			roomSize.x = roomSize.x < 1 ? 1 : roomSize.x;
			roomSize.y = roomSize.y < 1 ? 1 : roomSize.y;
		}
	}

	public int h
	{
		get{ return (int)size.y; }
	}

	public int w
	{
		get{ return (int)size.x; }
	}

	public Vector2 position
	{
		get{ return roomPosition; }
		set{ roomPosition = value; }
	}

	public float upperBounds
	{
		get{ return roomPosition.y + size.y - 1;}
	}
	public float lowerBounds
	{
		get{ return roomPosition.y;}
	}
	public float leftBounds
	{
		get{ return roomPosition.x;}
	}
	public float rightBounds
	{
		get{ return roomPosition.x + size.x - 1;}
	}

	public List<Exit> exits
	{
		get { return roomExits; }
		set { roomExits = value; }
	}
		
}
