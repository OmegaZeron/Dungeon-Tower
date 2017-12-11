using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowDonutOfTruth : PickupItem {

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlatformerCharacter2D>();
        }
    }

    public override void Pickup()
    {
        
    }
}
