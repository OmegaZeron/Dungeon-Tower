﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Robot : CombatCharacter {
    [SerializeField] private LayerMask LM;
    private bool m_Grounded;
    private float walkSpeed = .05f;
    private float runSpeed = 1;
    [SerializeField] private uint damageDealt;
    [SerializeField] private float knockback;
    [SerializeField] private CircleCollider2D playerCheck;
    private Collider2D[] check;

    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Grounded = true;
        m_Anim.SetBool("Ground", m_Grounded);
        m_FacingRight = true;
        m_Anim.SetFloat("Speed", walkSpeed);
        check = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in check)
        {
            if (collider.name.Equals("PlayerCheck"))
            {
                playerCheck = (CircleCollider2D)collider;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        Patrol();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().TakeDamage(damageDealt, knockback);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            StartCoroutine("AttackPlayer");
        }
    }

    IEnumerator AttackPlayer()
    {
        yield return null;
    }

    private void Patrol() 
    {
        Debug.Log("m_FacingRight: " + m_FacingRight);
        if (m_FacingRight)
        {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(.7f, 0, 0), new Vector2(.5f, 2), 0f, LM);
            if(c != null) {
                Flip();
            }
        }
        else {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(-.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(-.7f, 0, 0), new Vector2(.5f, 2), 0f, LM);
            if (c != null) {
                Flip();
            }
        }
    }

    void OnDrawGizmos() {
        if (m_FacingRight) {
            Gizmos.DrawWireCube(transform.position + new Vector3(.7f, 0, 0), new Vector3(.5f, 2, 0));
        }
        else {
            Gizmos.DrawWireCube(transform.position + new Vector3(-.7f, 0, 0), new Vector3(.5f, 2, 0));
        }
    }
}
