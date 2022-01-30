using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private bool isStarted = false;
    public AudioSource music1;
    public AudioSource music2;

    public float transitionSpeed = 2;

    private AudioSource activeMusic;
    private AudioSource inactiveMusic;

    public float maxVolume=1;
    void Start()
    {
        activeMusic = music1;
        inactiveMusic = music2;

        activeMusic.volume = maxVolume;
        inactiveMusic.volume = 0;

        if (RogerScript.singleton == null)
        {
            Invoke("Start", 0.1f);
            return;
        }
        else
        {
            RogerScript.singleton.swapped.AddListener(onSwap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //On recupere les axes
        float v = Input.GetAxis("Vertical");

        if (!isStarted && (h != 0 || v != 0))
        {
            isStarted = true;
            music1.Play();
            music2.Play();
        }

        if (activeMusic.volume < maxVolume)
        {
            activeMusic.volume += transitionSpeed * Time.deltaTime;
            if (activeMusic.volume > maxVolume)
            {
                activeMusic.volume = maxVolume;
            }
        }

        if (inactiveMusic.volume > 0)
        {
            inactiveMusic.volume -= transitionSpeed * Time.deltaTime;
            if (inactiveMusic.volume < 0)
            {
                inactiveMusic.volume = 0;
            }
        }
    }

    void onSwap()
    {
        AudioSource intermediaire = activeMusic;
        activeMusic = inactiveMusic;
        inactiveMusic = intermediaire;
    }
}
