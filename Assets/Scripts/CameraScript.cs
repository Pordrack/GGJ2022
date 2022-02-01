using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    private Transform mainTarget;

    private float targetPostProcessingLevel=0;
    public float ppTransitionSpeed = 2;
    public PostProcessVolume postProcessing;
    public float controlRate=5;
    private bool lockedOnTarget = false;

    public float minX = -50;
    public float minY = -30;

    // Start is called before the first frame update
    void Start()
    {    
        mainTarget = target1;
        lockedOnTarget = true;
        transform.position = new Vector3(mainTarget.position.x,mainTarget.position.y,transform.position.z);
        if (RogerScript.singleton == null)
        {
            Invoke("Start", 0.1f);
            return;
        }
        RogerScript.singleton.swapped.AddListener(Swap);
    }

    private void Update()
    {
        //On bouge vers la target actuelle
        if (lockedOnTarget) //TP si on est deja sur notre cible
        {
            transform.position = new Vector3(mainTarget.transform.position.x,mainTarget.transform.position.y,transform.position.z);
        }
        else //Mouvement fluide pour passer d'une cible a l'autre
        {
            float multi = controlRate * Time.deltaTime;
            if (multi > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
                multi = 1;
            Vector2 vectorToApply = new Vector2(mainTarget.position.x-transform.position.x,mainTarget.position.y-transform.position.y);
            if (Mathf.Abs(vectorToApply.x) + Mathf.Abs(vectorToApply.y) < 1)
            {
                lockedOnTarget = true;
            }
            //On va rapprocher la velocité actuelle de la vélocité désirée
            transform.position = new Vector3(transform.position.x + vectorToApply.x * multi, transform.position.y + vectorToApply.y * multi, transform.position.z);
        }

        //On change l'intensite du postProcessing
        float diff = Time.deltaTime * ppTransitionSpeed;
        if (postProcessing.weight > targetPostProcessingLevel)
        {
            if (postProcessing.weight - diff < targetPostProcessingLevel)
            {
                postProcessing.weight = targetPostProcessingLevel;
            }
            else
            {
                postProcessing.weight -= diff;
            }
        }else if (postProcessing.weight < targetPostProcessingLevel)
        {
            if (postProcessing.weight + diff > targetPostProcessingLevel)
            {
                postProcessing.weight = targetPostProcessingLevel;
            }
            else
            {
                postProcessing.weight += diff;
            }
        }

        if (transform.position.x < minX)
        {
            transform.SetPositionAndRotation(new Vector3(minX, transform.position.y, transform.position.z), transform.rotation);
        }

        if (transform.position.y < minY)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, minY, transform.position.z), transform.rotation);
        }
    }
    void Swap()
    {

            if (mainTarget == target1)
            {
                mainTarget = target2;
            }
            else
            {
                mainTarget = target1;
            }
        lockedOnTarget = false;
        targetPostProcessingLevel = 1 - targetPostProcessingLevel;
    }
}
