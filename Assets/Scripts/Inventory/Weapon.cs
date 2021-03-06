﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IUsableItem, IInteractable 
{
    public enum Stance { NONE, ONE_HAND, TWO_HAND, DUAL_WIELD, OFF_HAND, SHIELD }

    protected Transform frontWeaponTransform;
    protected Transform backWeaponTransform;
	protected Collider2D weaponCollider;

	[SerializeField] protected uint damage = 0;
	[SerializeField] protected float knockBack = 0;

    [SerializeField] protected Stance equipStance;
	[SerializeField] protected float weaponRotation = 0.0f;

	private bool equipped = false;

	protected Animator charAnimator;
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected float animationSpeed = 100.0f;
    [SerializeField] protected List<string> charAnimationTriggers = new List<string>();
	private int maxAttacks = 0;
	private int currentAttack = 0;
	public bool blockAttackInput = false;

    [SerializeField] protected LayerMask enemyLayer;
//    [SerializeField] protected List<HitBox> hitBoxes = new List<HitBox>();
	[SerializeField] protected List<Collider2D> hitBoxes =  new List<Collider2D>();
    [SerializeField] protected int currentHitBox = 0;
    [SerializeField] private bool checkForHit = false;
    [SerializeField] private List<Collider2D> hitColliders = new List<Collider2D>();

    [System.Serializable]
    public class HitBox 
	{
        //TODO create a Shape type for the hitBoxes [SerializeField] protected Shape shape Shape.Shape;
        [SerializeField] public Vector2 center = Vector2.zero;
        [SerializeField] public Vector2 size = new Vector2(1, 1);
        [SerializeField] public float angle = 0.0f;

    }

    protected void Awake() 
	{
		weaponCollider = GetComponent<Collider2D> ();

		Collider2D[] colliders = GetComponentsInChildren<Collider2D> (true);

		foreach (Collider2D collider in colliders) 
		{
			if (collider.name == "Visual")
				hitBoxes.Add (collider);
		}

        weaponAnimator = gameObject.GetComponent<Animator>();
		charAnimationTriggers.TrimExcess ();
		maxAttacks = charAnimationTriggers.Count;
    }

    public void Equip(Transform firstAttachmentPoint = null, Transform secondAttachmentPoint = null, Animator equipAnimator = null) 
	{
        //assign Animator
        charAnimator = equipAnimator;

        if (firstAttachmentPoint == null && secondAttachmentPoint == null)
            return;

        // Equip the weapon to the AttachmentPoints based on the weapons stance
        switch (equipStance) 
		{
            case Stance.ONE_HAND:

            if (firstAttachmentPoint != null) 
			{
				weaponCollider.enabled = false;

                transform.position = firstAttachmentPoint.position;
                transform.SetParent(firstAttachmentPoint);
				transform.localRotation = Quaternion.Euler(0,0, weaponRotation);

            }
            break;
            case Stance.TWO_HAND:

            if (firstAttachmentPoint != null) 
			{
				weaponCollider.enabled = false;

                transform.position = firstAttachmentPoint.position;
				transform.SetParent(firstAttachmentPoint);
				transform.localRotation = Quaternion.Euler(0,0, weaponRotation);
            }

            break;
            case Stance.DUAL_WIELD:
            //TODO Weapons with two objects that are to be equipped to two seperate points need to have two seperate Parent Transforms and Animators  They must still be handled by one Weapon Script.
            if (firstAttachmentPoint != null && frontWeaponTransform != null) 
			{
                frontWeaponTransform.position = firstAttachmentPoint.position;
                frontWeaponTransform.SetParent(firstAttachmentPoint);
            }

            if (secondAttachmentPoint != null && backWeaponTransform != null) 
			{
                backWeaponTransform.position = secondAttachmentPoint.position;
                backWeaponTransform.SetParent(firstAttachmentPoint);
            }
            break;
            case Stance.OFF_HAND:

            if (secondAttachmentPoint != null) 
			{
				weaponCollider.enabled = false;
				
                transform.position = secondAttachmentPoint.position;
                transform.SetParent(firstAttachmentPoint);
				transform.localRotation = Quaternion.Euler(0,0, weaponRotation);
            }
            break;
			default:

            break;
        }

		equipped = true;
    }

    public void Unequip() 
	{
        charAnimator = null;

        if (equipStance == Stance.DUAL_WIELD) 
		{
            if (frontWeaponTransform != null)
                frontWeaponTransform.SetParent(this.transform);
            if (backWeaponTransform != null)
                backWeaponTransform.SetParent(this.transform);
        }
        else 
		{
            transform.SetParent(null);
			weaponCollider.enabled = true;
        }
		equipped = false;
    }

    //===== IInteractable functions =====//
	public void StartInteracting(Character interactingCharacter = null) 
	{
		CombatCharacter combatCharacter = interactingCharacter as CombatCharacter;

		if (!equipped && interactingCharacter != null) 
		{
			combatCharacter.SetFrontWeapon (this);
		}
    }

    public void StopInteracting()
	{

    }

    //===== IUsableItem functions =====//
    public virtual void StartUsingItem()
	{
		if(!blockAttackInput)
		{
			blockAttackInput = true;
			charAnimator.SetBool ("Attacking", true);

			//Start Attack Animations
			if (charAnimationTriggers.Count > 0) 
			{
				charAnimator.SetTrigger (charAnimationTriggers [currentAttack]);

				if (weaponAnimator != null)
					weaponAnimator.SetTrigger ("Attack");
			} 
			else
			{
				//if charAnimationTriggers is empty, than no animation will play.
			}

			currentAttack++;
			if(currentAttack >= maxAttacks)
				currentAttack = 0;
		}
    }

    public virtual void StopUsingItem() 
	{

    }

    public virtual void StartHit() 
	{
		blockAttackInput = false;

        checkForHit = true;
        StartCoroutine("CheckingForHit", false);

    }

    public virtual void CheckHitOnce() 
	{
        checkForHit = true;
        StartCoroutine("CheckingForHit", true);
    }

    IEnumerator CheckingForHit(bool checkOnce = false) 
	{
		weaponCollider.isTrigger = true;
		weaponCollider.enabled = true;

		ContactFilter2D filter = new ContactFilter2D();
		filter.SetLayerMask(enemyLayer);

        while (checkForHit) 
		{
			Collider2D[] hitObjects = new Collider2D[2];

			Physics2D.OverlapCollider(hitBoxes[currentHitBox], filter, hitObjects);

            foreach (Collider2D hitObject in hitObjects) 
			{
				if (hitObject != null && !hitColliders.Contains(hitObject) && hitObject.transform.root != this.transform.root) 
				{
                    IDamageable id = hitObject.GetComponent<IDamageable>();

                    if (id != null)
                        id.TakeDamage(damage, knockBack);

                    hitColliders.Add(hitObject);
					Debug.Log ("Hit" + hitObject.name);
                }

            }

			if (checkOnce) 
			{
				checkForHit = false;
				charAnimator.SetBool ("Attacking", false);
			}


            yield return null;
        }
			
		hitColliders.Clear();

		weaponCollider.isTrigger = false;
		weaponCollider.enabled = false;
    }

    public virtual void StopHit() 
	{
        checkForHit = false;
		charAnimator.SetBool ("Attacking", false);
    }

    public void ForceInterrupt() 
	{
        checkForHit = false;
		blockAttackInput = false;
		charAnimator.SetBool ("Attacking", false);
        //stop weaponAnimations
        //stop charAnimations
    }

    public void OnDrawGizmos() {
        if (checkForHit && hitBoxes.Count > 0)
		{    //points to the hit colliders
            Gizmos.color = Color.red;
            foreach (Collider2D hitCollider in hitColliders) 
			{
				Gizmos.DrawLine(hitBoxes[currentHitBox].bounds.center, hitCollider.transform.position);
            }

            Gizmos.color = Color.yellow;
        }
        else
            Gizmos.color = Color.white;
        //Draws the hitBox for the attack
        if (hitBoxes.Count > 0)
			Gizmos.DrawWireCube(hitBoxes[currentHitBox].bounds.center, hitBoxes[currentHitBox].bounds.size);
    }
}
