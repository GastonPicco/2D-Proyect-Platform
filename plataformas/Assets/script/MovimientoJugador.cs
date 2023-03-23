using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D RB;
    private BoxCollider2D C2;
    private Animator animacion;
    public float Direccion;
    public float velocidad;
    float velocidadNF;
    public float fuerza;
    public bool Piso;
    public float rayos;
    public float rayosx;
    bool duck,bloquE = false;
    
    
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        new Vector3();
        velocidadNF = velocidad;
        C2 = GetComponent<BoxCollider2D>();
        C2.size = new Vector2(0.068f, 0.17f);
        C2.offset = new Vector2(0, -0.009f);

    }

    
    void Update()
    {
        if(transform.position.y < -20)
        {
            gameObject.transform.position = new Vector3(-7.94f, 1.5f, transform.position.z);
        }
        
        Direccion = Input.GetAxisRaw("Horizontal");
        
        GameControl.instance.OnBock(bloquE);
        
        if (Direccion < 0) transform.localScale = new Vector3(-10, 10, 1);
        else if (Direccion > 0) transform.localScale = new Vector3(10, 10, 1);

        animacion.SetBool("corriendo", Direccion != 0.0f && Piso == true);
        if (Direccion != 0)
        {
            C2.size = new Vector2(0.068f, 0.17f);
            C2.offset = new Vector2(0, -0.009f);
        }
        Debug.DrawRay(transform.position + new Vector3(-0.35f, 0, 0), Vector3.down * rayos, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.35f, 0, 0), Vector3.down * rayos, Color.red);
        //Debug.DrawRay(transform.position,new Vector3 (0.4f, rayos*-1, 0), Color.red);
        //Debug.DrawRay(transform.position, new Vector3(-0.4f, rayos * -1, 0), Color.red);
        //|| Physics2D.Raycast(transform.position, new Vector2(-0.4f, rayos * -1), rayos * 1.1f)|| Physics2D.Raycast(transform.position, new Vector2(0.4f, rayos * -1), rayos * 1.1f))
        if (Physics2D.Raycast(transform.position + new Vector3(-0.35f, 0, 0), Vector3.down, rayos) || (Physics2D.Raycast(transform.position + new Vector3(0.35f, 0, 0), Vector3.down, rayos)))
        {
            Piso = true;
        }
        else
        {
            Piso = false;
        }
        if (Piso == true)
        {
            animacion.SetBool("saltar", false);
        }
        else { animacion.SetBool("saltar", true);}

       

        Debug.DrawRay(transform.position + new Vector3(0,-0.9f,0), Vector3.right * rayosx, Color.red);
        
        Debug.DrawRay(transform.position + new Vector3(0, -0.9f, 0), Vector3.left * rayosx, Color.red);
        

        if (Input.GetKeyDown(KeyCode.W) && Piso == true)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Piso == true)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.DownArrow) && Piso == true && Direccion == 0.0f)
        {
            Duck();
            C2.size = new Vector2(0.068f,0.08f);
            C2.offset = new Vector2(0, -0.055f);
        }
        if(Direccion != 0.0f)
        {
            animacion.SetBool("duck", false);
            duck = false;
        }
        GameControl.instance.DucK(duck);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "agua")
        {
            RB.gravityScale =-1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "agua")
        {
            RB.gravityScale = 4;
            
        }
        if (collision.tag == "bloque")
        {
            bloquE = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "bloque")
        {
            bloquE = true;
        }
        if (collision.tag == "bala")
        {
            gameObject.transform.position = new Vector3(-7.94f, 1.5f, transform.position.z);
        }
        if (collision.tag == "Finish")
        {
            gameObject.transform.position = new Vector3(-7.94f, 1.5f, transform.position.z);
        }


    }

    private void FixedUpdate()
    {
        
        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.9f, 0), Vector3.right, rayosx) && Direccion == 1)
        {
            //gameObject.transform.position = new Vector3(transform.position.x - velocidadNF, transform.position.y, 0.0f);            
            velocidad = 0;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(0, -0.9f, 0), Vector3.right, rayosx) && Direccion != 1)
        {
            velocidad = velocidadNF;
        }
        else if (Physics2D.Raycast(transform.position + new Vector3(0, -0.9f, 0), Vector3.left, rayosx) && Direccion < 0)
        {
            //gameObject.transform.position = new Vector3(transform.position.x + velocidadNF, transform.position.y, 0.0f);
            velocidad = 0;
        }
        else
        {
            velocidad = velocidadNF;
        }
        RB.velocity = new Vector2(Direccion * velocidad, RB.velocity.y);
        
        GameControl.instance.pos(transform.position.x,transform.position.y);

    }
    void Jump()
    {
        RB.AddForce(Vector2.up * fuerza);
        animacion.SetBool("duck", false);
        duck = false;
        C2.size = new Vector2(0.068f, 0.19f);
        C2.offset = new Vector2(0, 0);

    }
    void Duck()
    {
        animacion.SetBool("duck", true);
        duck = true;

    }

}
