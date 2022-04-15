using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject enemigoAst;
    public GameObject enemigoMart;
    
    private Quaternion angulo;
    public GameObject gameOver;
    private float dificultad = 3;

    Score puntos;
    void Awake()
    {
        dificultad = 3;
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
        Time.timeScale = 1;
        StartCoroutine("spawn");
        gameOver.SetActive(false);
    }

    IEnumerator spawn()
    {
        StartCoroutine("SpawneoMart");
        StartCoroutine("SpawneoAst");
        StartCoroutine("dific");
        yield return null;
    }

    IEnumerator stopSpawn()
    {
        StopCoroutine("SpawneoMart");
        StopCoroutine("SpawneoAst");
        StopCoroutine("dific");
        yield return null;
    }

    IEnumerator SpawneoAst()
    {
        while (true)
        {
            int indice = Random.Range(0, spawners.Length);
            angulo.eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
            Instantiate(enemigoAst, spawners[indice].position, angulo);
            yield return new WaitForSecondsRealtime(dificultad);
        }
    }
    IEnumerator SpawneoMart()
    {
        while (true)
        {
            int indice = Random.Range(0, spawners.Length);
            Instantiate(enemigoMart, spawners[indice].position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(dificultad*3);
        }
    }
    IEnumerator dific()
    {
        while (true)
        {
            if (dificultad >= 2)
            {
                dificultad -= 0.05f;
            } else if (dificultad < 2 && dificultad >= 1)
            {
                dificultad -= 0.025f;
            } else if (dificultad < 1 && dificultad >= 0.25)
            {
                dificultad -= 0.005f;
            }else if (dificultad < 0.25)
            {
                dificultad = 0.25f;
                StopCoroutine("dific");
            }
            yield return new WaitForSecondsRealtime(2);
        }
    }
    public void dead()
    {
        StopCoroutine("SpawneoMart");
        StopCoroutine("SpawneoAst");
    }
}

//https://low-scope.com/unity-quick-the-most-common-ways-to-move-a-object/#:~:text=The%20quickest%20way%20to%20move,position%20is%20to%20call%20transform.
//https://angelsolares.wordpress.com/2014/08/12/esperando-por-n-segundos-en-unity3d/