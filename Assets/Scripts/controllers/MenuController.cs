using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
     public string nomeDoLevelDeJogo;
     public GameObject panelMenuInicial;
     public GameObject panelOptions;
     public GameObject panelTutorial;
     public GameObject panelCredits;

     public void Start()
     {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
     }

     public void NewGame()
    {
        SceneManager.LoadScene("Game");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions()
    {
        panelMenuInicial.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void OpenTutorial()
        {
            panelMenuInicial.SetActive(false);
            panelTutorial.SetActive(true);
        }
    
    public void CloseOptions()
    {
        panelOptions.SetActive(false);
        panelMenuInicial.SetActive(true);
        

    }

    public void OpenCredits()
    {
        panelMenuInicial.SetActive(false);
        panelCredits.SetActive(true);

    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
        panelMenuInicial.SetActive(true);
    }

    
    public void QuitGame()
    {
        Debug.Log("Fechou o Jogo");
        Application.Quit();
    }

}
