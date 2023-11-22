using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class InventoryItemEditorWindow : EditorWindow
{
    [NonSerialized] public int id;
    [NonSerialized] public string displayName;
    [NonSerialized] public string description;
    [NonSerialized] public Sprite icon;
    [NonSerialized] public int maxStackSize;
    [NonSerialized] public int goldValue;
    [NonSerialized] public GameObject itemPrefab;
    [NonSerialized] public BuildingData itemData;
    [NonSerialized] public Building data;
    [NonSerialized] public bool isBuilding;
    [NonSerialized] public bool isSeed;

    private Vector2 scrollPosition;
    private InventoryItemData[] allItems;

    private InventoryItemData selectedItem;

    [MenuItem("Window/Inventory Item Editor")]
    public static void ShowWindow()
    {
        InventoryItemEditorWindow window = GetWindow<InventoryItemEditorWindow>("Inventory Item Editor");
        window.LoadAllItems();
    }

    private string searchQuery = "";

    private void OnGUI()
    {
        GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);

        EditorGUILayout.Separator(); // Linha de divisão

        // Campo de pesquisa
        searchQuery = EditorGUILayout.TextField("Search by Name:", searchQuery);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Mostra todos os itens existentes
        if (allItems != null && allItems.Length > 0)
        {
            foreach (var item in allItems)
            {
                // Filtra os itens com base na pesquisa
                if (string.IsNullOrEmpty(searchQuery) || item.DisplayName.ToLower().Contains(searchQuery.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal();

                    // Modificação para mostrar o ID junto com o nome
                    GUILayout.Label($"ID: {item.ID} - {item.DisplayName}", EditorStyles.boldLabel, GUILayout.Width(200));

                    // Exibe o ícone do item
                    if (item.Icon != null)
                    {
                        GUILayout.Label(item.Icon.texture, GUILayout.Width(50), GUILayout.Height(50));
                    }

                    if (GUILayout.Button("Edit", GUILayout.Width(50)))
                    {
                        OpenEditWindow(item);
                    }
                    if (GUILayout.Button("Delete", GUILayout.Width(50)))
                    {
                        DeleteInventoryItem(item);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        EditorGUILayout.EndScrollView();

        // Botão para abrir o menu de criação de item
        if (GUILayout.Button("Create New Item"))
        {
            ShowCreateItemMenu();
        }

        // Mostra a janela de edição se um item estiver selecionado
        if (selectedItem != null)
        {
            ShowEditWindow();
        }
    }


    public void CreateInventoryItem()
    {
        if (id <= 0 || string.IsNullOrEmpty(displayName))
        {
            Debug.LogError("ID e Nome de Exibição são necessários para criar um item de inventário.");
            return;
        }

        InventoryItemData newItemData = ScriptableObject.CreateInstance<InventoryItemData>();
        newItemData.ID = id;
            newItemData.DisplayName = displayName;
            newItemData.Description = description;
            newItemData.Icon = icon;
            newItemData.MaxStackSize = maxStackSize;
            newItemData.GoldValue = goldValue;

        GameObject novoItemObject = new GameObject(displayName);
        novoItemObject.transform.position = new Vector3(0, 0, 0);
        newItemData.ItemPrefab = Instantiate(itemPrefab, novoItemObject.transform);
        newItemData.ItemData = itemData;
        newItemData.data = data;
        newItemData._building = isBuilding;
        newItemData.semente = isSeed;


        
        Rigidbody rb = novoItemObject.AddComponent<Rigidbody>();
        
        MeshCollider meshCollider = novoItemObject.AddComponent<MeshCollider>();

        MeshCollider meshColliderItem = newItemData.ItemPrefab.AddComponent<MeshCollider>();
        meshColliderItem.convex = true;
        

        ItemPickUp itemPickup = novoItemObject.AddComponent<ItemPickUp>();
        itemPickup.ItemData = newItemData;

        // Salva o asset diretamente na pasta Resources/ItemData
        string caminho = "Assets/Scripts/Item Scripts/Resources/ItemData/" + displayName + ".asset";
        AssetDatabase.CreateAsset(newItemData, caminho);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string prefabCaminho = "Assets/Scripts/Item Scripts/Resources/ItemData/" + displayName + ".prefab";
        prefabCaminho = AssetDatabase.GenerateUniqueAssetPath(prefabCaminho);
        PrefabUtility.SaveAsPrefabAsset(novoItemObject, prefabCaminho);

        Debug.Log("Prefab do Item de Inventário criado em: " + prefabCaminho);
        Debug.Log("Item de Inventário criado em: " + caminho);

        // Atualiza a lista de itens
        LoadAllItems();
    }




    private void OpenEditWindow(InventoryItemData item)
    {
        selectedItem = item;
    }

    private void ShowEditWindow()
    {
        // Cria uma nova janela para editar o item
        InventoryItemEditWindow editWindow = EditorWindow.GetWindow<InventoryItemEditWindow>("Edit Inventory Item");
        editWindow.SetItem(selectedItem);
        editWindow.Focus();
        selectedItem = null;
    }

    private void DeleteInventoryItem(InventoryItemData item)
    {
        // Deleta o item
        string path = AssetDatabase.GetAssetPath(item);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Deleted item: " + item.DisplayName);

        // Limpa os campos após deletar o item
        id = 0;
        displayName = "";
        description = "";
        icon = null;
        maxStackSize = 0;
        goldValue = 0;
        itemPrefab = null;
        itemData = null;
        data = null;
        isBuilding = false;
        isSeed = false;

        // Atualiza a lista de itens
        LoadAllItems();
    }

    private void LoadAllItems()
    {
        // Carrega todos os itens existentes na pasta Resources/ItemData
        System.Object[] loadedItems = Resources.LoadAll("ItemData", typeof(InventoryItemData));
        allItems = loadedItems.Cast<InventoryItemData>().ToArray();
    }

    private void ShowCreateItemMenu()
    {
        // Cria uma nova janela para criar um novo item
        InventoryItemCreateWindow createWindow = EditorWindow.GetWindow<InventoryItemCreateWindow>("Create Inventory Item");
        createWindow.Focus();
    }
}

public class InventoryItemEditWindow : EditorWindow
{
    private InventoryItemData currentItem;

    public void SetItem(InventoryItemData item)
    {
        currentItem = item;
    }

    private void OnGUI()
    {
        if (currentItem != null)
        {
            GUILayout.Label("Edit Inventory Item", EditorStyles.boldLabel);

            currentItem.ID = EditorGUILayout.IntField("Id", currentItem.ID);
            currentItem.DisplayName = EditorGUILayout.TextField("Display Name:", currentItem.DisplayName);
            currentItem.Description = EditorGUILayout.TextArea(currentItem.Description, GUILayout.Height(60));
            currentItem.Icon = (Sprite)EditorGUILayout.ObjectField("Icon:", currentItem.Icon, typeof(Sprite), false);
            currentItem.MaxStackSize = EditorGUILayout.IntField("Max Stack Size:", currentItem.MaxStackSize);
            currentItem.GoldValue = EditorGUILayout.IntField("Gold Value:", currentItem.GoldValue);
            currentItem.ItemPrefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab:", currentItem.ItemPrefab, typeof(GameObject), false);
            currentItem.ItemData = (BuildingData)EditorGUILayout.ObjectField("Item Data:", currentItem.ItemData, typeof(BuildingData), false);
            currentItem.data = (Building)EditorGUILayout.ObjectField("Data:", currentItem.data, typeof(Building), false);
            currentItem._building = EditorGUILayout.Toggle("Is Building:", currentItem._building);
            currentItem.semente = EditorGUILayout.Toggle("Is Seed:", currentItem.semente);

            if (GUILayout.Button("Save Changes"))
            {
                SaveChanges();
            }
        }
        else
        {
            GUILayout.Label("No item selected for editing.", EditorStyles.boldLabel);
        }
    }

    private void SaveChanges()
    {
        if (currentItem != null)
        {
            EditorUtility.SetDirty(currentItem);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Saved changes for item: " + currentItem.DisplayName);
        }
    }
}

public class InventoryItemCreateWindow : EditorWindow
{
    private int id;
    private string displayName;
    private string description;
    private Sprite icon;
    private int maxStackSize;
    private int goldValue;
    private GameObject itemPrefab;
    private BuildingData itemData;
    private Building data;
    private bool isBuilding;
    private bool isSeed;

    private void OnGUI()
    {
        GUILayout.Label("Create Inventory Item", EditorStyles.boldLabel);

        // Campos de entrada para um novo item
        id = EditorGUILayout.IntField("ID:", id);
        displayName = EditorGUILayout.TextField("Display Name:", displayName);
        description = EditorGUILayout.TextArea(description, GUILayout.Height(60));
        icon = (Sprite)EditorGUILayout.ObjectField("Icon:", icon, typeof(Sprite), false);
        maxStackSize = EditorGUILayout.IntField("Max Stack Size:", maxStackSize);
        goldValue = EditorGUILayout.IntField("Gold Value:", goldValue);
        itemPrefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab:", itemPrefab, typeof(GameObject), false);
        itemData = (BuildingData)EditorGUILayout.ObjectField("Item Data:", itemData, typeof(BuildingData), false);
        data = (Building)EditorGUILayout.ObjectField("Data:", data, typeof(Building), false);
        isBuilding = EditorGUILayout.Toggle("Is Building:", isBuilding);
        isSeed = EditorGUILayout.Toggle("Is Seed:", isSeed);

        if (GUILayout.Button("Create Inventory Item"))
        {
            InventoryItemEditorWindow editorWindow = GetWindow<InventoryItemEditorWindow>();
            editorWindow.id = id;
            editorWindow.displayName = displayName;
            editorWindow.description = description;
            editorWindow.icon = icon;
            editorWindow.maxStackSize = maxStackSize;
            editorWindow.goldValue = goldValue;
            editorWindow.itemPrefab = itemPrefab;
            editorWindow.itemData = itemData;
            editorWindow.data = data;
            editorWindow.isBuilding = isBuilding;
            editorWindow.isSeed = isSeed;

            editorWindow.CreateInventoryItem();
            Close();
        }
    }
}
