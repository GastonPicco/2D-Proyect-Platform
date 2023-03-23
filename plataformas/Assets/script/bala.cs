using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float direccion, velocidad, timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.position = new Vector3(transform.position.x + direccion * velocidad*Time.deltaTime,transform.position.y,0);
        if (timer > 2.3f)
        {
            Destroy(gameObject);
        }
    }
}
