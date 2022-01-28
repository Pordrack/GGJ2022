using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    public bool onGround=false;

    public int nbrOfColliders;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nbrOfColliders++;
        onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        nbrOfColliders--;
        if (nbrOfColliders <= 0)
        {
            onGround = false;
        }
    }
}
