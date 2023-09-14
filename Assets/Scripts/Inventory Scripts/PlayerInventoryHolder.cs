using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{

    
    public static UnityAction OnPlayerInventoryChanged;
    
    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        
       // SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {
        
    }
    

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame) OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
       
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            
            //Debug.Log(data);
            return true;
        }
        

        return false;
    }
}
