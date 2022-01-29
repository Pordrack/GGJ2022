using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogerScript : MonoBehaviour
{
    public float jumpSpeed;
    public float maxJumpHeight;
    public float baseSpeed;
    public float runMultiplier=3;
    private float initialXScale;
    public float controlRate = 5;
    public float turnSpeed = 2;

    private float distToGround;
    public float gravityMultiplier=2;

    private float gravityForce;

    private bool wasStatic = true;

    private Animator animator;

    private float jumpTimer;

    private FeetScript feets;

    private Vector3 velocity;

    private CharacterController2D characterController;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        initialXScale = transform.localScale.x;
        Physics.gravity = new Vector3(0, -30, 0);

        feets = gameObject.GetComponentInChildren<FeetScript>();
        animator = gameObject.GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpFunction(Time.deltaTime);

        float speed = baseSpeed;
        if(Input.GetButton("Run"))
        {
            speed *= runMultiplier;
        }

        //Se deplace
        float h = Input.GetAxis("Horizontal"); //On recupere les axes
        Vector2 m_Move = new Vector2(h*speed,0); //Et on en fait un mouvement a appliquer
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        //On envoie le mouvement désiré à l'animateur
        animator.SetFloat("xSpeed", Mathf.Abs(100 * h));


        //Se retourne
        if (h < 0)
        {
            if (transform.localScale.x > -initialXScale)
            {
                float localTurnSpeed = turnSpeed;
                if (wasStatic)
                {
                    localTurnSpeed = 1000;
                }
                Vector3 change = new Vector3(-Time.deltaTime * localTurnSpeed, 0, 0);
                if(transform.localScale.x-Time.deltaTime*localTurnSpeed < -initialXScale){
                    change = new Vector3(-initialXScale-transform.localScale.x, 0, 0);
                }
                transform.localScale += change;
                wasStatic = false;
            }
        }else if(h>0)
        {
            if (transform.localScale.x < initialXScale)
            {
                float localTurnSpeed = turnSpeed;
                if (wasStatic)
                {
                    localTurnSpeed = 1000;
                }
                Vector3 change = new Vector3(Time.deltaTime * localTurnSpeed, 0, 0);
                if (transform.localScale.x + Time.deltaTime * localTurnSpeed > initialXScale)
                {
                    change = new Vector3(initialXScale - transform.localScale.x, 0, 0);
                }
                transform.localScale += change;
                wasStatic = false;
            }
        }
        else
        {
            wasStatic = true;
        }

        float multi = controlRate * Time.deltaTime;
        if (multi > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
            multi = 1;
        //On va rapprocher la velocité actuelle de la vélocité désirée
        velocity.x =  ((h * speed) - velocity.x) * multi;

        characterController.move(velocity*Time.deltaTime);
    }

    void JumpFunction(float dt)
    {
        if (characterController.isGrounded)
        {
            jumpTimer = maxJumpHeight / jumpSpeed;
            velocity.y = 0;
            gravityForce = 0;
        }
        else
        {
            if (!Input.GetButton("Jump"))
            {
                jumpTimer = 0;
            }
            velocity.y += dt * gravityMultiplier * Physics2D.gravity.y;
        }


        if (jumpTimer > 0 && Input.GetButton("Jump"))
        {
            velocity.y= jumpSpeed;
            jumpTimer -= dt;
        }
    }
}
