using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;
    public AudioSource mumble;
    public AudioSource door;
    public AudioSource shoot;
    public AudioSource laser;
    public AudioSource boom;
    void Start()
    {
        singleton = this;
    }
}
