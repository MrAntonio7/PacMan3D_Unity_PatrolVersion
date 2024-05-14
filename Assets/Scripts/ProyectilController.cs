using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el objeto con el que colisionamos está en la capa "Suelo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Destroy(gameObject);  // Destruye el objeto proyectil
        }
        if (collision.gameObject.CompareTag("Player"))
        {
           
        }
    }
}
