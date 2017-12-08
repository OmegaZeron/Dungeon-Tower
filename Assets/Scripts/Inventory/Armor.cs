using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IInteractable, IUsableItem 
{
    public int defense;

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
