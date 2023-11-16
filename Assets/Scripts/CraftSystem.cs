using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    //public static CraftSystem craft;
    public int slot1 = 0;
    public int slot2 = 0;
    public int slot3 = 0;
    [SerializeField] private int receitas;
    private bool slotOcupado1 = false;
    private bool slotOcupado2 = false;
    private bool slotOcupado3 = false;

    [SerializeField] private Transform[] itemTransforms;
    private InventoryItemData itemdata2;
    private Database data;
    private GameObject[] prefabitem = new GameObject[3];
    private float velocidadeRotacao = 30.0f;
    void FixedUpdate()
    {
        slot1 = (slot1 == -1) ? 0 : slot1;
        slot2 = (slot2 == -1) ? 0 : slot2;
        slot3 = (slot3 == -1) ? 0 : slot3;
        receita();
        for (int i = 0; i < 3; i++)
        {
            if (prefabitem.Length > i && prefabitem[i] != null)
            {
                float rotacaoAtual = Time.deltaTime * velocidadeRotacao;
                prefabitem[i].transform.Rotate(Vector3.up, rotacaoAtual);
            }
        }
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
        rb.isKinematic = true;
        SphereCollider sphereCollider = prefabitem[current - 1].GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        prefabitem[current - 1].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
       
    }

  public void receita()
    {
        if(!slot1.Equals(0) || !slot2.Equals(0) || !slot3.Equals(0))
        {
            receitas = int.Parse((slot1 != 0 ? slot1.ToString() : "") + (slot2 != 0 ? slot2.ToString() : "") + (slot3 != 0 ? slot3.ToString() : ""));
            Debug.Log("Receita: " + receitas);
        }
    }

    public void HandleSlotInteraction(int slots, int id, int trava)
    {
        // Verifica se o slot já está ocupado
        bool slotOcupado = false;

        // Verifica slot 1
        if (slots == 1 && !id.Equals(-1) && trava.Equals(1))
        {
            slotOcupado = slotOcupado1;
            if (!slotOcupado)
            {
                slot1 = id;
                Debug.Log(slot1);
                InstanciarItem(id,1);
                HotbarDisplay.Display.ClearSelectedItem();
                slotOcupado1 = true; // Marca o slot como ocupado
            }
        }
        else if (slots == 1 && trava.Equals(1))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot1, 1);
            id = -1;
            slot1 = -1;
            slot1 = 0;
            slotOcupado1 = false; // Marca o slot como desocupado
            Destroy(prefabitem[slots - 1]);
        }

        // Verifica slot 2
        if (slots == 2 && !id.Equals(-1) && trava.Equals(2))
        {
            slotOcupado = slotOcupado2;
            if (!slotOcupado)
            {
                slot2 = id;
                Debug.Log(slot2);
                InstanciarItem(id,2);
                HotbarDisplay.Display.ClearSelectedItem();
                slotOcupado2 = true; // Marca o slot como ocupado
            }
        }
        else if (slots == 2 && trava.Equals(2))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot2, 1);
            id = -1;
            slot2 = -1;
            slot2 = 0;
            slotOcupado2 = false; // Marca o slot como desocupado
            Destroy(prefabitem[slots - 1]);
        }

        // Verifica slot 3
        if (slots == 3 && !id.Equals(-1) && trava.Equals(3))
        {
            slotOcupado = slotOcupado3;
            if (!slotOcupado)
            {
                slot3 = id;
                Debug.Log(slot3);
                InstanciarItem(id,3);
                HotbarDisplay.Display.ClearSelectedItem();
                slotOcupado3 = true; // Marca o slot como ocupado
            }
        }
        else if (slots == 3 && trava.Equals(3))
        {
            HotbarDisplay.Display.AddItemToInventoryById(slot3, 1);
            id = -1;
            slot3 = -1;
            slot3 = 0;
            slotOcupado3 = false; // Marca o slot como desocupado
            Destroy(prefabitem[slots - 1]);
        }

        // Se algum slot estiver ocupado, exiba uma mensagem de log
        if (slotOcupado)
        {
            Debug.Log("Slot ocupado! Não é possível adicionar um novo item.");
        }
    }

}
