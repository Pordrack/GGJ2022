using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour
{
    public List<GameObject> canvas;
    private Animator animator;
    private int step=-1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void onPress()
    {
        step++;

        if (step > 0)
        {
            canvas[step - 1].SetActive(false);
        }
        if (step < canvas.Count)
        {

            canvas[step].SetActive(true);
        }
        else
        {
            step = -1;
        }
    }
}
