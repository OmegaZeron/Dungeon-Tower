using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Robot : CombatCharacter {
    [SerializeField] private LayerMask LM;
    private bool m_Grounded;
    private float walkSpeed = .05f;
    private float runSpeed = 1;
    [SerializeField] private uint damageDealt;
    [SerializeField] private float knockback;
    [SerializeField] private float selfKnockback = 3000;
    [SerializeField] private BoxCollider2D playerCheck;
    private Collider2D[] check;

    [SerializeField] private GameObject player;

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
				playerCheck = collider as BoxCollider2D;
            }
        }
        health = maxHealth = 50;
        inventory.currency = 7;
	}
	
	// Update is called once per frame
	void Update () {
        Patrol();
	}

    private void OnCollisionEnter2D(Collision2D collision) // Damage Player
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            GameManager.instance.Player.GetComponent<PlatformerCharacter2D>().TakeDamage(damageDealt, knockback);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Detect Player
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            player = GameManager.instance.Player.gameObject;
            StartCoroutine("AttackPlayer");
        }
        if (collision.gameObject.GetComponent<Weapon>()) {
            TakeDamage(collision.gameObject.GetComponent<Weapon>().damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == GameManager.instance.Player.gameObject) {
            player = null;
            m_Rigidbody2D.velocity = m_Rigidbody2D.velocity.normalized;
        }
    }

    IEnumerator AttackPlayer()
    {
        while(player != null) 
        {
            MoveTo(player.transform.position, 20);
            yield return null;
        }
    }

    private void MoveTo(Vector3 position, float speed)
    {
        Debug.Log("Robot MoveTo" + player.name);
        Vector3 target = new Vector3(position.x, position.y, 0); // Tandy: this corrects position
        Vector3 offset = new Vector3(position.x - transform.position.x, 0, 0);
        if(offset.x > 0 && !m_FacingRight || offset.x < 0 && m_FacingRight)
        {
            Flip();
        }
        //m_Rigidbody2D.MovePosition((target * 0.1f) + target * speed * Time.deltaTime);
        m_Rigidbody2D.velocity = offset.normalized * speed;

        Collider2D c;
        if (m_FacingRight) {
            c = Physics2D.OverlapBox(transform.position + new Vector3(.75f, 0, 0), new Vector2(.75f, 2), 0f, LM);
        }
        else {
            c = Physics2D.OverlapBox(transform.position + new Vector3(-.75f, 0, 0), new Vector2(.75f, 2), 0f, LM);
        }
        if (c != null) {
            m_Rigidbody2D.velocity = Vector2.zero;
            //TakeKnockback(selfKnockback);
            Flip();
        }
    }

    private void Patrol() 
    {
        //Debug.Log("m_FacingRight: " + m_FacingRight);
        if (m_FacingRight)
        {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(.75f, 0, 0), new Vector2(.75f, 2), 0f, LM);
            if(c != null) {
                Flip();
            }
        }
        else {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(-.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(-.75f, 0, 0), new Vector2(.75f, 2), 0f, LM);
            if (c != null) {
                Flip();
            }
        }
    }

    void OnDrawGizmos() {
        if (m_FacingRight) {
            Gizmos.DrawWireCube(transform.position + new Vector3(.75f, 0, 0), new Vector3(.75f, 2, 0));
        }
        else {
            Gizmos.DrawWireCube(transform.position + new Vector3(-.75f, 0, 0), new Vector3(.75f, 2, 0));
        }
    }
}
