using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 target = new Vector3(0, 0);
    public float speed;
    public int puntos = 2;
    public GameObject jugador;
    private Rigidbody2D rb;
    private int vida;
    public Transform Asteroide;
    public float velocidad;

    float numran;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Jugador");

        Vector3 direccion = jugador.transform.position - transform.position;
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        numran = Random.Range(-0.8f, 1.5f);
        speed = Random.Range(2.5f, 6);
        cambiovida();

        // https://forum.unity.com/threads/how-to-change-scale-of-a-gameobject-in-run-time.109705/
        rb.transform.localScale += new Vector3(numran, numran, numran);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Asteroide.up * velocidad);
        //transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void cambiovida()
    {
        if(numran <= -0.4)
        {
            vida = 1;
            velocidad = 8-vida;
        }else if (numran <= 0 && numran > -0.4)
        {
            vida = 3;
            velocidad = 10 - vida;
        }
        else if (numran <= 0.5 && numran > 0)
        {
            vida = 5;
            velocidad = 10 - vida;
        }
        else if (numran <= 1 && numran > 0.5)
        {
            vida = 7;
            velocidad = 10 - vida;
        }
        else
        {
            vida = 9;
            velocidad = 10 - vida;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        vida -= 1;
        if (col.gameObject.tag == "Edge")
        {
            Destroy(this.gameObject);
        }
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
 // https://low-scope.com/unity-quick-the-most-common-ways-to-move-a-object/#:~:text=The%20quickest%20way%20to%20move,position%20is%20to%20call%20transform.