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

    Score puntos;
    void Start()
    {
        puntos = GameObject.FindWithTag("Manager").GetComponent<Score>();
        Time.timeScale = 1;
        StartCoroutine("SpawneoMart");
        StartCoroutine("SpawneoAst");
        StartCoroutine("sumascoresegundos");
        gameOver.SetActive(false);
    }

    IEnumerator SpawneoAst()
    {
        while (true)
        {
            int indice = Random.Range(0, spawners.Length);
            angulo.eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
            Instantiate(enemigoAst, spawners[indice].position, angulo);
            yield return new WaitForSeconds(0.8f);
        }
    }
    IEnumerator SpawneoMart()
    {
        while (true)
        {
            int indice = Random.Range(0, spawners.Length);
            Instantiate(enemigoMart, spawners[indice].position, Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator sumascoresegundos()
    {
        while (true)
        {
            puntos.supervivencia();
            yield return new WaitForSeconds(1);
        }
    }
}

//https://low-scope.com/unity-quick-the-most-common-ways-to-move-a-object/#:~:text=The%20quickest%20way%20to%20move,position%20is%20to%20call%20transform.
//https://angelsolares.wordpress.com/2014/08/12/esperando-por-n-segundos-en-unity3d/