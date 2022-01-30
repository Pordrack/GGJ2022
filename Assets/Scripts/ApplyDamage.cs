using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public bool selfDestruct = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            RogerScript.singleton.GetComponent<Respawn>().Die();
            if (selfDestruct)
            {
                Destroy(gameObject);
            }          
        }
    }
}
