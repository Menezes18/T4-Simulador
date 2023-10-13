using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class TerraArada : MonoBehaviour
{

    public int arrarCount = 0;
    public enum TipoDeTextura
    {
        TerraNormal,
        TerraPronta,
        TerraArada,
        TerraComAgua,
    }

    public TipoDeTextura tipoDeTextura;

    public Material materialTerraArada;
    public Material materialTerraNormal;
    public Material materialTerraPronta;

    private Renderer objetoRenderer;
    public Material novoMaterial;
    public GameObject sementePrefab;
    public int capacidadeMaxima = 10;
    private List<GameObject> sementesPlantadas = new List<GameObject>();

    public void PlantarSementes(int quantidade, RaycastHit hit, InventoryItemData item)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("terraArada"))
        {
            sementePrefab = item.ItemPrefab;
                
             if (sementePrefab != null)
             {
                 Collider collider = hit.collider;
                 Bounds bounds = collider.bounds;
                 
                 for (int i = 0; i < quantidade; i++)
                 {
                     if (sementesPlantadas.Count >= capacidadeMaxima)
                     {
                         Debug.LogWarning("Capacidade máxima de sementes atingida.");
                         return;
                     }
            
                     Vector3 randomPosition = GetRandomPositionWithinCollider(bounds);
                     GameObject semente = Instantiate(sementePrefab, randomPosition, Quaternion.identity);
                     sementesPlantadas.Add(semente);
                 }
            }
             else
             {
                 Debug.LogWarning("Prefab de semente não definido na TerraArada.");
             }
        }
    }

    private Vector3 GetRandomPositionWithinCollider(Bounds bounds)
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y, // You can modify this to control the height of sementes
            Random.Range(bounds.min.z, bounds.max.z)
        );

        return randomPosition;
    }
    private void Start()
    {
        objetoRenderer = GetComponent<Renderer>();
        AtualizarTextura();
    }

    public void Update()
    {
        AtualizarTextura();
    }

    public void TexturaEdit(Material p1, Material p2, Material p3)
    {
        materialTerraArada = p1;
        materialTerraNormal = p2;
        materialTerraPronta = p3;
    }
    private void AtualizarTextura()
    {
         novoMaterial = null;

        switch (tipoDeTextura)
        {
            case TipoDeTextura.TerraArada:
                novoMaterial = materialTerraArada;
                gameObject.layer = LayerMask.NameToLayer("terraArada");
                break;
            case TipoDeTextura.TerraPronta:
                novoMaterial = materialTerraPronta;
                break;
            case TipoDeTextura.TerraComAgua:
                novoMaterial = materialTerraNormal;
                break;
        }

        if (novoMaterial != null)
        {
            objetoRenderer.material = novoMaterial;
        }
    }

    public void ArarTerra()
    {
        
        arrarCount++;
        if (arrarCount.Equals(5))
        {
            tipoDeTextura = TipoDeTextura.TerraArada; 
        }
    }
}