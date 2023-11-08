using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;
using TMPro;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    public BuildingPanelUI BuildPanel;
    
    public Slider FomeSlider;
    public Slider EnergiaSlider;
    public TextMeshProUGUI FomeTextMeshPro;
    public TextMeshProUGUI EnergiaTextMeshPro;
    //private PlayerManager playerManager;

    public GameObject menu;
    public CinemachineBrain cinemachineBrain; // Referência para o CinemachineBrain
    
    
    
    public GameObject BuildP;
    public GameObject TaskPanel;
    public GameObject StatusPanel;

    private void Start()
    {
        //playerManager = FindObjectOfType<PlayerManager>();
       // SetMouseCursorState(BuildPanel.gameObject.activeInHierarchy);
        if(BuildPanel == null) return;
        BuildPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(PlayerManager.playerManager != null) UpdateUI();
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            menu.SetActive(!menu.activeSelf);
            SetMouseCursorState(menu.activeSelf);
            

            // Ativa ou desativa o CinemachineBrain quando pressionar Tab
            if (cinemachineBrain != null)
            {
                cinemachineBrain.enabled = !menu.gameObject.activeInHierarchy;
            }
        }
    }
    
    private void UpdateUI()
    {
        FomeSlider.value = PlayerManager.playerManager.fome / PlayerManager.playerManager.maxfome;
        EnergiaSlider.value = PlayerManager.playerManager.energia / 100f; // Supondo que a energia máxima é 100.

        // Atualiza os TextMeshPro de Porcentagem.
        FomeTextMeshPro.text = Mathf.RoundToInt(PlayerManager.playerManager.fome) + "%";
        EnergiaTextMeshPro.text = Mathf.RoundToInt(PlayerManager.playerManager.energia) + "%";
    }

    private void SetMouseCursorState(bool newState)
    {
        Cursor.visible = newState;
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
    
    
    
    // Método para ativar o painel de construção (Build)
    public void AtivarBuildPanel()
    {
        
        TaskPanel.SetActive(false);
        StatusPanel.SetActive(false);
        BuildPanel.gameObject.SetActive(!BuildPanel.gameObject.activeInHierarchy); 
        if (BuildPanel.gameObject.activeInHierarchy) BuildPanel.PopulateButtons();
        
    }

    // Método para ativar o painel de tarefas (Task)
    public void AtivarTaskPanel()
    {
        BuildPanel.gameObject.SetActive(false);
        TaskPanel.SetActive(true);
        StatusPanel.SetActive(false);
    }

    // Método para ativar o painel de status (Status)
    public void AtivarStatusPanel()
    {
        BuildPanel.gameObject.SetActive(false);
        TaskPanel.SetActive(false);
        StatusPanel.SetActive(true);
    }
}
