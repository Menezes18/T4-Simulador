using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.AzureSky;

public enum Estacao
{
    
    Outono,
    Inverno,
    Primavera,
    Verao
}
public class CicloDiaNoite : MonoBehaviour
{
    public static CicloDiaNoite ciclo;
    [SerializeField] private Transform luzDirecional;
    [SerializeField] [Tooltip("Duração do dia em segundos")] public int duracaoDoDia = 200;
    [SerializeField] private TextMeshProUGUI horarioText;
    [SerializeField] private TextMeshProUGUI estadoText;
    [SerializeField] private TextMeshProUGUI anoText;
    [SerializeField] private int diaAtual = 1;
    [SerializeField] public Estacao estacaoAtual = Estacao.Primavera;
    [Header("Estações")]
    [Header("0 - Primavera, 1 - Verao, 2 - Outono, 3 - Inverno")]
    [SerializeField] private GameObject[] gameObjectsEstacao;
    [SerializeField] private GameObject[] SoleDia;
    public float segundos;
    public float multiplacador;
    private float soma = 86400f;
    public AzureTimeController azureTimeController;
    public float tempoNoiteDia;
    public float tempoEmHoras;
    private void Awake()
    {
        if (ciclo != null && ciclo != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ciclo = this;
        }
        
    }
    
    void Start()
    {
        
        gameObjectsEstacao[0].SetActive(true);
        multiplacador = 86400 / duracaoDoDia;
        diaAtual = 1;
        segundos = 0;
        TimeSpan horarioInicial = TimeSpan.FromHours(6);
        segundos = (float)horarioInicial.TotalSeconds;

    }

    void Update()
    {
        if (estacaoAtual == Estacao.Inverno && diaAtual == 2)
        {
            SnowManager.instancia.isSnowActive = true;
        }
        else if (estacaoAtual == Estacao.Inverno && diaAtual == 13)
        {
            SnowManager.instancia.isSnowActive = false;
            SnowManager.instancia.shouldRevertSnow = true;
        }

        segundos += Time.deltaTime * multiplacador;
       
        if (segundos >= soma)
        {
            segundos = 0;
            SubjectPlant.inst.NotifyPlantaAguaAll(1);
            diaAtual++;   
            SubjectPlant.inst.NotifyPlantaAll(estacaoAtual);
            if (diaAtual == 15)
            {
                
                diaAtual = 1;
                estacaoAtual = (Estacao)(((int)estacaoAtual + 1) % Enum.GetValues(typeof(Estacao)).Length);
                estadoText.text = estacaoAtual.ToString();
                AtualizarEstacao();
                SubjectPlant.inst.NotifyPlantaAll(estacaoAtual);
            }
            

        }
        ProcessarCeu();
        CalcularHorario();
        CalcularAno();

        // Verificar se é noite (entre 18:00 e 05:00) ou dia (entre 06:00 e 17:59)
        TimeSpan horarioAtual = TimeSpan.FromSeconds(segundos);
        TimeSpan inicioNoite = TimeSpan.FromHours(18);
        TimeSpan fimNoite = TimeSpan.FromHours(5);
        TimeSpan inicioDia = TimeSpan.FromHours(6);
        TimeSpan fimDia = TimeSpan.FromHours(17).Add(TimeSpan.FromMinutes(59)); // Adiciona 59 minutos ao fim do dia

        if (horarioAtual >= inicioNoite || horarioAtual <= fimNoite)
        {
            SoleDia[0].SetActive(false);
            SoleDia[1].SetActive(true);
        }
        else if (horarioAtual >= inicioDia && horarioAtual <= fimDia)
        {
            SoleDia[0].SetActive(true);
            SoleDia[1].SetActive(false);
           
        }
        
        if (azureTimeController != null)
        {
             tempoNoiteDia = segundos / soma; // Calcule o tempo no formato esperado pelo AzureTimeController
             tempoEmHoras = tempoNoiteDia * 24f; // Converta para horas (0 a 24)

            // Configurar o tempo no AzureTimeController
            azureTimeController.SetTimeline(tempoEmHoras);
        }

    }

    private void ProcessarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90, 270, segundos / 86400);
        luzDirecional.rotation = Quaternion.Euler(rotacaoX, 0, 0);
    }

    public void AtualizarEstacao()
    {
        int indiceEstacaoAtual = (int)estacaoAtual;

        for (int i = 0; i < 4; i++)
        {
            gameObjectsEstacao[i].SetActive(false);
        }

        gameObjectsEstacao[indiceEstacaoAtual].SetActive(true);
    }

    public void AtivarEstacaoAtual()
    {
       
        Estacao estacaoAtual = (Estacao)this.estacaoAtual;
        gameObjectsEstacao[(int)estacaoAtual].SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i != (int)estacaoAtual)
            {
                gameObjectsEstacao[i].SetActive(false);
            }
        }
    }
    private void CalcularHorario()
    {
        horarioText.text = TimeSpan.FromSeconds(segundos).ToString(@"hh\:mm");
        
    }

    private void CalcularAno()
    {
        anoText.text = "DIA " + " \n" + diaAtual;
    }
}
