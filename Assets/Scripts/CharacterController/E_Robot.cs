using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Robot : CombatCharacter {
    [SerializeField] private LayerMask LM;
    private bool m_Grounded;
    private float walkSpeed = .05f;
    private float runSpeed = 1;

    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Grounded = true;
        m_Anim.SetBool("Ground", m_Grounded);
        m_FacingRight = true;
        m_Anim.SetFloat("Speed", walkSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        Patrol();
	}

    private void Patrol() 
    {
        Debug.Log("m_FacingRight: " + m_FacingRight);
        if (m_FacingRight)
        {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(1, 0, 0), new Vector2(1, 2), 0f, LM);
            if(c != null) {
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                Flip();
                //m_FacingRight = !m_FacingRight;
            }
        }
        else {
            m_Rigidbody2D.MovePosition(transform.position + new Vector3(-.1f, 0, 0));
            Collider2D c = Physics2D.OverlapBox(transform.position + new Vector3(-1, 0, 0), new Vector2(1, 2), 0f, LM);
            if (c != null) {
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                Flip();
                //m_FacingRight = !m_FacingRight;
            }
        }
    }

    void OnDrawGizmos() {
        if (m_FacingRight) {
            Gizmos.DrawWireCube(transform.position + new Vector3(1, 0, 0), new Vector3(1, 2, 0));
        }
        else {
            Gizmos.DrawWireCube(transform.position + new Vector3(-1, 0, 0), new Vector3(1, 2, 0));
        }
    }
}
