using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : Character
{
	[SerializeField] protected Transform frontWeapon;
	[SerializeField] protected Transform backWeapon;

	[SerializeField] protected Weapon frontEquippedWeapon;
	[SerializeField] protected Weapon backEquippedWeapon;

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

	public void StartAttacking()
	{
		if(frontEquippedWeapon != null)
			frontEquippedWeapon.StartUsingItem();
		else
			Debug.LogWarning ("Cannot Use frontEquippedWeapon as there is none");
	}

	public void StopAttacking()
	{
		if (frontEquippedWeapon != null)
			frontEquippedWeapon.StopUsingItem ();
	}

	public void StartHitCheck()
	{
		if(frontEquippedWeapon != null)
			frontEquippedWeapon.StartHit ();
	}

	public void StopHitCheck()
	{
		if(frontEquippedWeapon != null)
			frontEquippedWeapon.StopHit ();
	}
}
