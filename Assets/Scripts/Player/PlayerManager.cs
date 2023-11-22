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
    public CraftArvore craftArvore;
    public CraftArvore quebrarArvore;
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
            if (id.Equals(6) && hitInfo.collider.gameObject.tag.Equals("ArvoreCraft")) 
            {

              quebrarArvore = hitInfo.collider.transform.parent?.gameObject.GetComponent<CraftArvore>();
                
                    quebrarArvore.Quebrar();
                
               

            }

        }
    }
void VerificarScriptNoAvoo(GameObject objeto, int id)
    {
        
        CraftArvore scriptDoObjeto = objeto.GetComponent<CraftArvore>();

        if (scriptDoObjeto != null)
        {
            
            if(id.Equals(1))
            {
                scriptDoObjeto.receitas = 1;
                scriptDoObjeto.vidaMadeira();
            }else if(id.Equals(2))
            {
                scriptDoObjeto.receitas = 2;
                scriptDoObjeto.vidaMadeira();
            }
            return; // Sai da função se o script for encontrado no objeto atual
        }

        // Obtém o pai do objeto atual
        Transform pai = objeto.transform.parent;

        // Verifica se há um pai (evita erro se o objeto não tiver pai)
        if (pai != null)
        {
            // Obtém o avô (pai do pai) do objeto atual
            Transform avo = pai.parent;

            // Verifica se há um avô (evita erro se o pai não tiver pai)
            if (avo != null)
            {
                // Chama recursivamente a função para o avô
                VerificarScriptNoAvoo(avo.gameObject, id);
            }
            else
            {
                Debug.Log("O script do avô NÃO foi encontrado.");
            }
        }
        else
        {
            Debug.Log("O script do pai NÃO foi encontrado.");
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
            craft = hitInfo.collider.transform.parent?.gameObject.GetComponent<CraftSystem>();
            craftArvore = hitInfo.collider.gameObject.GetComponent<CraftArvore>();
            if (hitInfo.collider.gameObject.tag.Equals("slot1"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);
                // Item do slot Debug.Log(item);
                craft.HandleSlotInteraction(1,item, 1);
                
            }
            else if (hitInfo.collider.gameObject.tag.Equals("slot2"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);

                craft.HandleSlotInteraction(2,item, 2);
                
            }
            else if (hitInfo.collider.gameObject.tag.Equals("slot3"))
            {
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);

                craft.HandleSlotInteraction(3,item, 3);
                
            }else if(hitInfo.collider.gameObject.tag.Equals("SlotFinal"))
            {
               if(craft.VerificaItemFinal())
               {

                HotbarDisplay.Display.AddItemToInventoryById(craft.receitas, 1);
                craft.LimparTodosSlots(true, true, true);
                craft.receitas = 0;
                craft.slotOcupado = true;
                craft.slotsOcupado[0] = false;
                craft.slotsOcupado[1] = false;
                craft.slotsOcupado[2] = false;
               }
            }
            if(hitInfo.collider.gameObject.tag.Equals("CraftArvore"))
            {
                
                item = HotbarDisplay.Display.GetCurrentItemId(itemInHandId);
                
                    
                craftArvore.HandleSlotInteraction(1, item, 1);
                
                
            }
                if(hitInfo.collider.gameObject.tag.Equals("SlotGraveto"))
                {
                    VerificarScriptNoAvoo(hitInfo.collider.gameObject, 1);
                }
                if(hitInfo.collider.gameObject.tag.Equals("SlotLenha"))
                {
                    VerificarScriptNoAvoo(hitInfo.collider.gameObject, 2);
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
