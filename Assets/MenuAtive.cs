using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuAtive : MonoBehaviour
{
    public static MenuAtive instancia;
    public GameObject menu;


    public void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
        }
    }

    void Start()
    {
        
        // Inicia o jogo com o cursor desativado
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        // Inverte o estado do menu
        menu.SetActive(!menu.activeSelf);

        // Ativa ou desativa o cursor com base no estado do menu
        if (menu.activeSelf)
        {
            HotbarDisplay.Display.menu = false;
            FirstPersonController.instancia.cameraMovementEnabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            HotbarDisplay.Display.menu = true;
            FirstPersonController.instancia.cameraMovementEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}