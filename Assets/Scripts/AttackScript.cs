using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject roger;
    public GameObject projectile;
    private double attackCount;
    public Rigidbody2D rbProjectile;
    private Vector2 mMovement;
    private Int16 isFacingRight = 1;
    public Animator animator;
    public GameObject robot;
    private Boolean isInRange;
    private Boolean isLasering;
    private Rigidbody2D clone;
    private float cooldown = 0;
    public float maxCooldown = 1;
    // Start is called before the first frame update
    void Start()
    {
        // Animator animator = this.GetComponent<Animator>(); 
        attackCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(roger.transform.position.x <= this.transform.parent.position.x){
            this.transform.parent.rotation = new Quaternion(0,-180,0,0);
            isFacingRight = -1;
        }else{
            this.transform.parent.rotation = new Quaternion(0,0,0,0);
            isFacingRight = 1;
        }       
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
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && roger.transform.position.y >= this.transform.parent.position.y && (roger.transform.position.x >= this.transform.parent.position.x-2.7 && roger.transform.position.x <= this.transform.parent.position.x+2.7)) {
            isLasering = true;
            BigLaserSaMere();
        }else{
            isLasering = false;
        }
    }
    void StartAttackingPlayer(){   
        if(isInRange && !isLasering && cooldown<0){
            cooldown = maxCooldown;
            robot.GetComponent<LifeGestion>().SwapAttaks();
            attackCount+=1;
            Rigidbody2D clone;
            clone = Instantiate(rbProjectile, transform.position, transform.rotation);
            mMovement = new Vector2 (1,0);
            clone.AddForce((mMovement * 1000 * isFacingRight));
        }
        Invoke("StartAttackingPlayer",2f);
        
    }   
    void BigLaserSaMere(){
        robot.GetComponent<LifeGestion>().InitiateLaser(); 
    }
}
