using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    public static MenuScript inst;
    public GameObject menuObject;
    private bool isMenuActive = false;

    public GameObject tutorialPanel;
    public GameObject ajudaPanel;
    

    public Interactor interactor;

    public void Awake()
    {
        inst = this;
    }
    
    void Update()
    {
        // Verifica se a tecla Tab foi pressionada para ativar/desativar o menu
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            menusair();
        }

        // Verifica se a tecla J foi pressionada para mostrar o cursor
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // Método para abrir o menu
    public void OpenMenu()
    {
        isMenuActive = true;

        // Ativa o GameObject do menu
        menuObject.SetActive(true);

        // Ativa o cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Método para fechar o menu
    public void CloseMenu()
    {
        isMenuActive = false;

        // Desativa o GameObject do menu
        menuObject.SetActive(false);

        // Desativa o cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Método para verificar se o menu está ativo
    public bool IsMenuActive()
    {
        return isMenuActive;
    }
    public void menusair()
    {
        isMenuActive = !isMenuActive;

        // Ativa/desativa o GameObject do menu
        menuObject.SetActive(isMenuActive);

        // Ativa/desativa o cursor com base no estado do menu
        Cursor.visible = isMenuActive;
        Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void AtivarTutorial()
    {
        // Ativa o painel de tutorial
        tutorialPanel.SetActive(true);
    }
    
    public void DesativarTutorial()
    {
        // Desativa o painel de tutorial
        tutorialPanel.SetActive(false);
    }

    public void AtivarAjuda()
    {
        // Ativa o painel de ajuda
        ajudaPanel.SetActive(true);
    }

    public void DesativarAjuda()
    {
        // Desativa o painel de ajuda
        ajudaPanel.SetActive(false);
    }
    
    
}