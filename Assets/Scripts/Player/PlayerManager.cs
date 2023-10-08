using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public float fome = 100f;
    public float maxfome = 100f;
    public const float taxaDecaimentoFome = 0.16f;
    public float energia = 100f;
    public float maxEnergia = 100f;

    private RaycastHit hitInfo;

    public event Action<float> OnFomeChanged;   // Evento para a fome
    public event Action<float> OnEnergiaChanged; // Evento para a energia
    public GameObject objetoQuebravel;
    public SystemQuebrar systemQuebrarComponent;

    public Animator animator;
    public HotbarDisplay hotbarDisplay;

    public void bater(int id)
    {
        if (energia > 10)
        {
            animator.SetTrigger("Bater");
            DescerEnergia(10f);
            if (id.Equals(6)) systemQuebrarComponent?.Quebrar(hitInfo.collider.gameObject, Opcoes.Tree, hitInfo);
        }
    }

    private void Start()
    {
        hotbarDisplay = FindObjectOfType<HotbarDisplay>();
        InvokeRepeating("RegenerarEnergia", 5.0f, 5.0f);
    }

    private void Update()
    {
        DescerFome();

        // Verifique se o jogador pressionou a tecla "E" nesta atualização do frame
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            InteragirComObjeto();
        }
    }

    private void InteragirComObjeto()
    {
        // Verifique se o objeto atingido possui o componente BuildHouse ou em um de seus filhos
        BuildHouse buildHouseComponent = hitInfo.collider.gameObject.GetComponentInChildren<BuildHouse>();

        if (buildHouseComponent != null)
        {
            InventoryItemData itemData = GetItemData(); // Substitua pelo método adequado para obter os dados do item
            int quantidade = GetQuantidade(); // Substitua pelo método adequado para obter a quantidade
            buildHouseComponent.removeritem(itemData, quantidade);
        }
        else
        {
            // Verifique se o objeto atingido possui o componente SystemQuebrar
            systemQuebrarComponent = hitInfo.collider.gameObject.GetComponent<SystemQuebrar>();
            if (systemQuebrarComponent != null)
            {
                objetoQuebravel = hitInfo.collider.gameObject;

                if (hitInfo.collider.gameObject.tag.Equals("Tree"))
                {
                    // Faça algo relacionado à árvore aqui
                }
                else if (hitInfo.collider.gameObject.tag.Equals("Rocha"))
                {
                    systemQuebrarComponent.Quebrar(hitInfo.collider.gameObject, Opcoes.rock, hitInfo);
                }
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

        // Verifique se o objeto atingido possui o componente BuildHouse ou em um de seus filhos
        BuildHouse buildHouseComponent = hitInfo.collider.gameObject.GetComponentInChildren<BuildHouse>();

        if (buildHouseComponent != null)
        {
            buildHouseComponent.hud();
        }
        else
        {
            // Verifique se o objeto atingido possui o componente SystemQuebrar
            systemQuebrarComponent = hitInfo.collider.gameObject.GetComponent<SystemQuebrar>();
            if (systemQuebrarComponent != null)
            {
                objetoQuebravel = hitInfo.collider.gameObject;

                if (hitInfo.collider.gameObject.tag.Equals("Tree"))
                {
                    // Faça algo relacionado à árvore aqui
                }
                else if (hitInfo.collider.gameObject.tag.Equals("Rocha"))
                {
                    systemQuebrarComponent.Quebrar(hitInfo.collider.gameObject, Opcoes.rock, hitInfo);
                }
            }
        }

        // Resto do seu código do método raycast
    }

    public InventoryItemData GetItemData()
    {
        // Substitua esta lógica pelo código necessário para obter os dados do item da barra de atalho.
        // Por exemplo, você pode usar uma referência ao HotbarDisplay para obter os dados do item selecionado.
        if (hotbarDisplay != null)
        {
            return hotbarDisplay.GetItemData();
        }
        else
        {
            return null; // Retorne null se não houver um item selecionado.
        }
    }

    public int GetQuantidade()
    {
        // Substitua esta lógica pelo código necessário para obter a quantidade do item da barra de atalho.
        // Por exemplo, você pode usar uma referência ao HotbarDisplay para obter a quantidade do item selecionado.
        if (hotbarDisplay != null)
        {
            return hotbarDisplay.GetQuantidade();
        }
        else
        {
            return 0; // Retorne 0 se não houver um item selecionado.
        }
    }
}
