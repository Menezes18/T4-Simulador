using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorItem : MonoBehaviour
{
   

    public GameObject animacao;
    public Animator aniamacaomao;
    
    public Animator itemAnimator;
    public GameObject aux;
    public GameObject linePrefab;
    
    public float valorEnergia = 1f;
    private PlayerManager ps;

    public bool bateu = false;

    
    public void Start()
    {
        aniamacaomao = animacao.GetComponent<Animator>();
        ps = FindObjectOfType<PlayerManager>();
    }

   
    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (itemAnimator != null)
            {
                
            //itemAnimator.SetBool("Bater", false);
            aniamacaomao.SetBool("BaterMachado", false);
            
            ps.DescerEnergia(valorEnergia);
            bateu = false;
            // valorEnergia = 0;

            }
        }
        
    }

    public void OnItemUsed(GameObject itemGameObject)
    {
        // Obter o componente Animator do item
        itemAnimator = itemGameObject.GetComponent<Animator>();
        aux = itemGameObject;

        if (itemAnimator != null)
        {
            
            //itemAnimator.SetBool("Bater", true);
            aniamacaomao.SetBool("BaterMachado", true);
            bateu = true;

        }
        else
        {
            Debug.LogWarning("O item não possui um componente Animator.");
        }
    }

}
