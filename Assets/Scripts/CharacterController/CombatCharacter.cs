using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : Character {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFrontWeapon(Weapon equip)
    {
        if (frontEquippedWeapon != null)
        {
            frontEquippedWeapon.Unequip();

            interactCheck.Unignore(frontEquippedWeapon);
        }

        frontEquippedWeapon = equip;
        frontEquippedWeapon.Equip(frontWeapon, backWeapon, m_Anim);

        interactCheck.Ignore(equip);
        //Add item to InteractableCheck ignore list.
    }
}
