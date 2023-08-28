using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorItem : MonoBehaviour
{
    public Animator itemAnimator;
    public GameObject aux;

    
    public float valorEnergia = 1f;
    private PlayerStatus ps;

    public void Start()
    {
        ps = FindObjectOfType<PlayerStatus>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (itemAnimator != null)
            {
                
            itemAnimator = aux.GetComponent<Animator>();
            itemAnimator.SetBool("Bater", false);
            ps.DescerEnergia(valorEnergia);
            
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
            
            itemAnimator.SetBool("Bater", true);

        }
        else
        {
            Debug.LogWarning("O item não possui um componente Animator.");
        }
    }

}
