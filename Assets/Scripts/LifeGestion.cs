using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGestion : MonoBehaviour
{
    protected double liveTotal = 3;
    protected double actualHP;
    private Animator animator;
    ParticleSystem part;
    // Start is called before the first frame update
    void Start()
    {
        actualHP = liveTotal;
        animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("LifeUnder50Percent",false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(double TotalDamage){
        this.actualHP-=TotalDamage;
        animator.SetBool("TakeDamage",true);
        if(this.actualHP <= this.liveTotal/2){
        animator.SetBool("LifeUnder50Percent",true);
        }
        Invoke ("DisableDamagAnim", (float)0.05);
    }
    void DisableDamagAnim(){
        if(this.actualHP <=0){
            animator.SetBool("IsDead", true);
            this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
            this.gameObject.GetComponent<ParticleSystem>().enableEmission=false;
        }
        animator.SetBool("TakeDamage",false);
        
    }
}
