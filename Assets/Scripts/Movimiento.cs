using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public int velocidad;
    public Rigidbody2D pfLazer;
    public Transform punta1;
    public Transform punta2;

    public GameObject gameOver;

    private int vida;

    Vector3 dir;
    float angle;

    Score puntos;

    void Start()
    {
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
        velocidad = 5;
        vida = 1;


    }

    // Update is called once per frame
    void Update()
    {
        //https://www.youtube.com/watch?v=Geb_PnF1wOk&t=67s

        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        movimiento();
        shoot();   

    }

    private void movimiento() //usando transform.Translate
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Translate(Vector3.up * Time.deltaTime * velocidad);
        }
        
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    public void shoot()
    {
        var rotacionLazer = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bala = Instantiate(pfLazer) as Rigidbody2D;
            bala.transform.position = punta1.position;
            bala.transform.rotation = rotacionLazer;
            bala.AddForce(punta1.up * 1000);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bala = Instantiate(pfLazer) as Rigidbody2D;
            bala.transform.position = punta2.position;
            bala.transform.rotation = rotacionLazer;
            bala.AddForce(punta1.up * 1000);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        vida -= 1;
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameOver.SetActive(true);
        puntos.puntFIN();
    }
}
