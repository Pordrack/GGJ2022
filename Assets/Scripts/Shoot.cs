using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Boolean isInRange;
    public Rigidbody2D rbProjectile;
    private Vector2 mMovement;
    private Int16 isFacingRight = 1;
    public Animator animator;
    public GameObject robot;
 
    private Boolean isLasering;
    private Rigidbody2D clone;  

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
            Invoke("StartAttackingPlayer",0.5f);
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isInRange = false;
            robot.GetComponent<LifeGestion>().StopAttacking();
        }
    }
    void StartAttackingPlayer(){   
        if(isInRange){
            robot.GetComponent<LifeGestion>().SwapAttaks();
            Rigidbody2D clone;
            clone = Instantiate(rbProjectile, transform.position, transform.rotation);
            mMovement = new Vector2 (1,0);
            clone.AddForce((mMovement * 1000 * isFacingRight));
        }
        Invoke("StartAttackingPlayer",2f);
        
    }   

}
