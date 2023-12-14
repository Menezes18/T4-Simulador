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
    public bool semente = true;
    
    //Variavel balde
    public bool isBucket = false;
    public int maxWater = 100;
    public int currentWater = 0; // Quantidade atual de água no balde
    

}


