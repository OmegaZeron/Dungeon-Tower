using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheck : MonoBehaviour
{
	[SerializeField] private GameObject highlighter;

	private List<GameObject> interactibles = new List<GameObject>();

    protected internal GameObject closest_Object;
    protected internal IInteractable closest_interactable;


	public GameObject closestObject
	{
		get{ return closest_Object;}
	}

	public IInteractable closestInteractable 
	{
		get{ return closest_interactable;}
	}


    private void Start() 
	{
        highlighter.SetActive(false);        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
	{
		IInteractable iInt = collision.GetComponent<IInteractable> ();
		if(iInt != null) 
		{
            interactibles.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
	{
		if (interactibles.Contains(collision.gameObject) )
		{
            interactibles.Remove(collision.gameObject);
        }
			
    }

    private void Update()
	{
		//Turn off the Highlighter if there are no IInteractables in the Interactable Check Range
        if(interactibles.Count == 0 && closest_Object != null) {
            closest_Object = null;
            highlighter.SetActive(false);
        }
        else
		{	//find the clostest GO in interactables inside of the Interactable Check
            float distance = float.MaxValue;
			foreach (GameObject GO in interactibles) 
			{
                float newDistance = 0f;
				Vector2 objectPos = GO.transform.position;
                Vector2 playerPos = gameObject.transform.position;
                newDistance = (objectPos - playerPos).magnitude;

                if (newDistance < distance)
				{
                    distance = newDistance;
					closest_Object = GO;
                    closest_interactable = closest_Object.GetComponent<IInteractable>();
                    if(closest_interactable == null) {
                        Debug.Log("closest_interactable null");
                    }
                }
            }
            if(closest_Object != null)
			{
                highlighter.transform.position = closest_Object.transform.position;
                if(highlighter.activeInHierarchy == false) {
                    highlighter.SetActive(true);
                }
            }
        }
			
    } // end Update()

}
