using System;
using System.Collections.Generic;
using UnityEngine;

public enum Estacoes
{

    VERAO,
    OUTONO,
    INVERNO,
    PRIMAVERA
}

public class Clima : MonoBehaviour
{
    public static Clima instance;
    public Estacoes estacaoAtual;
    private List<IPlantaObserver> observadores;
    
    public float intervaloDeMudancaDeEstacao = 60.0f; 
    private float tempoDecorrido = 0.0f;

    public void Awake()
    {
        observadores = new List<IPlantaObserver>();
        instance = this;
    }

    private void Start()
    {
        estacaoAtual = Estacoes.VERAO;
    }

    private void Update()
    {
        
        tempoDecorrido += Time.deltaTime;
        
        if (tempoDecorrido >= intervaloDeMudancaDeEstacao)
        {
            MudarEstacaoAutomaticamente();
            tempoDecorrido = 0.0f;
        }
    }

    public void AddObservador(IPlantaObserver observador)
    {
        observadores.Add(observador);
    }
    private void NotificarObservadores()
    {
        foreach (var observador in observadores)
        {
            observador.Atualizar(this);
        }
    }

    private void MudarEstacaoAutomaticamente()
    {
        int numeroEstacaoAtual = (int)estacaoAtual;
        int numeroProximaEstacao = (numeroEstacaoAtual + 1) % Enum.GetValues(typeof(Estacoes)).Length;
        estacaoAtual = (Estacoes)numeroProximaEstacao;
       NotificarObservadores();
    }

}
