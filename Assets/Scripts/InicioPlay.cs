using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioPlay : MonoBehaviour
{
    public GameObject pausa;
    public GameObject menuPausa;
    public void inicio()
    {
        SceneManager.LoadScene("Ingame", LoadSceneMode.Single);
    }

    public void reseteo()
    {
        SceneManager.LoadScene("Menu-Inicio", LoadSceneMode.Single);
    }

    public void salir()
    {
        Application.Quit();
    }

    private void Update()
    {
        //https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/#what_gets_paused
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausa.SetActive(false);
            menuPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausa.SetActive(true);
            menuPausa.SetActive(false);
        }
    }
}
