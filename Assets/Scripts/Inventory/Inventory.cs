using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Inventory : System.Object {
    public List<Item> equippedItems = new List<Item>();
    public List<IInteractable> equippedInteractables = new List<IInteractable>();
    public uint currency = 1;
}
