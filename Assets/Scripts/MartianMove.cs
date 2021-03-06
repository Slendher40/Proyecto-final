using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartianMove : MonoBehaviour
{
    public GameObject jugador;
    private Rigidbody2D rb;
    private int vida;

    public Transform shieldSpawn;

    private int NumRand;
    private float angle;
    public GameObject Escudo;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        vida = 10;
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        NumRand = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        // https://www.youtube.com/watch?v=4Wh22ynlLyk

        Vector3 direccion = jugador.transform.position - transform.position;
        angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        rb.rotation = angle-90;
        rb.AddForce(shieldSpawn.up*20);
        //transform.Translate(Vector3.up * Time.deltaTime * 1.5f);
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

    private void OnDestroy()
    {
        if (NumRand == 2)
        {
            Instantiate(Escudo, shieldSpawn.position, Quaternion.identity);
        }
    }
}
