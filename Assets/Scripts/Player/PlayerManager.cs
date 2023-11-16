using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    public float fome = 100f;
    public float maxfome = 100f;
    private const float taxaDecaimentoFome = 0.16f;
    public float energia = 100f;
    public float maxEnergia = 100f;
    private RaycastHit hitInfo;
    private event Action<float> OnFomeChanged;   // Evento para a fome
    private event Action<float> OnEnergiaChanged; // Evento para a energia
    [SerializeField] private GameObject objetoQuebravel;
    public SystemQuebrar systemQuebrarComponent;
    public TerraArada terraArada;
    public BuildTool build;
    public Animator animator;
    public CraftSystem craft;
    public int item;

    [SerializeField] private GameObject prefabCanvasInfo;

    private void Awake()
    {
        if (playerManager != null && playerManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            playerManager = this;
        }
        
    }
    public void bater(int id, BuildingData item, Building itemdata)
    {
        if (energia > 10)
        {
            animator.SetTrigger("Bater");
            if (id.Equals(4) && terraArada != null) terraArada.ArarTerra();
            if (id.Equals(6)) systemQuebrarComponent?.Quebrar(hitInfo.collider.gameObject, Opcoes.Tree, hitInfo);
            if (id.Equals(17)) {}
        }
    }


    private void Start()
    {
    
        
       // hotbarDisplay = FindObjectOfType<HotbarDisplay>();
        InvokeRepeating("RegenerarEnergia", 5.0f, 5.0f);
        
    }

    private void Update()
    {
        
        DescerFome();
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            InteragirComObjeto();
        }
        
    }
    private bool isItemInHand = false;
    private int itemInHandId;
    // Chama no update quando apertar E
    private void InteragirComObjeto()
    {
        if (hitInfo.collider != null)
        {
            craft = hitInfo.collider.transform.parent.gameObject.GetComponent<CraftSystem>();

            if (hitInfo.collider.gameObject.tag.Equals("slot1"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);
                // Item do slot Debug.Log(item);
                craft.HandleSlotInteraction(1,item, 1);
                craft.slot1 = item;
            }
            else if (hitInfo.collider.gameObject.tag.Equals("slot2"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);

                craft.HandleSlotInteraction(2,item, 2);
                craft.slot2 = item;
            }
            else if (hitInfo.collider.gameObject.tag.Equals("slot3"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);

                craft.HandleSlotInteraction(3,item, 3);
                craft.slot3 = item;
            }
        }
    }
    public void DescerFome()
    {
        fome -= taxaDecaimentoFome * Time.deltaTime;
        fome = Mathf.Clamp(fome, 0f, maxfome);
        OnFomeChanged?.Invoke(fome); // Chama o evento quando a fome é alterada
    }

    private void RegenerarEnergia()
    {
        energia += 5f;

        // Limite a energia ao valor máximo.
        energia = Mathf.Clamp(energia, 0f, maxEnergia);

        // Chame o evento de energia alterada.
        OnEnergiaChanged?.Invoke(energia);
    }

    public void DescerEnergia(float perder)
    {
        if (energia - perder >= 0)
        {
            energia -= perder;
            OnEnergiaChanged?.Invoke(energia); // Chama o evento quando a energia é alterada
        }
        else
        {
            energia = 0;
            Debug.LogWarning("Energia não pode ser negativa.");
        }
    }

    public void raycast(RaycastHit hitInforay)
    {
        hitInfo = hitInforay;
        string obj = hitInfo.collider.gameObject.name;
        CanvasInfo(hitInfo);
        
        BuildHouse buildHouseComponent = hitInfo.collider.gameObject.GetComponentInChildren<BuildHouse>();
        systemQuebrarComponent = hitInfo.collider.gameObject.GetComponent<SystemQuebrar>();
        terraArada = hitInfo.collider.gameObject.GetComponent<TerraArada>();
        craft = hitInfo.collider.gameObject.GetComponent<CraftSystem>();
        if (buildHouseComponent != null)
        {
            buildHouseComponent.hud();
        }
         
    }

    private GameObject prefab;
    public void CanvasInfo(RaycastHit hitInforay)
    {
        if(hitInforay.collider.gameObject.tag.Equals("CanvasInfo"))
        {
            
            Canvas canvas = hitInforay.collider.gameObject.GetComponentInChildren<Canvas>();
            if (canvas == null)
            {
                Debug.Log("Canvas"); 
            prefab = Instantiate(prefabCanvasInfo, hitInforay.collider.gameObject.transform.position, Quaternion.identity);
            prefab.transform.parent = hitInforay.collider.gameObject.transform;
            }
        }
        else if (prefab != null)
        {
            Destroy(prefab);
        }
        
        

    }
    public InventoryItemData GetItemData()
    {
        if (HotbarDisplay.Display != null)
        {
            return HotbarDisplay.Display.GetItemData();
        }
        else
        {
            return null; // Retorne null se não houver um item selecionado.
        }
    }

    public int GetQuantidade()
    {
        if (HotbarDisplay.Display != null)
        {
            return HotbarDisplay.Display.GetQuantidade();
        }
        else
        {
            return 0; // Retorne 0 se não houver um item selecionado.
        }
    }
}
