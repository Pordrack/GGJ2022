using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    //https://answers.unity.com/questions/1277650/how-can-i-pass-a-method-like-a-parameter-unity3d-c.html
    public UnityEvent hasBeenPressed;
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void onPress()
    {
        hasBeenPressed.Invoke();
        if (animator != null)
        {
            animator.SetBool("ButtonPressed", true);
            Invoke("SetBoolFalse", 0.4f);
        }
    }

    public void SetBoolFalse()
    {
        animator.SetBool("ButtonPressed", false);
    }
}
