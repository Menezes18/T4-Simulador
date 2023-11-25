using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public int slot1 = 0;
    public int slot2 = 0;
    public int slot3 = 0;
    public int receitas;
    public bool[] slotsOcupado = new bool[3] { false, false, false };

    public InventoryItemData itemdata2;
    public Database data;
    public GameObject[] prefabitem = new GameObject[3];
    public GameObject FinalItem;
    [SerializeField] private Transform[] itemTransforms;
    public bool slotOcupado = false;
    public bool itemFinalInstanciado = false;
    public int currentReceitaId = -1;
    
    void FixedUpdate()
    {
        Regras();
        receita();
        GirarItem();
        
    }
    public void InstanciarItem(int itemId, int current)
    {
        // Obtém os dados do item
        itemdata2 = data.GetItem(itemId);

        // Obtém o prefab do item
        var prefab = itemdata2.ItemPrefab;

        // Instancia o objeto e armazena a referência
        prefabitem[current - 1] = Instantiate(prefab, itemTransforms[current - 1].position, itemTransforms[current - 1].rotation);

        // Obtém o Rigidbody do objeto instanciado
        Rigidbody rb = prefabitem[current - 1].GetComponent<Rigidbody>();
        if(rb != null)
        {
        rb.isKinematic = true;
        }
        SphereCollider sphereCollider = prefabitem[current - 1].GetComponent<SphereCollider>();
        if(sphereCollider != null)
        {
        sphereCollider.enabled = false;
        }
        prefabitem[current - 1].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
       
    }
   public void receita()
    {
        if(!slot1.Equals(0) || !slot2.Equals(0) || !slot3.Equals(0))
        {
            receitas = int.Parse((slot1 != 0 ? slot1.ToString() : "") + (slot2 != 0 ? slot2.ToString() : "") + (slot3 != 0 ? slot3.ToString() : "") + "0");
            Debug.Log("Receita: " + receitas);
            VerificarReceitaNoBancoDeDados(receitas);
            
        }
    }
    public void LimparTodosSlots(bool clear1, bool clear2, bool clear3) {
        slot1 = clear1 ? 0 : slot1;
        slot2 = clear2 ? 0 : slot2;
        slot3 = clear3 ? 0 : slot3;
    }
    private void VerificarReceitaNoBancoDeDados(int receitaId)
    {
        // Verifica se o receitaId foi alterado
        if (receitaId != currentReceitaId)
        {
            // Se sim, redefine a variável de controle e o ID atual
            itemFinalInstanciado = false;
            currentReceitaId = receitaId;
            foreach (Transform filho in FinalItem.transform)
            {
                
                Destroy(filho.gameObject);
            
            }
        }

        // Verifica se o item final ainda não foi instanciado
        if (!itemFinalInstanciado)
        {
            var itemReceita = data.GetItem(receitaId);

            if (itemReceita != null)
            {
                
                InstanciarItemFinal(itemReceita);

                itemFinalInstanciado = true;
            }
            else
            {
                return;
            }
        }
    }
    private void InstanciarItemFinal(InventoryItemData itemData)
    {
        // Obtém o prefab do item final
        var prefab = itemData.ItemPrefab;

        // Instancia o objeto e armazena a referência em uma variável temporária
        GameObject novoItem = Instantiate(prefab, new Vector3(FinalItem.transform.position.x, FinalItem.transform.position.y, FinalItem.transform.position.z), FinalItem.transform.rotation);

        novoItem.transform.parent = FinalItem.transform;
        Rigidbody rb = novoItem.GetComponent<Rigidbody>();
        if(rb != null)
        {
        rb.isKinematic = true;
        }
        SphereCollider sphereCollider = novoItem.GetComponent<SphereCollider>();
        if(sphereCollider != null)
        {
        sphereCollider.enabled = false;

        }
        novoItem.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public bool VerificaItemFinal()
    {
    
        Debug.Log("VerificaItemFinal");
        if (FinalItem != null)
        {          
            if (FinalItem.transform.childCount > 0)
            {
                // O "FinalItem" tem pelo menos um item filho
                LimparItensFilhos();
                return true;
            }
            else
            {
                // O "FinalItem" não tem nenhum item filho
                return false;
            }
        }
        else
        {
            
            Debug.LogError("GameObject 'FinalItem' não encontrado!");
            return false;
        
        }

    }
    private void LimparItensFilhos()
    {
         Destroy(prefabitem[0]);
         Destroy(prefabitem[1]);
         Destroy(prefabitem[2]);
        foreach (Transform filho in FinalItem.transform)
        {
            Destroy(filho.gameObject);
        }
    }
    public void HandleSlotInteraction(int slots, int id, int trava)
    {
        
        // Verifica se o slot já está ocupado
        slotOcupado = false;

        // Verifica slot 1
        if (slots == 1 && !id.Equals(-1) && trava.Equals(1))
        {
            slotOcupado = slotsOcupado[0];
            if (!slotOcupado)
            {

                slot1 = id;
                InstanciarItem(id,1);
                HotbarDisplay.Display.ClearSelectedItem();
                slotsOcupado[0] = true; // Marca o slot como ocupado
                
                
            }
        }
        else if (slots == 1 && trava.Equals(1))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot1, 1);
            id = -1;
            slot1 = -1;
            slot1 = 0;
            slotsOcupado[0] = false; // Marca o slot como desocupado
            itemFinalInstanciado = false;
            Destroy(prefabitem[slots - 1]);
        }

        // Verifica slot 2
        if (slots == 2 && !id.Equals(-1) && trava.Equals(2))
        {
            slotOcupado = slotsOcupado[1];
            if (!slotOcupado)
            {
                slot2 = id;
                InstanciarItem(id,2);
                HotbarDisplay.Display.ClearSelectedItem();
                slotsOcupado[1] = true; // Marca o slot como ocupado
            }
        }
        else if (slots == 2 && trava.Equals(2))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot2, 1);
            id = -1;
            slot2 = -1;
            slot2 = 0;
            slotsOcupado[1] = false; // Marca o slot como desocupado
            itemFinalInstanciado = false;
            Destroy(prefabitem[slots - 1]);
        }

        // Verifica slot 3
        if (slots == 3 && !id.Equals(-1) && trava.Equals(3))
        {
            slotOcupado = slotsOcupado[2];
            if (!slotOcupado)
            {
                slot3 = id;
                InstanciarItem(id,3);
                HotbarDisplay.Display.ClearSelectedItem();
                slotsOcupado[2] = true; // Marca o slot como ocupado
            }
        }
        else if (slots == 3 && trava.Equals(3))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot3, 1);
            id = -1;
            slot3 = -1;
            slot3 = 0;
            slotsOcupado[2] = false; // Marca o slot como desocupado
            itemFinalInstanciado = false;
            Destroy(prefabitem[slots - 1]);
        }

        // Se algum slot estiver ocupado, exiba uma mensagem de log
        if (slotOcupado)
        {
            Debug.Log("Slot ocupado! Não é possível adicionar um novo item.");
        }
    }
    public void Regras()
    {
        slot1 = (slot1 == -1) ? 0 : slot1;
        slot2 = (slot2 == -1) ? 0 : slot2;
        slot3 = (slot3 == -1) ? 0 : slot3;
        if (slot1 == 0 && slot2 == 0 && slot3 == 0) receitas = 0;
        
    }
    public void GirarItem()
    {
        var velocidadeRotacao = 30.0f;
        for (int i = 0; i < 3; i++)
        {
            if (prefabitem.Length > i && prefabitem[i] != null)
            {
                float rotacaoAtual = Time.deltaTime * velocidadeRotacao;
                prefabitem[i].transform.Rotate(Vector3.up, rotacaoAtual);
            }
        }
    }
}
