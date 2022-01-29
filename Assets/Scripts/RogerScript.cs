using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RogerScript : MonoBehaviour
{
    public float jumpForce=10;
    public int maxNbrJump=2;
    private int currentNbrJump=10;
    public float baseSpeed;
    public float runMultiplier = 3;
    private float initialXScale;
    public float controlRate = 5;
    public float turnSpeed = 2;
    public bool isMoveable = true;

    private float distToGround;
    public float gravityMultiplier = 2;

    private float gravityForce;

    private Animator animator;

    private FeetScript feets;
    public GameObject spiritForm;

    public UnityEvent swapped;

    public static RogerScript singleton;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
        initialXScale = transform.localScale.x;
        Physics.gravity = new Vector3(0, -30, 0);
        //float caca=2*gameObject.GetComponent<Collider>().bounds.extents.x;

        feets = gameObject.GetComponentInChildren<FeetScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Swap();
        float localControlRate = controlRate;

        float speed = baseSpeed;
        if (!IsGrounded())
        {
            speed *= 0.9f;
            localControlRate *= 0.8f;
        }
        else if (Input.GetButton("Run"))
        {
            speed *= runMultiplier;
        }

        //Se deplace
        float h = 0;
        if (isMoveable)
        {
            JumpFunction(Time.deltaTime);
            h = Input.GetAxis("Horizontal"); //On recupere les axes
        }
        Vector2 m_Move = new Vector2(h * speed, 0); //Et on en fait un mouvement a appliquer
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        //On envoie le mouvement d�sir� � l'animateur
        animator.SetFloat("xSpeed", Mathf.Abs(100 * h));

        //Se retourne (code bien sale mais tkt)
        if (h < 0)
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
        else if (h > 0)
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
        else
        {
            if (transform.localScale.x < 0 && transform.localScale.x>-initialXScale)
            {
                float localTurnSpeed = turnSpeed;
                Vector3 change = new Vector3(-Time.deltaTime * localTurnSpeed, 0, 0);
                if (transform.localScale.x - Time.deltaTime * localTurnSpeed < -initialXScale)
                {
                    change = new Vector3(-initialXScale - transform.localScale.x, 0, 0);
                }
                transform.localScale += change;
            }else if (transform.localScale.x > 0 && transform.localScale.x < initialXScale)
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


        Vector2 cVel = rb.velocity; //On recupere la velocit� actuelle, pour pouvoir faire une transition

        float multi = localControlRate * Time.deltaTime;
        if (multi > 1) //Si les FPS suivent pas, on va changer la velocit� instantennement, pour eviter les d�passements etc.
            multi = 1;
        //On va rapprocher la velocit� actuelle de la v�locit� d�sir�e
        rb.velocity = new Vector2(cVel.x + (m_Move.x - cVel.x) * multi, rb.velocity.y);
    }

    bool IsGrounded()
    {
        //animator.SetBool("IsJumping",false);
        //bool cond1 = Physics2D.Raycast(transform.position, -gameObject.transform.up, distToGround + 0.1f);
        bool cond1 = feets.onGround;
        return cond1;
    }

    void JumpFunction(float dt)
    {
        

        if (IsGrounded())
        {
            currentNbrJump = maxNbrJump;
        }
        else
        {
            if (currentNbrJump > maxNbrJump - 1)
                currentNbrJump = maxNbrJump - 1;
        }

        if (currentNbrJump > 0 && Input.GetButtonDown("Jump"))
        {
            Invoke("removeAJump",0.1f);
            animator.SetBool("IsJumping",true);
            Invoke("dothajump",0.2f);
        }
    }

    void Swap()
    {
        if (Input.GetButtonDown("Swap"))
        {
            swapped.Invoke();
            this.isMoveable = !this.isMoveable;
            if (!this.isMoveable && !spiritForm.activeSelf)
            {
                spiritForm.SetActive(true);
                spiritForm.transform.SetPositionAndRotation(transform.position, spiritForm.transform.rotation);
            }
        }   
    }
    void dothajump(){
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity=new Vector2(rigidbody.velocity.x, jumpForce);
        Invoke("deleteAnimJump",0.2f);

    }
    void deleteAnimJump(){
        animator.SetBool("IsJumping",false);

    }
    void removeAJump()
    {
        currentNbrJump--;
    }
}