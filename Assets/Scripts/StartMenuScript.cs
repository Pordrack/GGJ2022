using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //On recupere les axes
        float v = Input.GetAxis("Vertical");

        if(h!=0 || v != 0)
        {
            gameObject.SetActive(false);
        }
    }
}
