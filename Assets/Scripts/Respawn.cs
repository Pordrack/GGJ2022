using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    public bool isPlayer;
    private Vector3 respawnCoords;
    public UnityEvent respawned;
    private GameObject ourCharacter;

    void Start()
    {
        Debug.Log(gameObject.name);
        Debug.Log("Ta mère la pute");
        ourCharacter = gameObject;
        respawnCoords = new Vector3(ourCharacter.transform.position.x, ourCharacter.transform.position.y, ourCharacter.transform.position.z);
        if (!isPlayer)
        {
            if (RogerScript.singleton == null)
            {
                Invoke("Start", 0.1f);
                return;
            }
            else
            {
                RogerScript.singleton.GetComponent<Respawn>().respawned.AddListener(Die);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint" && isPlayer)
        {
            respawnCoords = new Vector3(ourCharacter.transform.position.x, ourCharacter.transform.position.y, ourCharacter.transform.position.z);
            ParticleSystem.MainModule particles= collision.gameObject.GetComponent<ParticleSystem>().main;
            particles.startColor = new Color(140.0f/255.0f, 30.0f/255.0f, 124.0f/255.0f);
        }
    }

    public void Die()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        Debug.Log(transform.gameObject.name);
        Debug.Log(ourCharacter.transform.position);
        ourCharacter.transform.SetPositionAndRotation(respawnCoords, ourCharacter.transform.rotation);
        animator.Rebind();

        if (isPlayer)
        {
            respawned.Invoke();
        }
    }
}
