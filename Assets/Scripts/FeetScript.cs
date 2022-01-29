using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    //https://answers.unity.com/questions/1124822/how-many-colliders-im-colliding-with.html
    public bool onGround=false;

    List<Collider2D> collidedObjects = new List<Collider2D>();

    void FixedUpdate()
    {
        collidedObjects.Clear(); //clear the list of all tracked objects.
    }

    // if there is collision with an object in either Enter or Stay, add them to the list 
    // (you can check if it already exists in the list to avoid double entries, 
    // just in case, as well as the tag).
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!collidedObjects.Contains(col.collider) && col.gameObject.name!="Roger")
        {
            collidedObjects.Add(col.collider);
        }
        onGround = true;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        OnCollisionEnter2D(col); //same as enter
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collidedObjects.Count <= 0)
        {
            onGround = false;
        }
    }
}
