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
  

    public void Start()
    {
        
    }

    public void Quebrar(GameObject obj, Opcoes nenhum, RaycastHit hitInfo)
    {
        if (nenhum.Equals(opcoes) && vida > 0) // Verifique se a vida é maior que 0
        {
            Debug.Log("AAA");
            vida--;

            if (vida <= 0)
            {
                Instantiate(item, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
        
    
}
