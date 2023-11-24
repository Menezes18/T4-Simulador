using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private GameObject craftSlotHighlight; // Destaque para os slots de craft
    [SerializeField] private int craftSlotNumber; // O número do slot de craft (1 a 9)
    
    private Button button;

    private CraftingSystem craftingSystem; // Referência para o seu sistema de craft

    public int CraftSlotNumber => craftSlotNumber;

    private void Awake()
    {
        itemSprite.preserveAspect = true;

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnCraftSlotClick);

        craftingSystem = FindObjectOfType<CraftingSystem>();
    }

    public void Init(int numeroDoSlot)
    {
        craftSlotNumber = numeroDoSlot;
        UpdateCraftSlot();
    }

    public void UpdateCraftSlot()
    {
        if (craftingSystem != null)
        {
            InventoryItemData item = craftingSystem.GetItemInCraftSlot(craftSlotNumber);

            if (item != null)
            {
                itemSprite.sprite = item.Icon;
                itemSprite.color = Color.white;

                int quantidade = craftingSystem.GetItemCountInCraftSlot(craftSlotNumber);
                if (quantidade > 1) itemCount.text = quantidade.ToString();
                else itemCount.text = "";
            }
            else
            {
                itemSprite.color = Color.clear;
                itemSprite.sprite = null;
                itemCount.text = "";
            }
        }
    }
    

    public void ToggleCraftSlotHighlight()
    {
        craftSlotHighlight.SetActive(!craftSlotHighlight.activeInHierarchy);
    }

    public void ClearCraftSlot()
    {
        if (craftingSystem != null)
        {
            craftingSystem.ClearCraftSlot(craftSlotNumber);
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
            itemCount.text = "";
        }
    }

    public void OnCraftSlotClick()
    {
        Debug.Log("Slot de Craft " + craftSlotNumber + " clicado!");
        // Você pode agora chamar a lógica de craft específica para o slot de craft clicado.
        // Por exemplo:
        // craftingSystem.CraftItemInSlot(craftSlotNumber);
    }
}
