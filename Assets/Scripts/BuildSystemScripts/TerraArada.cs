using System;
using UnityEngine;

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