using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Opcoes
{
    nenhum,
    Tree, 
    rock
    
}
public class SystemQuebrar : MonoBehaviour
{
    public Opcoes opcoes;
    public int vida = 5;
    public GameObject item;
    public GameObject[] modelos;
    public ControladorItem cntrlIt;

    public void Start()
    {
        cntrlIt = FindObjectOfType<ControladorItem>();
    }

    public void Quebrar(GameObject obj, Opcoes nenhum, RaycastHit hitInfo)
    {
        if (nenhum.Equals(opcoes) && vida > 0) // Verifique se a vida é maior que 0
        {
            if (cntrlIt.bateu == true)
            {
                Debug.Log("AAA");
                vida--;
            }
            
            if (vida <= 0)
            {
                // Você pode adicionar código aqui para instanciar um novo item ou fazer outras ações
                Destroy(gameObject);
            }
        }
    }
        
    
}
