using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hare : CombatCharacter {

    [SerializeField] private GameObject player;
    private bool alert = false;

    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        m_Anim.SetBool("Alert", alert);
    }

    private void OnTriggerEnter2D(Collider2D collision) // Detect Player
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            player = GameManager.instance.Player.gameObject;
            alert = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.Player.gameObject)
        {
            player = null;
            alert = false;
        }
    }
}
