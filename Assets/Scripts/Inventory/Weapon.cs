﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IUsableItem, IInteractable {
    public enum Stance { NONE, ONE_HAND, TWO_HAND, DUAL_WIELD, OFF_HAND, SHIELD }

    [SerializeField]
    protected Transform frontWeaponTransform;
    [SerializeField]
    protected Transform backWeaponTransform;
    [SerializeField]
    protected Stance equipStance;

    [SerializeField]
    protected int damage = 0;
    [SerializeField]
    protected float knockBack = 0;

    [SerializeField]
    protected Animator charAnimator;
    [SerializeField]
    protected Animator myAnimator;
    [SerializeField]
    protected float animationSpeed = 100.0f;
    [SerializeField]
    protected List<string> animationTriggers = new List<string>();

    [SerializeField]
    protected LayerMask enemyLayer;
    [SerializeField]
    protected List<HitBox> hitBoxes = new List<HitBox>();
    [SerializeField]
    protected int currentHitBox = 0;
    [SerializeField]
    private bool checkForHit = false;
    [SerializeField]
    private List<Collider2D> hitColliders = new List<Collider2D>();

    [System.Serializable]
    public class HitBox {
        //[SerializeField] protected Shape shape Shape.Shape;
        [SerializeField]
        public Vector2 center = Vector2.zero;
        [SerializeField]
        public Vector2 size = new Vector2(1, 1);
        [SerializeField]
        public float angle = 0.0f;
    }

    protected void Awake() {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    public void Equip(Transform firstAttachmentPoint = null, Transform secondAttachmentPoint = null, Animator equipAnimator = null) {
        //assign Animator
        charAnimator = equipAnimator;

        if (firstAttachmentPoint == null && secondAttachmentPoint == null)
            return;

        // Equip the weapon to the AttachmentPoints based on the weapons stance
        switch (equipStance) {
            case Stance.ONE_HAND:

                if (firstAttachmentPoint != null) {
                    transform.position = firstAttachmentPoint.position;
                    transform.SetParent(firstAttachmentPoint);
                }
                break;
            case Stance.TWO_HAND:

                if (firstAttachmentPoint != null) {
                    transform.position = firstAttachmentPoint.position;
                    transform.SetParent(firstAttachmentPoint);
                }

                break;
            case Stance.DUAL_WIELD:
                //TODO Weapons with two objects that are to be equipped to two seperate points need to have two seperate Parent Transforms and Animators  They must still be handled by one Weapon Script.
                if (firstAttachmentPoint != null && frontWeaponTransform != null) {
                    frontWeaponTransform.position = firstAttachmentPoint.position;
                    frontWeaponTransform.SetParent(firstAttachmentPoint);
                }

                if (secondAttachmentPoint != null && backWeaponTransform != null) {
                    backWeaponTransform.position = secondAttachmentPoint.position;
                    backWeaponTransform.SetParent(firstAttachmentPoint);
                }
                break;
            case Stance.OFF_HAND:

                if (secondAttachmentPoint != null) {
                    transform.position = secondAttachmentPoint.position;
                    transform.SetParent(firstAttachmentPoint);
                }
                break;
            default:

                break;

        }

    }

    public void Unequip() {
        charAnimator = null;

        if (equipStance == Stance.DUAL_WIELD) {
            if (frontWeaponTransform != null)
                frontWeaponTransform.SetParent(this.transform);
            if (backWeaponTransform != null)
                backWeaponTransform.SetParent(this.transform);
        }
        else {
            transform.SetParent(this.transform);
        }

    }

    //===== IInteractable functions =====//
    public void StartInteracting() 
	{

    }

    public void Interacting() 
	{

    }

    public void StopInteracting()
	{

    }

    //===== IUsableItem functions =====//
    public virtual void StartUsingItem()
	{

    }

    public virtual void StopUsingItem() 
	{

    }

    public virtual void StartHit() {
        checkForHit = true;
        StartCoroutine("CheckingForHit", false);

    }

    public virtual void CheckHitOnce() {
        checkForHit = true;
        StartCoroutine("CheckingForHit", true);
    }

    IEnumerator CheckingForHit(bool checkOnce = false) {
        while (checkForHit) {
            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(hitBoxes[currentHitBox].center, hitBoxes[currentHitBox].size, hitBoxes[currentHitBox].angle, enemyLayer.value);

            foreach (Collider2D hitObject in hitObjects) {
                if (!hitColliders.Contains(hitObject)) {
                    IDamageable id = hitObject.GetComponent<IDamageable>();

                    if (id != null)
                        id.TakeDamage(damage, knockBack);

                    hitColliders.Add(hitObject);
                }

            }

            if (checkOnce)
                checkForHit = false;

            yield return null;
        }

        hitColliders.Clear();
    }

    public virtual void StopHit() {
        checkForHit = false;
    }

    public void ForceInterrupt() {
        checkForHit = false;
        //stop myAnimations
        //stop charAnimations
    }

    public void OnDrawGizmos() {
        if (checkForHit && hitBoxes.Count > 0) {    //points to the hit colliders
            Gizmos.color = Color.red;
            foreach (Collider2D hitCollider in hitColliders) {
                Gizmos.DrawLine(this.transform.position, hitCollider.transform.position);
            }

            Gizmos.color = Color.yellow;
        }
        else
            Gizmos.color = Color.white;
        //Draws the hitBox for the attack
        if (hitBoxes.Count > 0)
            Gizmos.DrawWireCube(hitBoxes[currentHitBox].center, hitBoxes[currentHitBox].size);
    }
}
