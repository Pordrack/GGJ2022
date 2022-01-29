using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : MonoBehaviour
{
    public float baseSpeed=20;
    public bool canFly=true;
    public bool isMoveable = true;

    private Collider2D collider;
    private Rigidbody2D rb;
    public float blinkTimer=0.5f;
    public float maxBlinkCooldown=1;
    private float blinkCooldown = 0;

    private GameObject particles;
    private float angle = 0;

    void Start()
    {
        particles = transform.Find("Particles").gameObject;
        collider = gameObject.GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
        Swap();
        float speed = baseSpeed;
        //Se deplace
        float h = 0;
        float v = 0;
        if (isMoveable)
        {
            h = Input.GetAxis("Horizontal"); //On recupere les axes
            if (canFly)
            {
                v = Input.GetAxis("Vertical");
            }
        }
        Debug.Log("h:" + h + "  v:" + v);

        if (h == 0 && v == 0)
        {
            if (!isMoveable)
            {
                angle = Mathf.PI / 2;
            }
            speed = 0;
        }
        else
        {
            angle = Mathf.Atan2(v, h);
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                speed *= Mathf.Abs(h);
            }
            else
            {
                speed *= Mathf.Abs(v);
            }
        }
        //On rotate les particles en fonction
        //On recupere la rotation actuelle
        float rotation = Mathf.Deg2Rad*particles.transform.eulerAngles.z;
        float multi = 6 * Time.deltaTime;
        if (multi > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
            multi = 1;

        float diff = Mathf.Rad2Deg*(((angle) - rotation)*multi);
        diff = multi * Mathf.Rad2Deg * Mathf.Atan2(Mathf.Sin(angle - rotation), Mathf.Cos(angle - rotation));

        particles.transform.Rotate(new Vector3(0,0,diff));

        Vector2 m_Move = new Vector2(speed*Mathf.Cos(angle),speed*Mathf.Sin(angle)); //Et on en fait un mouvement a appliquer
        
        Vector2 cVel = rb.velocity; //On recupere la velocité actuelle, pour pouvoir faire une transition

        float multi2 = 5 * Time.deltaTime;
        if (multi2 > 1) //Si les FPS suivent pas, on va changer la velocité instantennement, pour eviter les dépassements etc.
            multi2 = 1;
        //On va rapprocher la velocité actuelle de la vélocité désirée
        rb.velocity = new Vector2(cVel.x + (m_Move.x - cVel.x) * multi, rb.velocity.y);
    }

    void Swap()
    {
        this.isMoveable = !this.isMoveable;
    }

    void Blink()
    {
        if (!this.canFly)
        {
            blinkCooldown -= Time.deltaTime;
            if(Input.GetButtonDown("Jump") && blinkCooldown < 0)
            {
                blinkCooldown = maxBlinkCooldown;
                collider.isTrigger = true;
                rb.AddForce(new Vector2(baseSpeed * Mathf.Cos(angle), baseSpeed * Mathf.Sin(angle)));
                Invoke("Unblink", blinkTimer);
            }     
        }
    }

    void Unblink()
    {
        if (!this.canFly)
        {
            collider.isTrigger = false;
        }
    }
}
