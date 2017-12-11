using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheck : MonoBehaviour
{
    [SerializeField] private GameObject highlighter;
    private GameObject highlighterInstance;
    [SerializeField] private bool useHighlighter = false;

    private List<GameObject> interactableGameObjects = new List<GameObject>();

    [SerializeField] protected internal GameObject closest_Object;
    protected internal IInteractable closest_interactable = null;
    //TODO add properties

    private Inventory inventory;

    public GameObject closestObject
    {
        get { return closest_Object; }
    }

    public IInteractable closestInteractable
    {
        get { return closest_interactable; }
    }

    public List<Item> IgnoredObjects
    {
        get { return inventory.equippedItems; }
    }

    public void Ignore(Item ignoreItem)
    {
        if (!inventory.equippedItems.Contains(ignoreItem))
        {
            inventory.equippedItems.Add(ignoreItem);

            if (ignoreItem.GetType() == typeof(IInteractable))
                inventory.equippedInteractables.Add(ignoreItem as IInteractable);
        }

        if (interactableGameObjects.Contains(ignoreItem.gameObject))
            interactableGameObjects.Remove(ignoreItem.gameObject);
    }

    public void Unignore(Item unignoreItem)
    {
        if (inventory.equippedItems.Contains(unignoreItem))
        {
            inventory.equippedItems.Remove(unignoreItem);

            if (unignoreItem.GetType() == typeof(IInteractable))
                inventory.equippedInteractables.Remove(unignoreItem as IInteractable);
        }


    }

    private void Awake()
    {
        if (highlighter != null && useHighlighter)
        {
            inventory = FindObjectOfType<PlatformerCharacter2D>().inventory;
            highlighterInstance = Instantiate(highlighter);
            highlighterInstance.SetActive(false);
        }
        else if (useHighlighter)
        {
            Debug.LogError("Trying to use highlighter, but no highlighter prefab is set");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable iInt = collision.GetComponent<IInteractable>();
        if (iInt != null)
        {
            if (!inventory.equippedInteractables.Contains(iInt))
                interactableGameObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactableGameObjects.Contains(collision.gameObject))
        {
            interactableGameObjects.Remove(collision.gameObject);
        }

    }

    private void Update()
    {
        //Turn off the Highlighter if there are no IInteractables in the Interactable Check Range
        if (interactableGameObjects.Count == 0 && closest_Object != null)
        {
            closest_Object = null;
            if (highlighterInstance != null && useHighlighter)
            {
                highlighterInstance.SetActive(false);
            }
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
                    if (closest_interactable == null)
                    {
                        Debug.Log("closest_interactable null");
                    }
                }
            }
            if (closest_Object != null && useHighlighter)
            {
                if (highlighterInstance != null)
                {
                    highlighterInstance.transform.position = closest_Object.transform.position;
                    if (highlighterInstance.activeInHierarchy == false)
                    {
                        highlighterInstance.SetActive(true);
                    }
                }
            }
        }

    } // end Update()

}