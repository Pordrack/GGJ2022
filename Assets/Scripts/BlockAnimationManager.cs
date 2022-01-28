using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationManager : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartGroundAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGroundAnimations(){
        animator.SetTrigger("StartAnimation");
    }
}
