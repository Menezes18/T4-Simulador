using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> craftableItems; // Lista de itens que podem ser criados
    [SerializeField] private CraftSlot_UI[] craftSlots; // Array de slots de craft (1 a 9)

    private InventoryItemData[] itemsInCraftSlots; // Itens nos slots de craft
    private int[] itemCountInCraftSlots; // Quantidade de cada item nos slots de craft

    private void Start()
    {
        InitializeCraftSlots();
    }

    private void InitializeCraftSlots()
    {
        itemsInCraftSlots = new InventoryItemData[craftSlots.Length];
        itemCountInCraftSlots = new int[craftSlots.Length];
    }

    public void AddItemToCraftSlot(int slotNumber, InventoryItemData item)
    {
        if (slotNumber >= 1 && slotNumber <= craftSlots.Length)
        {
            itemsInCraftSlots[slotNumber - 1] = item;
            itemCountInCraftSlots[slotNumber - 1]++;
            craftSlots[slotNumber - 1].UpdateCraftSlot();
            CheckForCraft();
        }
    }

    public InventoryItemData GetItemInCraftSlot(int slotNumber)
    {
        if (slotNumber >= 1 && slotNumber <= craftSlots.Length)
        {
            return itemsInCraftSlots[slotNumber - 1];
        }
        return null;
    }

    public int GetItemCountInCraftSlot(int slotNumber)
    {
        if (slotNumber >= 1 && slotNumber <= craftSlots.Length)
        {
            return itemCountInCraftSlots[slotNumber - 1];
        }
        return 0;
    }

    public void ClearCraftSlot(int slotNumber)
    {
        if (slotNumber >= 1 && slotNumber <= craftSlots.Length)
        {
            itemsInCraftSlots[slotNumber - 1] = null;
            itemCountInCraftSlots[slotNumber - 1] = 0;
            craftSlots[slotNumber - 1].ClearCraftSlot();
            CheckForCraft();
        }
    }

    private void CheckForCraft()
    {
        foreach (var craftableItem in craftableItems)
        {
            bool canCraft = true;

            // for (int i = 0; i < craftableItem.RequiredItems.Length; i++)
            // {
            //     InventoryItemData requiredItem = craftableItem.RequiredItems[i];
            //     int requiredItemCount = craftableItem.RequiredItemCount[i];
            //
            //     if (GetItemCountInCraftSlot(i + 1) < requiredItemCount || GetItemInCraftSlot(i + 1) != requiredItem)
            //     {
            //         canCraft = false;
            //         break;
            //     }
            // }
            //
            // if (canCraft)
            // {
            //     // Crafting bem-sucedido! Você pode adicionar o item resultante ao inventário do jogador, por exemplo.
            //     Debug.Log("Crafted: " + craftableItem.Name);
            // }
        }
    }
}
