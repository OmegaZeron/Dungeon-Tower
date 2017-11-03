using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 	//this stores the size of the floor, and rooms that are inside of it.
public class Floor 
{
	[SerializeField] private Vector2 size = new Vector2 ();

	[SerializeField] private List<Exit> entryways = new List<Exit>();

	[SerializeField] private List<Room> rooms = new List<Room> ();

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
