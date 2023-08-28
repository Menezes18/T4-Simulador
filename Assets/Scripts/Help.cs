using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Help : MonoBehaviour
{
 private bool isOpen = false;
 //public GameObject help;
 public int teste = 0;

    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            teste++;
            if (teste >= 1)
            {
            Debug.Log("apertei");
                
            }

            teste = 0;
        }
    }
}
            // if (isOpen)
            // {
            //     help.SetActive(false);
            //     Debug.Log("Fechou");
            //     isOpen = false;
            // }
            // else
            // {
            //     help.SetActive(true);
            //     Debug.Log("Abriu");
            //     isOpen = true;
            // }
