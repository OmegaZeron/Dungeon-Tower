using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muns : PickupItem {

    [SerializeField] private uint value = 1;
    [SerializeField] private Rigidbody2D rb;

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
        GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().AddMuns(value);
        Destroy(this.gameObject);
    }
}
