using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muns : PickupItem {

    [SerializeField] private uint value;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlatformerCharacter2D>();
        }
    }

    public override void Pickup()
    {
        HollaHolla();
    }

    private void HollaHolla()
    {
        GetDolla();
    }

    private void GetDolla()
    {
        //inventory.addmuns(value);
    }
}
