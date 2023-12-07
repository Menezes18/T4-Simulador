using UnityEngine;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    public GameObject menuObject;
    private bool isMenuActive = false;

    public GameObject tutorialPanel;
    public GameObject ajudaPanel;
    

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla Tab foi pressionada para ativar/desativar o menu
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
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