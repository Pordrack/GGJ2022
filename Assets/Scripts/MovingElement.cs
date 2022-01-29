using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElement : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 initialPosition;

    private bool moving;

    public float speed = 2;
    void Start()
    {
        GameObject target = transform.Find("Target").gameObject;
        initialPosition= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        target.SetActive(false);
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
                moving = false;
            }
            else
            {
                transform.position += move;
            }
        }
    }

    public void onPress()
    {
        moving = true;
    }
}
