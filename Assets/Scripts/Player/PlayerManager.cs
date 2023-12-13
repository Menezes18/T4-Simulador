using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    private RaycastHit hitInfo;
    public float playerReach = 3f;
    private CanvasInfo currentCanvasInfo;
    [SerializeField] private GameObject objetoQuebravel;
    public SystemQuebrar systemQuebrarComponent;
    public TerraArada terraArada;
    public BuildTool build;
    public Animator animator;
    public CraftSystem craft;
    public CraftArvore craftArvore;
    public CraftArvore quebrarArvore;
    public int item;
    public PlantTrigger plantT;
    public int ids;
    public ChickenController galinha;
    public CanvasInfo canvasinfo;
    public CanvasInfo auxCanvas;
    public bool isItemInHand = false;
    public int itemInHandId;
    [SerializeField] private GameObject prefabCanvasInfo;
    private bool cursorActive = false;

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
        animator.SetTrigger("Bater");
            if (id.Equals(2927270) && terraArada != null) terraArada.ArarTerra();
            if (id.Equals(27290)) systemQuebrarComponent?.Quebrar(hitInfo.collider.gameObject, Opcoes.Tree, hitInfo);
            if (id.Equals(27290) && hitInfo.collider.gameObject.tag.Equals("ArvoreCraft")) 
            {

              quebrarArvore = hitInfo.collider.transform.parent?.gameObject.GetComponent<CraftArvore>();
                
                    quebrarArvore.Quebrar();
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
    private void Update()
    {
        CheckInteraction();
        if (currentCanvasInfo != null)
        {
            currentCanvasInfo.Interact();
        }
        canvasinfo = hitInfo.collider?.transform.gameObject?.GetComponent<CanvasInfo>();
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            InteragirComObjeto();
        }
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            // Inverte o estado do cursor
            cursorActive = !cursorActive;

            
            if (cursorActive)
            {
                Debug.Log("Ativar");
                FirstPersonController.instancia.cameraMovementEnabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Debug.Log("Desativar");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                FirstPersonController.instancia.cameraMovementEnabled = true;
            }
        }

    }

    #region Interaction
    void CheckInteraction()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hitInfo, playerReach))
        {
            if (hitInfo.collider.gameObject.GetComponent<CanvasInfo>())
            {
                
                CanvasInfo newcanvas = hitInfo.collider.GetComponent<CanvasInfo>();

                if (currentCanvasInfo && newcanvas != currentCanvasInfo)
                {
                    currentCanvasInfo.DisableOutline();
                }
                
                if (newcanvas.enabled)
                {
                    SetCurrentInteraction(newcanvas);
                }
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }
    void SetCurrentInteraction(CanvasInfo canvasInfo)
    {

        currentCanvasInfo = canvasInfo;
        currentCanvasInfo.EnableOutline();
        HUDController.instancia.EnableText(currentCanvasInfo.message, currentCanvasInfo.image);

    }

    private void DisableCurrentInteractable()
    {
        HUDController.instancia.DisableText();
        if (currentCanvasInfo)
        {
            currentCanvasInfo.DisableOutline();
            currentCanvasInfo = null;
        }
    }
    #endregion
    private void InteragirComObjeto()
    {
        if (hitInfo.collider != null)
        {

            var itemPick = hitInfo.collider.transform.gameObject.GetComponent<ItemPickUp>();
            galinha = hitInfo.collider.transform?.gameObject.GetComponent<ChickenController>();
            craft = hitInfo.collider.transform.parent?.gameObject.GetComponent<CraftSystem>();
            craftArvore = hitInfo.collider.gameObject.GetComponent<CraftArvore>();
            plantT = hitInfo.collider?.gameObject.GetComponent<PlantTrigger>();
            if (itemPick != null)
            {
                Debug.Log(HotbarDisplay.Display.IsHotbarFull());
                if (!HotbarDisplay.Display.IsHotbarFull())
                {
                    itemPick.OnItemInventory();
                }
            }
            if (HotbarDisplay.Display.IsItemInHand(15) && plantT != null)
            {
                plantT.agua = true;
            }

            if (galinha != null)
            {
                galinha.playerBool = !galinha.playerBool;
                Debug.Log(galinha.playerBool);
                galinha.currentState = ChickenController.ChickenState.Player;
            }
            if (plantT != null)
            {
                if (plantT.cresceu)
                {
                plantT.ColherPlanta();  
                    
                }
            }
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
                
            }else if(hitInfo.collider.gameObject.tag.Equals("SlotFinal")) {
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

    public void raycast(RaycastHit hitInforay)
    {
        hitInfo = hitInforay;
        float distanceToHit = hitInfo.distance;

        // Adicionando verificação de distância aqui
        if (distanceToHit <= playerReach)
        {
            string obj = hitInfo.collider.gameObject.name;

            BuildHouse buildHouseComponent = hitInfo.collider.gameObject.GetComponentInChildren<BuildHouse>();
            systemQuebrarComponent = hitInfo.collider.gameObject.GetComponent<SystemQuebrar>();
            terraArada = hitInfo.collider.gameObject.GetComponent<TerraArada>();
            craft = hitInfo.collider.gameObject.GetComponent<CraftSystem>();
        
            if (buildHouseComponent != null)
            {
                buildHouseComponent.hud();
            }
        }
    }


    private GameObject prefab;
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
