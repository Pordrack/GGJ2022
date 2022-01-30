using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public bool canInteract = true;
    private GameObject InteractPrompt;
    List<Collider2D> collidedObjects = new List<Collider2D>();

    private void Start()
    {
        InteractPrompt = transform.Find("InteractKey").gameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            foreach (Collider2D col in collidedObjects)
            {
                InteractableScript interactableScript = col.gameObject.GetComponent<InteractableScript>();
                interactableScript.onPress();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="interactable" && canInteract)
        {
            if (!collidedObjects.Contains(col))
            {
                collidedObjects.Add(col);
                InteractPrompt.SetActive(true);
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (collidedObjects.Contains(col))
        {
            collidedObjects.Remove(col);
        }
        if (collidedObjects.Count <= 0)
        {
            InteractPrompt.SetActive(false);
        }
    }
}
