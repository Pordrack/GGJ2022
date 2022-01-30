using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour
{
    public List<GameObject> canvas;
    private Animator animator;
    private bool spiritTriggeredIt;
    private float cooldown = 0;


    private int step=-1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Interact") && step>0 && cooldown<0)
        {
            onPress();
        }
    }

    public void onPress()
    {
        step++;
        if (cooldown > 0)
        {
            return;
        }
        else
        {
            cooldown = 0.1f;

            if (step > 0)
            {
                canvas[step - 1].SetActive(false);
            }
            else
            {
                SoundManager.singleton.mumble.Play();
                if (SpiritScript.singleton != null)
                {
                    if (SpiritScript.singleton.isMoveable == true)
                    {
                        spiritTriggeredIt = true;
                        SpiritScript.singleton.gameObject.GetComponent<InteractScript>().canInteract = false;
                        SpiritScript.singleton.isMoveable = false;
                    }
                    else
                    {
                        spiritTriggeredIt = false;
                        RogerScript.singleton.gameObject.GetComponent<InteractScript>().canInteract = false;
                        RogerScript.singleton.isMoveable = false;
                    }
                }
                else
                {
                    spiritTriggeredIt = false;
                    RogerScript.singleton.gameObject.GetComponent<InteractScript>().canInteract = false;
                    RogerScript.singleton.isMoveable = false;
                }
            }

            if (step < canvas.Count)
            {
                animator.SetBool("talking", true);
                canvas[step].SetActive(true);
            }
            else
            {
                SoundManager.singleton.mumble.Stop();
                animator.SetBool("talking", false);
                step = -1;
                if (spiritTriggeredIt)
                {
                    SpiritScript.singleton.gameObject.GetComponent<InteractScript>().canInteract = true;
                    SpiritScript.singleton.isMoveable = true;
                }
                else
                {
                    RogerScript.singleton.gameObject.GetComponent<InteractScript>().canInteract = true;
                    RogerScript.singleton.isMoveable = true;
                }
            }
        }
    }
}
