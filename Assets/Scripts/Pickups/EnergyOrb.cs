using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : PickupItem {

    static PlatformerCharacter2D player;

    [SerializeField] private uint healAmount = 5;

    public void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlatformerCharacter2D>();
        }
    }

    public override void Pickup()
    {
        player.HealPlayer(healAmount);
        Destroy(this.gameObject);
    }
}
