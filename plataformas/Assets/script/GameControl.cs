using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public Vector3 posicionjugador;
    public bool Duck;
    public bool BloquE;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pos(float PoSx,float PoSy)
    {
        posicionjugador = new Vector3(PoSx+3,PoSy/4+1.5f,-10);
    }
    public void DucK(bool duck)
    {
        Duck = duck;
    }
    public void OnBock(bool onblock)
    {
        BloquE = onblock; 
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("!Quit");
    }
}
