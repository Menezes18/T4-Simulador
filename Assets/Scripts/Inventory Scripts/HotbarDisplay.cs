using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class HotbarDisplay : StaticInventoryDisplay
{
    public static HotbarDisplay Display;
    private int _maxIndexSize = 9;
    private int _currentIndex = 0;
    public Database database;
    private PlayerControls _playerControls;
    public Transform itemPrefab;
    //private GameObject _spawnedObject;
    public GameObject spawnObject2;
    public bool hasSpawned = true;
    private int itemId = -2;
    public bool buildingUse = false;
    public BuildingData ItemData;
    public InventoryItemData _ivItemData;
    public BuildTool _buildTools;

    public GameObject spawnedObject;
    private PlayerManager _playerManager;
    


    private void Awake()
    {
        if (Display != null && Display != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Display = this;
        }
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerControls = new PlayerControls();
       // _itemPickUp = FindObjectOfType<ItemPickUp>();
        _buildTools = FindObjectOfType<BuildTool>();
    }

    protected override void Start()
    {
        base.Start();
        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;
        slots[_currentIndex].ToggleHighlight();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerControls.Enable();

        _playerControls.Player.Hotbar1.performed += Hotbar1;
        _playerControls.Player.Hotbar2.performed += Hotbar2;
        _playerControls.Player.Hotbar3.performed += Hotbar3;
        _playerControls.Player.Hotbar4.performed += Hotbar4;
        _playerControls.Player.Hotbar5.performed += Hotbar5;
        _playerControls.Player.Hotbar6.performed += Hotbar6;
        _playerControls.Player.Hotbar7.performed += Hotbar7;
        _playerControls.Player.Hotbar8.performed += Hotbar8;
        _playerControls.Player.Hotbar9.performed += Hotbar9;
        _playerControls.Player.Hotbar10.performed += Hotbar10;
        _playerControls.Player.UseItem.performed += UseItem;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _playerControls.Disable();

        _playerControls.Player.Hotbar1.performed -= Hotbar1;
        _playerControls.Player.Hotbar2.performed -= Hotbar2;
        _playerControls.Player.Hotbar3.performed -= Hotbar3;
        _playerControls.Player.Hotbar4.performed -= Hotbar4;
        _playerControls.Player.Hotbar5.performed -= Hotbar5;
        _playerControls.Player.Hotbar6.performed -= Hotbar6;
        _playerControls.Player.Hotbar7.performed -= Hotbar7;
        _playerControls.Player.Hotbar8.performed -= Hotbar8;
        _playerControls.Player.Hotbar9.performed -= Hotbar9;
        _playerControls.Player.Hotbar10.performed -= Hotbar10;
        _playerControls.Player.UseItem.performed -= UseItem;
    }

    #region Hotbar Select Methods

    private void Hotbar1(InputAction.CallbackContext obj)
    {
        SetIndex(0);
        pegaritem();
    }

    private void Hotbar2(InputAction.CallbackContext obj)
    {
        SetIndex(1);
        pegaritem();
    }

    private void Hotbar3(InputAction.CallbackContext obj)
    {
        SetIndex(2);
        pegaritem();
    }

    private void Hotbar4(InputAction.CallbackContext obj)
    {
        SetIndex(3);
        pegaritem();
    }

    private void Hotbar5(InputAction.CallbackContext obj)
    {
        SetIndex(4);
        pegaritem();
    }

    private void Hotbar6(InputAction.CallbackContext obj)
    {
        SetIndex(5);
        pegaritem();
    }

    private void Hotbar7(InputAction.CallbackContext obj)
    {
        SetIndex(6);
        pegaritem();
    }

    private void Hotbar8(InputAction.CallbackContext obj)
    {
        SetIndex(7);
        pegaritem();
    }

    private void Hotbar9(InputAction.CallbackContext obj)
    {
        SetIndex(8);
        pegaritem();
    }

    private void Hotbar10(InputAction.CallbackContext obj)
    {
        SetIndex(9);
        pegaritem();
    }

    #endregion
    public bool IsHotbarFull()
    {
        int emptySlots = CountEmptySlots();
        //Debug.Log("Número de slots vazios: " + emptySlots);
        return (emptySlots == 0);
    }
    public int CountEmptySlots()
    {
        int emptySlotCount = 0;

        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;

            if (slot.ItemData == null)
            {
                // Encontrou um slot vazio
                emptySlotCount++;
            }
        }

        return emptySlotCount;
    }

    // Limpa o item selecionado do slot atual
    public void ClearSelectedItem()
    {
        InventorySlot selectedSlot = slots[_currentIndex].AssignedInventorySlot;

        if (selectedSlot.ItemData != null)
        {
            selectedSlot.RemoveFromStack(1); // Remover 1 unidade do stack do item
            slots[_currentIndex].UpdateUISlot();

            // Remover o item atualmente instanciado na mão, se houver
            //Destroy(spawnObject2);
            hasSpawned = true;
        }
    }
    
    // Verifica os itens na hotbar
    public void CheckHotbar()
    {
        foreach(InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if(slot.ItemData != null)
            {
                Debug.Log(slot.ItemData.DisplayName + " " + slot.StackSize);

            }
        }


    }
    
    // Adiciona um item à barra de atalho, empilhando ou encontrando um slot vazio
    public void AddItemToHotbar(InventoryItemData itemData, int quantity)
    {
        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if (slot.ItemData != null && slot.ItemData == itemData)
            {
                // O item já está presente na hotbar, apenas aumenta a quantidade do stack
                slot.AddToStack(quantity);
                slotUI.UpdateUISlot();
                return;
            }
        }

        // O item não está presente na hotbar, encontra um slot vazio para adicionar o item
        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if (slot.ItemData == null)
            {
                // Encontrou um slot vazio, adiciona o item
                slot.AssignItem(itemData, quantity);
                slotUI.UpdateUISlot();
                return;
            }
        }

        // Caso não haja slots vazios disponíveis na hotbar, você pode tomar alguma ação ou exibir uma mensagem de erro.
        Debug.LogWarning("Não há slots disponíveis na hotbar para adicionar o item.");
    }
    
    public void AddItemToInventoryById(int itemId, int quantity)
    {
        var db = Resources.Load<Database>("Database");
        InventoryItemData itemData = db.GetItem(itemId);

        if (itemData != null)
        {
            AddItemToHotbar(itemData, quantity);
        }
        else
        {
            Debug.LogWarning("Item not found in the database. ID: " + itemId);
        }
    }


    public InventoryItemData GetItemData()
    {
        InventorySlot selectedSlot = slots[_currentIndex].AssignedInventorySlot;
        return selectedSlot.ItemData;
    }

    public int GetQuantidade()
    {
        InventorySlot selectedSlot = slots[_currentIndex].AssignedInventorySlot;
        return selectedSlot.StackSize;
    }

    // Obtém o item atualmente na mão
    public void GetItemInHand()
    {
        InventoryItemData itemData = slots[_currentIndex].AssignedInventorySlot.ItemData;
        if (itemData != null)
        {
            string itemName = itemData.DisplayName;
            int itemId = itemData.ID;
            Debug.Log("Item na mão: " + itemName + " (ID: " + itemId + ")");
        }
        else
        {
            Debug.Log("Nenhum item na mão.");
        }
    }
    
    // Obtém a contagem total de um item específico na hotbar
    public int GetItemCount(int itemId)
    {
        int itemCount = 0;

        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if (slot.ItemData != null && slot.ItemData.ID == itemId)
            {
                itemCount += slot.StackSize;
            }
        }

        return itemCount;
    }
    public int GetCurrentItemId(int item)
    {
        InventoryItemData itemData = slots[_currentIndex].AssignedInventorySlot.ItemData;

         return (itemData != null) ? itemData.ID : -1;
    }
    //Verifica se o item da mao é do item pelo id
    public bool IsCurrentItem(int itemId)
    {
        InventoryItemData itemData = slots[_currentIndex].AssignedInventorySlot.ItemData;
        return (itemData != null && itemData.ID == itemId);
    }
    // Remove um item específico da barra de atalho por ID e quantidade
    public void RemoveItem(int itemId, int quantity)
    {
        InventorySlot currentSlot = slots[_currentIndex].AssignedInventorySlot;

        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;

            // Verifica se o slot contém o item desejado e não é o item atual na mão
            if (slot.ItemData != null && slot.ItemData.ID == itemId && slot != currentSlot)
            {
                int removedQuantity = slot.RemoveStack(quantity); // Remove a quantidade especificada do stack do item
                slotUI.UpdateUISlot();

                // Remove o item atualmente instanciado na mão se a quantidade for zero
                if (slot.StackSize == 0)
                {
                    Destroy(spawnObject2);
                    hasSpawned = true;
                }
                break;
            }
        }
    }
    public void RemoveItemFromInventory(int itemId, int quantity)
    {
        // Percorre todos os slots do inventário (incluindo a hotbar)
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot_UI slotUI = slots[i];
            InventorySlot slot = slotUI.AssignedInventorySlot;

            // Verifica se o slot contém o item desejado
            if (slot.ItemData != null && slot.ItemData.ID == itemId)
            {
                int removedQuantity = slot.RemoveStack(Mathf.Min(quantity, slot.StackSize));
                slotUI.UpdateUISlot();

                // Atualiza a quantidade restante a ser removida
                quantity -= removedQuantity;

                // Verifica se já removeu a quantidade desejada
                if (quantity <= 0)
                {
                    return;
                }
            }
        }
    }
    


    // Verifica se um item específico existe na barra de atalho
    public bool CheckItemInHotbar(int itemId)
    {
        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if (slot.ItemData != null && slot.ItemData.ID == itemId)
            {
                return true; // O item foi encontrado na hotbar
            }
        }
        return false; // O item não foi encontrado na hotbar
    }
    
    private void Update()
    {
        ShowItemInHotbar();
        if (_playerControls.Player.MouseWheel.ReadValue<float>() > 0.1f)
        {
            ChangeIndex(1);
            pegaritem();
        }
        if (_playerControls.Player.MouseWheel.ReadValue<float>() < -0.1f)
        {
            ChangeIndex(-1);
            pegaritem();
        }
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
            
            InventoryItemData item = slots[_currentIndex].AssignedInventorySlot.ItemData;

            if (item._building)
            {
                _buildTools.placedata = item.ItemData;
                _buildTools.data = item.ItemData;
                _buildTools.buildAtivar = true;
            }
        }
        else
        {
           _buildTools.data = null;
           _buildTools.DisableObjectPreview();
        }

    }
    
    public void FixedUpdate()
    {
        DesativarBuild();
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
            itemId = slots[_currentIndex].AssignedInventorySlot.ItemData.ID;
            //Debug.Log(itemId)
        }
    }
    //
    private void pegaritem()
    {
        Destroy(spawnObject2);
        hasSpawned = true;

        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
            itemId = slots[_currentIndex].AssignedInventorySlot.ItemData.ID;
            var db = Resources.Load<Database>("Database");
            if (hasSpawned)
            {
                InstantiateItemInHand(itemId);
                
                hasSpawned = false;
            }
        }
    }
    
    public bool ItemSemente()
    {

        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
            InventoryItemData item = slots[_currentIndex].AssignedInventorySlot.ItemData;
            if (item.semente)
            {
                return true;
            }
        }
        return false;
    }

    
    public void InstantiateItemInHand(int itemId)
    {
        var db = Resources.Load<Database>("Database");
        //string itemName = db.GetItemNameById(itemId);
        var item = db.GetItem(itemId);

        if (item != null)
        {
             spawnedObject = Instantiate(item.ItemPrefab, itemPrefab.transform.position, Quaternion.identity);
            
            // Obter a rotação da câmera principal

            // Calcular a rotação desejada, adicionando um desvio de 60 graus no eixo Y
            var cameraRotation = Camera.main.transform.rotation;
            var desiredRotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y + 60, 0);
        
            spawnedObject.transform.rotation = desiredRotation;
            var meshcolliderItem = spawnedObject.GetComponentInChildren<MeshCollider>();
            var colliderDoItem = spawnedObject.GetComponent<Collider>();
            var sphereCollider = spawnedObject.GetComponent<SphereCollider>();
            var filhodomesh = FindMeshColliderInChildren(spawnedObject.transform);
            var filhodobox = FindBoxColliderInChildren(spawnedObject.transform);

            //MeshCollider itemfilho = item.GetComponentInChildren<MeshCollider>();
            
            
           // if(itemfilho != null) itemfilho.enabled = false;
           if (filhodobox != null) filhodobox.enabled = false; 
            if (filhodomesh != null) filhodomesh.enabled = false;
            if (meshcolliderItem != null) meshcolliderItem.enabled = false;
            if (sphereCollider != null) sphereCollider.enabled = false;
            if (colliderDoItem != null) colliderDoItem.enabled = false;
            
            spawnedObject.transform.SetParent(itemPrefab);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            spawnObject2 = spawnedObject;
        }
        else
        {
            Debug.LogWarning("Item não encontrado no banco de dados. ID do item: " + itemPrefab);
        }
    }
    BoxCollider FindBoxColliderInChildren(Transform parent)
    {
        BoxCollider meshCollider = null;

        // Percorre todos os filhos do objeto pai
        for (int i = 0; i < parent.childCount; i++)
        {
            // Obtém o transform do filho atual
            Transform child = parent.GetChild(i);

            // Tenta obter o MeshCollider do filho atual
            meshCollider = child?.GetComponent<BoxCollider>();

            // Se encontrado, retorna imediatamente
            if (meshCollider != null)
            {
                return meshCollider;
            }

            // Se não encontrado no filho atual, chama recursivamente para os filhos desse filho
            meshCollider = FindBoxColliderInChildren(child);

            // Se encontrado em algum filho recursivo, retorna imediatamente
            if (meshCollider != null)
            {
                return meshCollider;
            }
        }

        // Se não encontrado em nenhum filho, retorna null
        return null;
    }
    MeshCollider FindMeshColliderInChildren(Transform parent)
    {
        MeshCollider meshCollider = null;

        // Percorre todos os filhos do objeto pai
        for (int i = 0; i < parent.childCount; i++)
        {
            // Obtém o transform do filho atual
            Transform child = parent.GetChild(i);

            // Tenta obter o MeshCollider do filho atual
            meshCollider = child.GetComponent<MeshCollider>();

            // Se encontrado, retorna imediatamente
            if (meshCollider != null)
            {
                return meshCollider;
            }

            // Se não encontrado no filho atual, chama recursivamente para os filhos desse filho
            meshCollider = FindMeshColliderInChildren(child);

            // Se encontrado em algum filho recursivo, retorna imediatamente
            if (meshCollider != null)
            {
                return meshCollider;
            }
        }

        // Se não encontrado em nenhum filho, retorna null
        return null;
    }

