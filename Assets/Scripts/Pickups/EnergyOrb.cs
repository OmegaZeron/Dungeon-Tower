using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : PickupItem {

    [SerializeField] private uint healAmount = 5;

    private void Start()
    {
        Debug.Log("Player? " + player);
    }

    public override void Pickup()
    {
        GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().HealPlayer(healAmount);
        Destroy(this.gameObject);
    }
}
