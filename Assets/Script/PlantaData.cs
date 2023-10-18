using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Planta", menuName = "Plantas/Data")]
public class PlantaData : ScriptableObject
{
    public string nome;
    public List<Estacoes> estacoesIdeais = new List<Estacoes>();
    public List<GameObject> prefabList = new List<GameObject>();
}
