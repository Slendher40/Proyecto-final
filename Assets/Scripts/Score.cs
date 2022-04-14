using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=5D7rxEcaJQ4

public class Score : MonoBehaviour
{
    public Text textoScore;
    public static int puntaje;

    public static int puntajeFinal;

    private string scorePrefsName = "ScoreFinal";


    void Start()
    {
        puntaje = 0;

    }
    void Update()
    {
        textoScore.text = puntaje.ToString();
        //Debug.Log("puntajeFinal:"+puntajeFinal);
    }
    
    public void sumascore(int suma)
    {
        puntaje += suma;
    }

    public void supervivencia()
    {
        puntaje += 1;
    }

    public void puntFIN()
    {
        puntajeFinal = puntaje;
        PlayerPrefs.SetInt(scorePrefsName, puntajeFinal);
    }

   /* private void SaveData()
    {
        PlayerPrefs.SetInt(scorePrefsName, puntajeFinal);
    }

    private void LoadData()
    {
        puntajeFinal = PlayerPrefs.GetInt(scorePrefsName, 0);
    }*/
}
