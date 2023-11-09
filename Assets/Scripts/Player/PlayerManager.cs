using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            if (id.Equals(17))
            {
                
            }
        }
    }


    private void Start()
    {
    
        build = FindObjectOfType<BuildTool>();
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

    // Chama no update quando apertar E
    private void InteragirComObjeto()
    {
        
        BuildHouse buildHouseComponent = hitInfo.collider.gameObject.GetComponentInChildren<BuildHouse>();

        if (buildHouseComponent != null)
        {
            InventoryItemData itemData = GetItemData(); // Substitua pelo método adequado para obter os dados do item
            int quantidade = GetQuantidade(); // Substitua pelo método adequado para obter a quantidade
            buildHouseComponent.removeritem(itemData, quantidade);
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
