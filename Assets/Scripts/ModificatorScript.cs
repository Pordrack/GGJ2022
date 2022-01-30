using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorScript : MonoBehaviour
{
    public GameObject question;
    public GameObject consequenceA;
    public GameObject consequenceB;
    
    public void choiceA()
    {
        question.SetActive(false);
        consequenceA.SetActive(true);
    }

    public void choiceB()
    {
        question.SetActive(false);
        consequenceB.SetActive(true);
    }

    public void jumpFurther()
    {
        RogerScript.singleton.jumpForce *= 1.5f;
        SpiritScript.singleton.canFly = false;
        SpiritScript.singleton.GetComponent<Rigidbody2D>().gravityScale = 2;
        SpiritScript.singleton.GetComponent<Collider2D>().isTrigger = false;
    }

    public void runFaster()
    {
        RogerScript.singleton.baseSpeed *= 1.5f;
        RogerScript.singleton.baseSpeed /= 1.5f;
    }

}
