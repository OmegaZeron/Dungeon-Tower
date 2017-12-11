using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : PickupItem {

    [SerializeField] private uint healAmount = 5;

    public override void Pickup()
    {
        GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().HealPlayer(healAmount);
        Destroy(this.gameObject);
    }
}
