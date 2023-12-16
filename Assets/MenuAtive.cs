using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuAtive : MonoBehaviour
{
    public GameObject menu;

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

    void ToggleMenu()
    {
        // Inverte o estado do menu
        menu.SetActive(!menu.activeSelf);

        // Ativa ou desativa o cursor com base no estado do menu
        if (menu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}