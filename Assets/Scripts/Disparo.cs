using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    Score puntos;
    private void Start()
    {
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemigo")
        {
            puntos.sumascore(2);
        }
        if (col.gameObject.tag == "Mar")
        {
            puntos.sumascore(5);
        }

        Destroy(this.gameObject);
    }
    
}
