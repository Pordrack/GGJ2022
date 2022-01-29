using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hitRange;
    protected Vector3 objectScale;
    private Animator animator;
    private Boolean canAttack;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        canAttack = true;
        // Gets the local scale of game object
        objectScale = hitRange.transform.localScale;
        // Sets the local scale of game object
        DisableHitRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && canAttack)
        {
            canAttack = false;
            hitRange.transform.localScale = new Vector3(objectScale.x,  objectScale.y, objectScale.z);
            animator.SetBool("AttackWithSword",true);
            Invoke ("DisableAttack", (float)0.05);
            Invoke ("DisableHitRange", (float)0.05);
            Invoke ("CanAttackAgain", (float)1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<LifeGestion>().TakeDamage(1);
        }
    }
    void DisableAttack(){
        animator.SetBool("AttackWithSword",false);
    }
    void DisableHitRange(){
        hitRange.transform.localScale = new Vector3(0,  0, objectScale.z);
    }
    void CanAttackAgain(){
        canAttack = true;
    }
}
