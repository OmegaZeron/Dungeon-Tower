using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowDonutOfTruth : PickupItem {

    public override void Pickup()
    {
        GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().inventory.equippedItems.Add(this);
        //TODO put this in the pool manager
        gameObject.SetActive(false);
    }
}
