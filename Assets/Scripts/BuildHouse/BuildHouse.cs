using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildHouse : MonoBehaviour
{
    public HouseData housedata;
    public GameObject prefab;
    public HouseData.BuildComponent[] data;


    public void Start()
    {
        if (housedata != null)
        {
            data = new HouseData.BuildComponent[housedata.build.Length];
            for (int i = 0; i < housedata.build.Length; i++)
            {
                data[i] = new HouseData.BuildComponent
                {
                    name = housedata.build[i].name,
                    // Copy other properties here as needed
                    resourceRequirements = new ResourceRequirement[housedata.build[i].resourceRequirements.Length]
                };

                for (int j = 0; j < housedata.build[i].resourceRequirements.Length; j++)
                {
                    data[i].resourceRequirements[j] = new ResourceRequirement
                    {
                        amount = housedata.build[i].resourceRequirements[j].amount,
                        item = housedata.build[i].resourceRequirements[j].item
                    };
                }
            }
        }
    }

    public void removeritem(InventoryItemData item, int quantidade)
    {
        foreach (HouseData.BuildComponent component in data)
        {
            if (component.resourceRequirements.Length > 0)
            {
                for (int i = 0; i < component.resourceRequirements.Length; i++)
                {
                    if (component.resourceRequirements[i].item == item)
                    {
                        if (component.resourceRequirements[i].amount >= quantidade)
                        {
                            component.resourceRequirements[i].amount -= quantidade;

                            if (component.resourceRequirements[i].amount == 0)
                            {
                                Debug.Log($"Construção '{component.name}' concluída!");
                                //constructionComplete = true;
                                prefab.SetActive(true);
                            }
                            else
                            {
                                Debug.Log($"Removido {quantidade} {item.name} da construção '{component.name}'");
                            }

                            return;
                        }
                        else
                        {
                            Debug.Log($"Quantidade insuficiente de {item.name} na construção '{component.name}' para remover.");
                            return;
                        }
                    }
                }
            }
        }
    }

    public void hud()
    {
        foreach (HouseData.BuildComponent component in data)
        {
            if (component.resourceRequirements.Length > 0)
            {
                //bool constructionCompleted = true; // Inicializa como verdadeiro

                foreach (ResourceRequirement requirement in component.resourceRequirements)
                {
                    if (requirement.amount > 0)
                    {
                        Debug.Log($"A construção '{component.name}' requer {requirement.amount} de {requirement.item.name}");
                    }
                }
            }
            else
            {
                Debug.Log($"A construção '{component.name}' não possui requisitos de recursos.");
            }
        }
    }
}
