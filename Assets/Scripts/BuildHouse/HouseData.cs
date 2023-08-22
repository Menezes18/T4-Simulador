using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building System/ Data")]
public class HouseData : ScriptableObject
{
    [System.Serializable]
    public struct BuildComponent
    {
        public string name;
        public GameObject prefab;
        public ResourceRequirement[] resourceRequirements;
    }
    
    public BuildComponent[] build;
}

[System.Serializable]
public class ResourceRequirement
{
    public InventoryItemData item;
    public int amount;
}
