using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildHouse : MonoBehaviour
{
    public HouseData housedata;
    public HouseData.BuildComponent[] data;

    public void Start()
    {
        if (housedata != null)
        {
            data = new HouseData.BuildComponent[housedata.build.Length];
            Array.Copy(housedata.build, data, housedata.build.Length);
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
                            Debug.Log($"Removido {quantidade} {item.name} da construção '{component.name}'");
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
        Debug.Log($"Item {item.name} não encontrado nos requisitos de recursos de nenhuma construção.");
    }

    public void hud()
    {
        foreach (HouseData.BuildComponent component in data)
        {
            if (component.resourceRequirements.Length > 0)
            {
                foreach (ResourceRequirement requirement in component.resourceRequirements)
                {
                    Debug.Log($"A construção '{component.name}' requer {requirement.amount} de {requirement.item.name}");
                }
            }
            else
            {
                Debug.Log($"A construção '{component.name}' não possui requisitos de recursos.");
            }
        }
    }
}
