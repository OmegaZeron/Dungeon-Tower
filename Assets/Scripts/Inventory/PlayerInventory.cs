using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    private List<GameObject> interactibles = new List<GameObject>();
    public GameObject closest;
    [SerializeField] private GameObject highlighter;

    private void Start() {
        highlighter.SetActive(false);        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Weapon>() || collision.GetComponent<Armor>()) {
            interactibles.Add(collision.gameObject);
            //Debug.Log("interactibles.Add(" + collision.gameObject.name + ");");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Weapon>() || collision.GetComponent<Armor>()) {
            interactibles.Remove(collision.gameObject);
            //Debug.Log("interactibles.Remove(" + collision.gameObject.name + ");");
        }
    }

    private void Update() {
        if(interactibles.Count == 0 && closest != null) {
            closest = null;
            highlighter.SetActive(false);
            //Debug.Log("interactibles.Count == 0");
        }
        else {
            float distance = float.MaxValue;
            foreach (GameObject GO in interactibles) {
                float newDistance = 0f;
                Vector2 objectPos = GO.transform.position;
                Vector2 playerPos = gameObject.transform.position;
                newDistance = (objectPos - playerPos).magnitude;

                if (newDistance < distance) {
                    distance = newDistance;
                    closest = GO;
                    //Debug.Log("closest = " + GO.name);
                }
            }
            if(closest != null) {
                highlighter.transform.position = closest.transform.position;
                if(highlighter.activeInHierarchy == false) {
                    highlighter.SetActive(true);
                }
            }
        }
    } // end Update()
}
