using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public Slider Fome;
    public Slider Energia;

    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        
        
        playerStatus.OnFomeChanged += AtualizarSliderFome;
        playerStatus.OnEnergiaChanged += AtualizarSliderEnergia;
    }

    
    private void AtualizarSliderFome(float novoValor)
    {
        Fome.value = novoValor;
    }

    
    private void AtualizarSliderEnergia(float novoValor)
    {
        Energia.value = playerStatus.energia; // Aqui estava usando maxfome em vez de maxenergia
    }

    private void Update()
    {
        
    }

    private void SetMouseCursorState(bool newState)
    {
        Cursor.visible = newState;
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}