using UnityEngine;
using UnityEngine.UI;

public class MostrarFPS : MonoBehaviour
{
    public Text textoFPS;
    public float atualizacaoIntervalo = 0.5f; // Intervalo de atualização em segundos
    private float tempoUltimaAtualizacao;
    private int quadrosDesdeUltimaAtualizacao;
    private float fps;

    void Start()
    {
        if (textoFPS == null)
        {
            Debug.LogError("Texto de FPS não atribuído! Adicione um objeto de texto para exibir o FPS.");
        }

        tempoUltimaAtualizacao = Time.realtimeSinceStartup;
    }

    void Update()
    {
        quadrosDesdeUltimaAtualizacao++;

        float tempoDesdeUltimaAtualizacao = Time.realtimeSinceStartup - tempoUltimaAtualizacao;

        if (tempoDesdeUltimaAtualizacao > atualizacaoIntervalo)
        {
            fps = quadrosDesdeUltimaAtualizacao / tempoDesdeUltimaAtualizacao;
            quadrosDesdeUltimaAtualizacao = 0;
            tempoUltimaAtualizacao = Time.realtimeSinceStartup;

            // Atualizar o texto de FPS
            if (textoFPS != null)
            {
                textoFPS.text = $"FPS: {fps:F2}";
            }
        }
    }
}