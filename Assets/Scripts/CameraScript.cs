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

    // Start is called before the first frame update
    void Start()
    {    
        mainTarget = target1;
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
        float multi = controlRate * Time.deltaTime;
        if (multi > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
            multi = 1;
        //On va rapprocher la velocité actuelle de la vélocité désirée
        transform.position = new Vector3(transform.position.x + (mainTarget.position.x - transform.position.x) * multi, transform.position.y + (mainTarget.position.y - transform.position.y) * multi,transform.position.z);

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
        targetPostProcessingLevel = 1 - targetPostProcessingLevel;
    }
}
