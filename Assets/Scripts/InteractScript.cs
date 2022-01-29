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

    void FixedUpdate()
    {
        collidedObjects.Clear(); //clear the list of all tracked objects.
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!collidedObjects.Contains(col) && col.gameObject.tag=="interactable" && canInteract)
        {
            collidedObjects.Add(col);
            InteractPrompt.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col); //same as enter
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (collidedObjects.Count <= 0)
        {
            InteractPrompt.SetActive(false);
        }
    }
}
