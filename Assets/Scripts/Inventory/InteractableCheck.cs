using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheck : MonoBehaviour
{
	[SerializeField] private GameObject highlighter;

	private List<GameObject> interactableGameObjects = new List<GameObject>();
    
	[SerializeField] protected internal GameObject closest_Object;
	[SerializeField] protected internal IInteractable closest_interactable;
    //TODO add properties
	protected internal List<Item> equippedItems = new List<Item>();
	protected internal List<IInteractable> equippedInteractables = new List<IInteractable> ();

	public GameObject closestObject
	{
		get{ return closest_Object;}
	}

	public IInteractable closestInteractable 
	{
		get{ return closest_interactable;}
	}

	public List<Item> IgnoredObjects
	{
		get{ return equippedItems; }
	}

	public void Ignore(Item ignoreItem)
	{
		if (!equippedItems.Contains (ignoreItem)) 
		{
			equippedItems.Add (ignoreItem);

			if(ignoreItem.GetType() == typeof(IInteractable) )
				equippedInteractables.Add (ignoreItem as IInteractable);
		}

		if( interactableGameObjects.Contains(ignoreItem.gameObject) )
			interactableGameObjects.Remove(ignoreItem.gameObject);
	}

	public void Unignore(Item unignoreItem)
	{
		if (equippedItems.Contains (unignoreItem)) 
		{
			equippedItems.Remove (unignoreItem);

			if(unignoreItem.GetType() == typeof(IInteractable) )
				equippedInteractables.Remove (unignoreItem as IInteractable);
		}

		
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
			if( !equippedInteractables.Contains(iInt) )
           		interactableGameObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
	{
		if (interactableGameObjects.Contains(collision.gameObject) )
		{
            interactableGameObjects.Remove(collision.gameObject);
        }
			
    }

    private void Update()
	{
		//Turn off the Highlighter if there are no IInteractables in the Interactable Check Range
        if(interactableGameObjects.Count == 0 && closest_Object != null) {
            closest_Object = null;
            highlighter.SetActive(false);
        }
        else
		{	//find the clostest GO in interactables inside of the Interactable Check
            float distance = float.MaxValue;
			foreach (GameObject GO in interactableGameObjects) 
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
