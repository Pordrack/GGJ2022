using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    public bool isPlayer;
    private Vector3 respawnCoords;
    private Animator animator;
    public UnityEvent respawned;
    void Start()
    {
        respawnCoords = new Vector3(transform.position.x,transform.position.y,transform.position.z);
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
            Start();
            ParticleSystem.MainModule particles= collision.gameObject.GetComponent<ParticleSystem>().main;
            particles.startColor = new Color(140.0f/255.0f, 30.0f/255.0f, 124.0f/255.0f);
        }
    }

    public void Die()
    {
        transform.SetPositionAndRotation(respawnCoords, transform.rotation);
        animator.Rebind();
        animator.Update(0f);

        if (isPlayer)
        {
            respawned.Invoke();
        }
    }
}
