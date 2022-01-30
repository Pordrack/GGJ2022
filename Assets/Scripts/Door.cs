using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    public void onPress()
    {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.isTrigger = true;
        SoundManager.singleton.door.Play();
        animator.SetBool("open", true);
    }
}

