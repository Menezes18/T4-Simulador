using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{


    public int ID = -1;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int GoldValue;
    public GameObject ItemPrefab;
    public BuildingData ItemData;
    public Building data;
    public bool _building = false;


    public void UseItem()
    {   
        Debug.LogWarning("aaaa");
        //if (_building) SetDataBuilding();
    }

    public bool buildingUse(bool valor)
    {
        if(_building == valor) return true;
        else{
        Debug.LogWarning("N é building");
        return false;
        }
    }

    public void SetDataBuilding()
    {

        BuildTool buildTools = FindObjectOfType<BuildTool>();
                
                if (buildTools != null)
                {
                    // buildTools.buildingAtivar = true;
                    // buildTools.SetData(ItemData);
                }
                else
                {
                    Debug.LogWarning("BuildTools não encontrado no cenário.");
                }

    }


}


