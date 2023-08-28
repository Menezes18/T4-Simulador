using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float fome = 100f;
    public const float maxfome = 100f;
    public const float taxaDecaimentoFome = 0.16f;
    public float energia = 100f;

    public event Action<float> OnFomeChanged;   // Evento para a fome
    public event Action<float> OnEnergiaChanged; // Evento para a energia

    private void Update()
    {
        DescerFome();
    }

    public void DescerFome()
    {
        fome -= taxaDecaimentoFome * Time.deltaTime;
        fome = Mathf.Clamp(fome, 0f, maxfome);
        OnFomeChanged?.Invoke(fome); // Chama o evento quando a fome é alterada
    }

    public void DescerEnergia(float perder)
    {
        energia -= perder;
        OnEnergiaChanged?.Invoke(energia); // Chama o evento quando a energia é alterada
    }
}