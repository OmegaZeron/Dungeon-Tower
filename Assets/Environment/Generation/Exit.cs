using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit
{
	public Vector2 location;

	public enum Oriented {DOWN, UP, LEFT, RIGHT};
	public Oriented orientation;
}
