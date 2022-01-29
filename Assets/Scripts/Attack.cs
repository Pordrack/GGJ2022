using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hitRange;
    protected Vector3 objectScale;
    // Start is called before the first frame update
    void Start()
    {

    // Gets the local scale of game object
    objectScale = hitRange.transform.localScale;
    // Sets the local scale of game object
    DisableHitRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            hitRange.transform.localScale = new Vector3(objectScale.x,  objectScale.y, objectScale.z);
            Invoke ("DisableHitRange", (float)0.05);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy")) {
            Debug.Log(other);
        }
    }
    void DisableHitRange(){
        hitRange.transform.localScale = new Vector3(0,  0, objectScale.z);
    }
}
