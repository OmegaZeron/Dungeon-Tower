using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheck : MonoBehaviour {
    private List<GameObject> interactibles = new List<GameObject>();
    protected internal GameObject closest;
    protected internal Item closest_item;
    [SerializeField] private GameObject highlighter;
    //[SerializeField] private bool isAtCamp;
    private CampHorde camp;

    private void Start() {
        highlighter.SetActive(false);        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if(collision.GetComponent<Weapon>() || collision.GetComponent<Armor>()) {
        if(collision.GetComponent<Item>()) {
            interactibles.Add(collision.gameObject);
            //Debug.Log("interactibles.Add(" + collision.gameObject.name + ");");
        }

        if(collision.GetComponent<CampHorde>()) {
            Debug.Log("Camp collision!");
            camp = collision.GetComponent<CampHorde>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //if (collision.GetComponent<Weapon>() || collision.GetComponent<Armor>()) {
        if (collision.GetComponent<Item>()) {
            interactibles.Remove(collision.gameObject);
            //Debug.Log("interactibles.Remove(" + collision.gameObject.name + ");");
        }

        if (collision.GetComponent<CampHorde>()) {
            camp = null;
        }
    }

    private void Update() {
        if(camp != null && Input.GetKeyDown(KeyCode.E) && closest_item != null) {
            camp.StashItem(closest_item, closest);
            //Destroy(closest);
            //closest_item = null;
        }

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
                    closest_item = closest.GetComponent<Item>();
                    if(closest_item == null) {
                        Debug.Log("closest_item null");
                    }
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
