using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]		//This stores the size of the room and all related tile objects of the room
public class Room
{
	private Vector2 size = new Vector2();

	private List<Exit> exits = new List<Exit> ();

	private List<GameObject> segments = new List<GameObject> ();

	public int h
	{
		get{ return (int)size.y; }
		set{ size.y = value; }
	}

	public int w
	{
		get{ return (int)size.x; }
		set{ size.x = value; }
	}
		

}
