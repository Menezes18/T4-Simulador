using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private void Start()
    {
        var chestSaveData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation);

        //SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }

    protected override void LoadInventory(SaveData data)
    {
        // // Check the save data for this specific chests inventory, and if it exists, load it in.
        // if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        // {
        //     this.primaryInventorySystem = chestData.InvSystem;
        //     this.transform.position = chestData.Position;
        //     this.transform.rotation = chestData.Rotation;
        // }
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            EndInteraction();
        }
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0);
        
        UnlockCursor();

        interactSuccessful = true;
    }

    private void UnlockCursor()
    {
        FirstPersonController.instancia.cameraMovementEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        FirstPersonController.instancia.cameraMovementEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void EndInteraction()
    {
        LockCursor();
    }
}
