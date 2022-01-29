using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverNegativeScale : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.lossyScale.x < 0)
        {
            transform.localScale =new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }   
    }
}
