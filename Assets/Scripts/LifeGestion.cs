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
        animator.SetBool("TakeDamage",false);
        if(this.actualHP <=0){
            animator.SetBool("IsDead", true);
            this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
            ParticleSystem part = this.gameObject.GetComponent<ParticleSystem>();
            if(part){
                part.enableEmission=false;
            }
            if (gameObject.GetComponentInChildren<Shoot>() != null)
            {
                gameObject.GetComponentInChildren<Shoot>().enabled = false;
            }
            if (gameObject.GetComponentInParent<MovingElement>() != null)
            {
                gameObject.GetComponentInParent<MovingElement>().enabled = false;
            }
        }
        
    }
    public void SwapAttaks(){
        if(animator.GetBool("isAttackingWithMissilesArms")){
            animator.SetBool("isAttackingWithMissilesArms",false);
            animator.SetBool("isAttackingWithMissilesChest",true);
        }else{
            animator.SetBool("isAttackingWithMissilesArms",true);
            animator.SetBool("isAttackingWithMissilesChest",false);
        }
    }
    public void StopAttacking(){
        animator.SetBool("isAttackingWithMissilesChest",false);
        animator.SetBool("isAttackingWithMissilesArms",false);
    }
    public void InitiateLaser(){
        StopAttacking();
        animator.SetBool("isLazering",true);
    }
    public void StopLaser(){
        animator.SetBool("isLazering",false);

    }
}
