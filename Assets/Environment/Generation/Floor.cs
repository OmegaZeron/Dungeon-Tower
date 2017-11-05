using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 	//this stores the size of the floor, and rooms that are inside of it.
public class Floor 
{
	[SerializeField] private int floorNumber = 0;
	[SerializeField] private Vector2 size = new Vector2 ();

	[SerializeField] private List<Exit> _entryways = new List<Exit>();

	[SerializeField] private List<Room> _rooms = new List<Room> ();

	public float h
	{
		get{ return (int)size.y; }
		set{ size.y = value; }
	}

	public float w
	{
		get{ return (int)size.x; }
		set{ size.x = value; }
	}

	public List<Exit> entrances
	{
		get { return _entryways; }
		set { _entryways = value; } 
	}
	
	public List<Room> rooms
	{
		get { return _rooms; }
		set { _rooms = value; } 
	}
}
