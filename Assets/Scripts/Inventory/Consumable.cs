using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : UsableItem, IInteractable, IUsableItem 
{
    public string name;
    public int amount;  // attack, defense, health points -- any stat or effect's "amount"
    // TODO: add variables to resolve / determine effects of Consumable

    //===== IInteractable functions =====//
    public void StartInteracting() {

    }

    public void Interacting() {

    }

    public void StopInteracting() {

    }

    //===== IUsableItem functions =====//
    public void StartUsingItem() {

    }

    public void UsingItem() {

    }

    public void StopUsingItem() {

    }
}
	