using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Item
{
    protected static PlatformerCharacter2D player;

    protected void Awake() 
	{
        if (player == null)
        {
            player = FindObjectOfType<PlatformerCharacter2D>();
        }
    }

    public virtual void Pickup()
    {

    }

}
