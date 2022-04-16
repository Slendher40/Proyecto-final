using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public float velocidad;
    public Rigidbody2D pfLazer;
    public Rigidbody2D nave;
    public Transform naveT;
    public Transform punta1;
    public Transform punta2;

    public GameObject gameOver;
    public Text NumEscudos;
    public GameObject CantidadEscudos;
    public int CantEscudo;

    public GameObject berImage;
    public Text berText;

    public int vida;
    private int autoCount = 100;
    public int NumLazers = 0;
    private bool autoStarted = false;
    Vector3 dir;
    float angle;

    Score puntos;
    Spawn spawn;

    void Awake()
    {
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
        spawn = GameObject.FindWithTag("Manager").GetComponent<Spawn>();
        velocidad = 0.0005f;
        vida = 1;
        CantEscudo = 0;
        berImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //https://www.youtube.com/watch?v=Geb_PnF1wOk&t=67s

        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        shoot();
        movimiento();
        NumEscudos.text = CantEscudo.ToString();
        berText.text = ((((100 * autoCount) / 75)-100)*(-1)).ToString();
        if (CantEscudo >= 1)
        {
            CantidadEscudos.SetActive(true);
        }
        else
        {
            CantidadEscudos.SetActive(false);
        }
    }


    private void movimiento() //usando transform.Translate
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            nave.AddForce(naveT.up * velocidad);
            //transform.Translate(Vector3.up * Time.deltaTime * velocidad);
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
            NumLazers += 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bala = Instantiate(pfLazer) as Rigidbody2D;
            bala.transform.position = punta2.position;
            bala.transform.rotation = rotacionLazer;
            bala.AddForce(punta1.up * 1000);
            NumLazers += 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Mar" || col.gameObject.tag == "Enemigo")
        {
            vida -= 1;
            CantEscudo -= 1;
        }
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Escudo")
        {
            vida += 1;
            CantEscudo += 1;
        }
        if(col.gameObject.tag != "Proyectil")
        {
            if (vida == 1 && CantEscudo == 0)
            {
                velocidad = 0.0008f;
                berImage.SetActive(true);
                if (autoStarted == false)
                {
                    autoStarted = true;
                    autoCount = 0;
                    StartCoroutine("autoShoot");
                }
            }
        }
    }

    private void normalidad()
    {
        velocidad = 0.0005f;
        autoCount = 100;
        autoStarted = false;
        berImage.SetActive(false);
    }

    private void OnDestroy()
    {
        gameOver.SetActive(true);
        puntos.puntFIN();
        spawn.dead();
    }

    IEnumerator autoShoot()
    {
        while (autoCount < 75)
        {
            Debug.Log("autoCount: " + autoCount);
            var rotacionLazer = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Rigidbody2D bala = Instantiate(pfLazer);
            bala.transform.position = punta1.position;
            bala.transform.rotation = rotacionLazer;
            bala.AddForce(punta1.up * 1000);

            Rigidbody2D bala2 = Instantiate(pfLazer);
            bala2.transform.position = punta2.position;
            bala2.transform.rotation = rotacionLazer;
            bala2.AddForce(punta1.up * 1000);
            NumLazers += 2;
            autoCount += 1;
            yield return new WaitForSeconds(0.1f);
        }
            normalidad();
            yield return null;

    }
}
