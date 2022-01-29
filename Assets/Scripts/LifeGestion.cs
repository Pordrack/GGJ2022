using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGestion : MonoBehaviour
{
    protected double liveTotal = 3;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(double TotalDamage){
        this.liveTotal-=TotalDamage;
        print(liveTotal);
        animator.SetBool("TakeDamage",true);
        Invoke ("DisableDamagAnim", (float)0.05);
    }
    void DisableDamagAnim(){
        if(this.liveTotal <=0){
            animator.SetTrigger("IsDead");
            this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
        }
        animator.SetBool("TakeDamage",false);
        
    }
}
