using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item, IInteractable, IUsableItem 
{
    public int amount;  // attack, defense, health points -- any stat or effect's "amount"
    // TODO: add variables to resolve / determine effects of Consumable

    //===== IInteractable functions =====//
    public void StartInteracting(Character interactingCharacter = null) {

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
	