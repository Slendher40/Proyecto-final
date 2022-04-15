using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("timer");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Jugador" || col.gameObject.tag == "Edge")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator timer()
    {
        int contador = 0;
        while (true)
        {
            contador++;
            if (contador >= 30)
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
