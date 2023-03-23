using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Rigidbody2D RB;
    private Animator animacion;
    public GameObject prefabbala,prefabbala2;
    public Transform puntodefuego;
    public float Direccion;
    public float velocidad;
    float velocidadNF;
    public float fuerza;
    public bool Piso;
    public float rayos;
    public float rayosx;
    public float rayosxy;
    public float rayossensorx , rayossensory;
    public float rayjumpx, rayjumpy, rayjumpposx, rayjumpposy;
    int wantmove;
    public bool suelod, sueloi , paredi, paredd;
    public float timerjump;
    public float jumpcd;
    public bool enR, enL;
    public bool canmove;
    public bool Onblock, firing, canfire;
    public float firetimer;


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        new Vector3();
        velocidadNF = velocidad;
        wantmove = Random.Range(-1, 2);
        while (wantmove == 0)
        {
            wantmove = Random.Range(-1, 2);
        }
        Debug.Log(wantmove);
        Direccion = wantmove;
        canmove = true;
        canfire = true;

    }


    void Update()
    {
        Vector3 punto = puntodefuego.transform.position;
        if (firing)
        {
            firetimer += Time.deltaTime;
        }
        Onblock = GameControl.instance.BloquE;
        if (Onblock && !GameControl.instance.Duck && Piso)
        {
            canmove = false;
            firing = true;
            
            GetComponent<Animator>().SetBool("fire", true) ;
        }
        else
        {
            canmove = true;
        }
        if(firetimer > 0.2 && Direccion == -1 && canfire)
        {
            Instantiate(prefabbala, punto, Quaternion.identity);
            canfire = false;
        }
        if (firetimer > 0.2 && Direccion == 1 && canfire)
        {
            Instantiate(prefabbala2, punto , Quaternion.identity);
            canfire = false;
        }
        if (firetimer > 0.8)
        {
            GetComponent<Animator>().SetBool("fire", false);
        }
        if (firetimer > 1)
        {            
            firetimer = 0;
            firing = false;
            canfire = true;
            
        }





        timerjump += Time.deltaTime;
        if (Physics2D.Raycast(transform.position + new Vector3(-rayossensorx, 4, 0), Vector3.down, rayossensory))
        {
            sueloi = true;
        }
        else
        {
            sueloi = false;
        }
        if (Physics2D.Raycast(transform.position + new Vector3(rayossensorx, 4, 0), Vector3.down, rayossensory))
        {
            suelod = true;
        }
        else
        {
            suelod = false;
        }
        if (Physics2D.Raycast(transform.position + new Vector3(rayjumpposx,rayjumpposy , 0), Vector3.right, rayjumpx))
        {
            paredd = true;
        }
        else { paredd = false; }
        if (Physics2D.Raycast(transform.position + new Vector3(-rayjumpposx, rayjumpposy, 0), Vector3.left, rayjumpx))
        {
            paredi = true;
        }
        else { paredi = false; }
        if (paredd && Direccion == 1 && Piso && timerjump > 0.3 + jumpcd)
        {
            Jump();
            Debug.Log("Enemigo salta derecha");
        }
        if (paredi && Direccion == -1 && Piso && timerjump > 0.3 + jumpcd)
        {
            Jump();
            Debug.Log("Enemigo salta izquierda");
        }
        if (suelod && sueloi && Direccion == 1)
        {
            Direccion = 1;
        }
        else if (suelod && sueloi && Direccion == -1)
        {
            Direccion = -1;
        }
        else if (!suelod && sueloi && Direccion == 1)
        {
            Direccion = -1;
        }
        else if (!suelod && sueloi && Direccion == -1)
        {
            Direccion = -1;
        }
        else if (suelod && !sueloi && Direccion == -1)
        {
            Direccion = 1;
        }
        else if (suelod && !sueloi && Direccion == 1)
        {
            Direccion = 1;
        }
        if (Direccion < 0) transform.localScale = new Vector3(-10, 10, 1);
        else if (Direccion > 0) transform.localScale = new Vector3(10, 10, 1);

        //GetComponent<Animator>().SetBool("corriendo", Direccion != 0.0f && Piso == true);
        //debug rayos abajo
        Debug.DrawRay(transform.position + new Vector3(-0.35f, 0, 0), Vector3.down * rayos, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.35f, 0, 0), Vector3.down * rayos, Color.red);
        // rayos piso para saltar
        if (Physics2D.Raycast(transform.position + new Vector3(-0.35f, 0, 0), Vector3.down, rayos) || (Physics2D.Raycast(transform.position + new Vector3(0.35f, 0, 0), Vector3.down, rayos)))
        {
            Piso = true;
        }

        else
        {
            Piso = false;
        }
        Debug.DrawRay(transform.position + new Vector3(-rayossensorx, 4, 0), Vector3.down * rayossensory, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(rayossensorx, 4, 0), Vector3.down * rayossensory, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(rayjumpposx, rayjumpposy, 0), Vector3.left * rayjumpx, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(rayjumpposx, rayjumpposy, 0), Vector3.right * rayjumpx, Color.blue);

        // animacion aire
        if (Piso == true)
        {
            GetComponent<Animator>().SetBool("saltar", false);
        }
        else { GetComponent<Animator>().SetBool("saltar", true); }


        //rayo derecha
        Debug.DrawRay(transform.position + new Vector3(0, rayosxy, 0), Vector3.right * rayosx, Color.red);
        //rayo izquierda
        Debug.DrawRay(transform.position + new Vector3(0, rayosxy, 0), Vector3.left * rayosx, Color.red);

        if (Input.GetKey(KeyCode.DownArrow) && Piso == true && Direccion == 0.0f)
        {
            Duck();
        }
        if (Direccion != 0.0f)
        {
            //GetComponent<Animator>().SetBool("duck", false);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "agua")
        {
            RB.gravityScale = -1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "agua")
        {
            RB.gravityScale = 4;
        }
    }

    private void FixedUpdate()
    {
        //coll contra muros frena o no frena
        if (Physics2D.Raycast(transform.position + new Vector3(0, rayosxy, 0), Vector3.right, rayosx) && Direccion == 1)
        {
            velocidad = 0;
        }
        else
        {

            velocidad = velocidadNF;

        }
        if (Physics2D.Raycast(transform.position + new Vector3(0, rayosxy, 0), Vector3.left, rayosx) && Direccion == -1)
        {
            velocidad = 0;
        }
        else
        {

            velocidad = velocidadNF;

        }
        if (canmove == true)
        {
            RB.velocity = new Vector2(Direccion * velocidad, RB.velocity.y);
            
        }
        GameControl.instance.pos(transform.position.x, transform.position.y);


    }
    void Jump()
    {
        RB.AddForce(Vector2.up * fuerza);
        //GetComponent<Animator>().SetBool("duck", false);
        timerjump = 0;
    }
    void Duck()
    {
        //GetComponent<Animator>().SetBool("duck", true);

    }



}

