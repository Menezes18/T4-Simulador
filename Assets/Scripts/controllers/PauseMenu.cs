using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelTutorial;

   public void PauseButton(InputAction.CallbackContext context)
   {
     panelPause.SetActive(true);
   }

   public void TutorialButton()
   {
    panelTutorial.SetActive(true);
    panelPause.SetActive(false);
   }

   public void FecharTutorial()
   {
    panelPause.SetActive(true);
    panelTutorial.SetActive(false);
   }

    public void Resume()
    {
        panelPause.SetActive(false);

        Time.timeScale = 1f;
    }

   public void BackMenu()
   {
    SceneManager.LoadScene("Menu");
   }
}
