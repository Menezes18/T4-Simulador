using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InvokeRepeating("RegenerarEnergia", 5.0f, 5.0f);
    }

    private void Update()
    {
        DescerFome();
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
        Debug.Log(obj);

        // Verifique se o objeto atingido possui o componente SystemQuebrar
         systemQuebrarComponent = hitInfo.collider.gameObject.GetComponent<SystemQuebrar>();
        if (systemQuebrarComponent != null)
        {
            objetoQuebravel = hitInfo.collider.gameObject;
        
            if (hitInfo.collider.gameObject.tag.Equals("Tree"))
            {
                
                
            }
            else if (hitInfo.collider.gameObject.tag.Equals("Rocha"))
            {
                systemQuebrarComponent.Quebrar(hitInfo.collider.gameObject, Opcoes.rock, hitInfo);
            }
        }
    }



}