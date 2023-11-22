using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CraftArvore : MonoBehaviour
{
    public static CraftArvore arvoreC;
    [SerializeField] private bool painelAtivo = false;
    public int slot = 0;
    public int receitas = 0;
    
    public GameObject painel;
    public GameObject arvore;
    private bool slotOcupado = false;
    private bool slotsOcupados = false;
    public int madeira = 0;

    public Transform itemTransform;
    public GameObject[] itens;
    public void Quebrar()
    {
        
        madeira = madeira -1;
        if(madeira <= 0)
        {
            
            if (receitas.Equals(1))
            {
                Instantiate(itens[0], new Vector3(itemTransform.position.x, itemTransform.position.y + 2f, itemTransform.position.z), Quaternion.identity);
                arvore.SetActive(false);
                receitas = 0;
                slotsOcupados = false;

            }
           if (receitas.Equals(2))
            {
                Instantiate(itens[1], new Vector3(itemTransform.position.x, itemTransform.position.y + 2f, itemTransform.position.z), Quaternion.identity);
                arvore.SetActive(false);
                receitas = 0;
                slotsOcupados = false;
            }
        }    
    }
    
    public void vidaMadeira()
    {
        if(receitas.Equals(1))
        {
            madeira = 2;
            painel.SetActive(false);
        }
        if(receitas.Equals(2))
        {
            madeira = 3;
            painel.SetActive(false);
        }
    }
    public void HandleSlotInteraction(int slots, int id, int trava)
    {
        // Verifica se o slot já está ocupado
        slotOcupado = slotsOcupados;

        if (slots == 1 && id == 7 && trava == 1)
        {
            if (!slotOcupado)
            {
                
                painel.SetActive(true);
                arvore.SetActive(true);
                slot = id;
                HotbarDisplay.Display.ClearSelectedItem();
                slotsOcupados = true; 
            }
        }
        else if (slots == 1 && trava == 1)
        {
            // Aqui, remova o item do slot e desative a árvore
            if(slotsOcupados)
            {
            madeira = 0;
            receitas = 0;
            arvore.SetActive(false);
            HotbarDisplay.Display.AddItemToInventoryById(slot, 1);
            painel.SetActive(false);
            slotsOcupados = false;  

            }
        }
    }

}
