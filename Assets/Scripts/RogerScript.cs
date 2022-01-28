using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogerScript : MonoBehaviour
{
    public float m_JumpPower = 12f;
    public float baseSpeed;
    private float initialXScale;
    public float controlRate = 5;
    public float turnSpeed = 2;
    private float distToGround;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        initialXScale = transform.localScale.x;
        Physics.gravity = new Vector3(0, -30, 0);
        //float caca=2*gameObject.GetComponent<Collider>().bounds.extents.x;

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
            Jump();

        float speed = baseSpeed;
        if(Input.GetButtonDown("Run") && IsGrounded())
        {
            speed *= 1.5f;
        }

        //Se deplace
        float h = Input.GetAxis("Horizontal"); //On recupere les axes
        Vector2 m_Move = new Vector2(h*speed,0); //Et on en fait un mouvement a appliquer
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        //On envoie le mouvement désiré à l'animateur
        //animator.SetFloat("xSpeed", Mathf.Abs(10 * h));

        //Se retourne
        if (h < 0)
        {
            if (transform.localScale.x > -initialXScale)
            {
                Vector3 change = new Vector3(Time.deltaTime * turnSpeed, 0, 0);
                if(transform.localScale.x-Time.deltaTime*turnSpeed < -initialXScale){
                    change = new Vector3(-initialXScale-transform.localScale.x, 0, 0);
                }
                transform.localScale += change;
            }
        }else
        {
            if (transform.localScale.x < initialXScale)
            {
                Vector3 change = new Vector3(Time.deltaTime * turnSpeed, 0, 0);
                if (transform.localScale.x + Time.deltaTime * turnSpeed > initialXScale)
                {
                    change = new Vector3(initialXScale - transform.localScale.x, 0, 0);
                }
                transform.localScale += change;
            }
        }

        Vector2 cVel = rb.velocity; //On recupere la velocité actuelle, pour pouvoir faire une transition
        float multi = controlRate * Time.deltaTime;
        if (multi > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
            multi = 1;
        //On va rapprocher la velocité actuelle de la vélocité désirée
        rb.velocity = new Vector2(cVel.x + (m_Move.x - cVel.x) * multi, rb.velocity.y);
    }

    bool IsGrounded()
    {
        bool cond1 = Physics2D.Raycast(transform.position, -gameObject.transform.up, distToGround + 0.1f);
        return cond1;
    }

    void Jump()
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, m_JumpPower * 0.75f);
    }
}