// Método para lidar com o uso de um item da barra de atalho
    public bool menu = true;
    public void UseItem(InputAction.CallbackContext obj)
    {
        if (menu)
        {
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
           
            InventoryItemData item = slots[_currentIndex].AssignedInventorySlot.ItemData;
            
            _playerManager.bater(itemId, item);

        }
        else
        {
            itemId = -1;
            _playerManager.bater(itemId, null);
        }
            
        }
    }

    public void DesativarBuild()
    {
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
        {
            InventoryItemData item = slots[_currentIndex].AssignedInventorySlot.ItemData;
            if (!item._building)
            {
                BuildTool.instancia.DisableObjectPreview();
                BuildTool.instancia.buildAtivar = false;
                
            }
        }
    }
    // Verifica se você tem recursos suficientes na barra de atalho para uma construção
    private bool HasSufficientResource(int itemId, int requiredAmount)
    {
        int itemCount = 0;

        foreach (InventorySlot_UI slotUI in slots)
        {
            InventorySlot slot = slotUI.AssignedInventorySlot;
            if (slot.ItemData != null && slot.ItemData.ID == itemId)
            {
                itemCount += slot.StackSize;
                if (itemCount >= requiredAmount)
                {
                    return true; // Recursos suficientes encontrados
                }
            }
        }

        return false; // Recursos insuficientes
    }

    /// Verifica se o item com o ID fornecido está na mão.
    /// "itemId" O ID do item a ser verificado.
    /// >True se o item estiver na mão, False caso contrário.
    public bool IsItemInHand(int itemId)
    {
        InventoryItemData itemData = slots[_currentIndex].AssignedInventorySlot.ItemData;
        if (itemData != null && itemData.ID == itemId)
        {
          //  Debug.Log("True");
            return true; // O item está na mão
        }
//        Debug.Log("ff");
        return false; 
    }

    private void ChangeIndex(int direction)
    {
        
        slots[_currentIndex].ToggleHighlight();
        _currentIndex += direction;

        if (_currentIndex > _maxIndexSize) _currentIndex = 0;
        if (_currentIndex < 0) _currentIndex = _maxIndexSize;

        slots[_currentIndex].ToggleHighlight();
        
        
    }

    private void ShowItemInHotbar()
    {
        InventorySlot_UI currentSlotUI = slots[_currentIndex];
        InventorySlot currentSlot = currentSlotUI.AssignedInventorySlot;

        if (currentSlot.ItemData != null)
        {
            int itemId = currentSlot.ItemData.ID;
            string itemName = currentSlot.ItemData.DisplayName;

            //Debug.Log("Item na hotbar: " + itemName + " (ID: " + itemId + ")");
        }
        else
        {
            //Debug.Log("Nenhum item na hotbar.");
        }
        
    }

    private void SetIndex(int newIndex)
    {
        slots[_currentIndex].ToggleHighlight();
        _currentIndex = newIndex;
        slots[_currentIndex].ToggleHighlight();
    }
}
