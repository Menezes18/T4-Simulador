using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta : MonoBehaviour, IPlantaObserver
{
    public GameObject planta;
    public GameObject obj;

    public PlantaData data;
    private int index = 0;
    
    public void Start()
    {
        planta = gameObject;
        planta = data.prefabList[index];
        obj = Instantiate(planta, transform.position, Quaternion.identity);
        Clima.instance.AddObservador(this);
        index++;

    }
    public void Atualizar(Clima clima)
    {
        if (data.estacoesIdeais.Contains(clima.estacaoAtual))
        {
            // A estação atual está entre as estações ideais da planta
            if (index < data.prefabList.Count)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
                planta = data.prefabList[index];
                obj = Instantiate(planta, transform.position, Quaternion.identity);
                index++;
            }
            else
            {
                index = 0;
                if (obj != null)
                {
                    Destroy(obj);
                }
                planta = data.prefabList[index];
                obj = Instantiate(planta, transform.position, Quaternion.identity);
                index++;
            }
        }
    }

    
}
