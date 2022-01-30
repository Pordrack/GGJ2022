using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElement : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 initialPosition;

    private bool moving;
    public bool forceOn = false;
    public bool flip = true;
    public float turnSpeed = 5.0f;

    private float initialXScale;
    public float speed = 2;
    void Start()
    {
        initialXScale = transform.localScale.x;
        GameObject target = transform.Find("Target").gameObject;
        initialPosition= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        target.SetActive(false);
        if (forceOn)
        {
            moving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //On calcule le mouvement a appliquer pour attendre la position
            Vector3 move = Time.deltaTime * speed*(targetPosition- initialPosition);
            //Si on va dépasser la destination (donc si on l'a atteint) on s'y tp puis on switch
            if((initialPosition.x<targetPosition.x && transform.position.x>targetPosition.x) || (initialPosition.x > targetPosition.x && transform.position.x < targetPosition.x) || (initialPosition.y < targetPosition.y && transform.position.y > targetPosition.y) || (initialPosition.y > targetPosition.y && transform.position.y < targetPosition.y))
            {
                transform.position = targetPosition;
                targetPosition = initialPosition;
                initialPosition = transform.position;
                if (!forceOn)
                {
                    moving = false;
                }
            }
            else
            {
                transform.position += move;
            }

            if (flip)
            {
                //Se retourne (code bien sale mais tkt)
                if (targetPosition.x < initialPosition.x)
                {
                    if (transform.localScale.x > -initialXScale)
                    {
                        float localTurnSpeed = turnSpeed;
                        Vector3 change = new Vector3(-Time.deltaTime * localTurnSpeed, 0, 0);
                        if (transform.localScale.x - Time.deltaTime * localTurnSpeed < -initialXScale)
                        {
                            change = new Vector3(-initialXScale - transform.localScale.x, 0, 0);
                        }
                        transform.localScale += change;
                    }
                }
                else if (targetPosition.x > initialPosition.x)
                {
                    if (transform.localScale.x < initialXScale)
                    {
                        float localTurnSpeed = turnSpeed;
                        Vector3 change = new Vector3(Time.deltaTime * localTurnSpeed, 0, 0);
                        if (transform.localScale.x + Time.deltaTime * localTurnSpeed > initialXScale)
                        {
                            change = new Vector3(initialXScale - transform.localScale.x, 0, 0);
                        }
                        transform.localScale += change;
                    }
                }
            }
        }
    }

    public void onPress()
    {
        moving = true;
    }
}
