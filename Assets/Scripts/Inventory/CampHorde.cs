using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampHorde : MonoBehaviour {
    // Active Pools - Weapons, Armor, and Consumables currently equipped on Player
    List<Weapon> activeWeapons = new List<Weapon>();
    List<Armor> activeArmor = new List<Armor>();
    List<Consumable> activeConsumables = new List<Consumable>();

    // Inactive Pools - Weapons, Armor, and Consumables currently at Camp
    public List<Weapon> inactiveWeapons = new List<Weapon>();
    public List<Armor> inactiveArmor = new List<Armor>();
    public List<Consumable> inactiveConsumables = new List<Consumable>();

    // Unacquired Pools - Weapons, Armor, and Consumables not yet acquired by the Player
    List<Weapon> unacquiredWeapons = new List<Weapon>();
    List<Armor> unacquiredArmor = new List<Armor>();
    List<Consumable> unacquiredConsumables = new List<Consumable>();

    // IDENTIFY ITEM TYPE
    public void StashItem(UsableItem usableItem, GameObject GO) {
        if(usableItem.GetComponent<Weapon>()) {
            DepositWeapon(usableItem as Weapon);
            //Destroy(GO);
            usableItem.gameObject.SetActive(false);
        }
        else if (usableItem.GetComponent<Armor>()) {
            DepositArmor(usableItem as Armor);
            //Destroy(GO);
            //GO.SetActive(false);
            usableItem.gameObject.SetActive(false);
        }
        else if (usableItem.GetComponent<Consumable>()) {
            DepositConsumable(usableItem as Consumable);
            //Destroy(GO);
            //GO.SetActive(false);
            usableItem.gameObject.SetActive(false);
        }
    }

    // WEAPON
    // Withdraw Weapon from Camp Horde to equip onto Player
    public void WithdrawWeapon(Weapon weapon) {
        if (inactiveWeapons.Count > 0) {
            inactiveWeapons.Remove(weapon);
        }
        activeWeapons.Add(weapon);
    }

    // Deposit equipped Weapon from Player into Camp Horde 
    public void DepositWeapon(Weapon weapon) {
        if(activeWeapons.Count > 0) {
            activeWeapons.Remove(weapon);
        }
        inactiveWeapons.Add(weapon);
    }

    // Acquire new Weapon from the game and add it to the inactive Weapon pool
    public void AcquireWeapon(Weapon weapon) {
        if(unacquiredWeapons.Contains(weapon)) {
            inactiveWeapons.Add(weapon);
            unacquiredWeapons.Remove(weapon);
        }
    }

    // ARMOR
    // Withdraw Armor from Camp Horde to equip onto Player
    public void WithdrawArmor(Armor armor) {
        activeArmor.Add(armor);
        inactiveArmor.Remove(armor);
    }

    // Deposit equipped Armor from Player into Camp Horde 
    public void DepositArmor(Armor armor) {
        inactiveArmor.Add(armor);
        activeArmor.Remove(armor);
    }

    // Acquire new Armor from the game and add it to the inactive Armor pool
    public void AcquireArmor(Armor armor) {
        if (unacquiredArmor.Contains(armor)) {
            inactiveArmor.Add(armor);
            unacquiredArmor.Remove(armor);
        }
    }

    // CONSUMABLE
    // Deposit equipped Consumable from Player into Camp Horde
    public void DepositConsumable(Consumable consumable) {
        activeConsumables.Remove(consumable);
        inactiveConsumables.Add(consumable);
    }

    // Withdraw Consumable from Camp Horde to equip onto Player
    public void WithdrawConsumable(Consumable consumable) {
        inactiveConsumables.Remove(consumable);
        activeConsumables.Add(consumable);
    }

    // Acquire new Consumable from the game and add it to the inactive Consumable pool
    public void AcquireConsumable(Consumable consumable) {
        if(unacquiredConsumables.Contains(consumable)) {
            unacquiredConsumables.Remove(consumable);
            inactiveConsumables.Add(consumable);
        }
    }
}