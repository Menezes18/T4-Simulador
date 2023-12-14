using System.Collections;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    public Transform waterPrefab; // Prefab representando a água
    public Transform waterSpawnPoint; // Ponto de spawn da água
    public float maxWaterLevel = 5f; // Nível máximo de água no balde

    private float currentWaterLevel = 0f; // Nível atual de água no balde
    
    void FillBucket()
    {
        if (currentWaterLevel < maxWaterLevel)
        {
            float waterToAdd = maxWaterLevel - currentWaterLevel;
            currentWaterLevel = maxWaterLevel;

            //SpawnWater(waterToAdd);
        }
    }

    void EmptyBucket()
    {
        currentWaterLevel = 0f;
        // Remover a água (implementação depende do seu projeto)
    }

    void SpawnWater(float amount)
    {
        // Instanciar o prefab de água no ponto de spawn
        Transform waterInstance = Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
        
        // Ajustar a escala da água com base na quantidade
        waterInstance.localScale = new Vector3(1f, amount, 1f);
    }
}