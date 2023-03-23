using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    Vector3 posicioncamara;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = GameControl.instance.posicionjugador;
    }


}
