using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    Score puntos;
    Movimiento NLazers;
    private void Start()
    {
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
        NLazers = GameObject.FindWithTag("Jugador").GetComponent<Movimiento>();
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
        if (col.gameObject.tag != "Proyectil" || NLazers.NumLazers >= 50)
        {
            Destroy(this.gameObject);
        }
        
    }
    
}
