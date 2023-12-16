using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    public static SnowManager instancia;
    [SerializeField] private Material[] materials;
    [SerializeField] private float targetSnowAmount = 1.0f;
    [SerializeField] private float snowAccumulationTime = 30.0f; // Tempo desejado para acumular a neve até o valor alvo
    [SerializeField] private float currentSnowAmount = 0.0f;
    [SerializeField] public bool isSnowActive = false;
    [SerializeField] public bool shouldRevertSnow = false;

    [SerializeField] public GameObject Snow;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
        }
        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("_SnowAmount", 0.0f);
        }
    }

    private void Update()
    {
        if (shouldRevertSnow)
        {
            RevertSnow();
        }
        else
        {
            // Verifica se a neve está ativa antes de atualizar
            if (isSnowActive)
            {
                if (currentSnowAmount > 0.3f)
                {
                    Snow.SetActive(true);
                }
                // Incrementa a quantidade de neve com base no tempo desejado
                currentSnowAmount = Mathf.Lerp(currentSnowAmount, targetSnowAmount, Time.deltaTime / snowAccumulationTime);

                // Garante que a quantidade de neve permaneça no intervalo desejado (0 a 1)
                currentSnowAmount = Mathf.Clamp01(currentSnowAmount);
            }
            else
            {
                
                // Se a neve não estiver ativa, define a quantidade de neve como zero
               // currentSnowAmount = 0.0f;
            }

            for (int i = 0; i < materials.Length; ++i)
            {
                materials[i].SetFloat("_SnowAmount", currentSnowAmount);
            }
        }
    }

    // Método para ativar ou desativar a neve
    public void SetSnowActive(bool isActive)
    {
        isSnowActive = isActive;
        shouldRevertSnow = false; // Garante que a reverter neve seja desativada quando a neve é ativada
    }

    // Método para reverter a quantidade de neve de 1 para 0
    public void RevertSnow()
    {
        Snow.SetActive(false);
        currentSnowAmount = Mathf.Lerp(currentSnowAmount, 0.0f, Time.deltaTime / 40);

        if (currentSnowAmount <= 0.01f)
        {
            currentSnowAmount = 0.0f;
            shouldRevertSnow = false; // Desativa a flag de reverter neve quando a reversão é concluída
        }

        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("_SnowAmount", currentSnowAmount);
        }
    }
}
