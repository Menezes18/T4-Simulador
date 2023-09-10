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
    public int vida = 100;
    public GameObject item;
    public GameObject[] modelos;
    public ControladorItem cntrlIt;

    public void Start()
    {
        cntrlIt = FindObjectOfType<ControladorItem>();
    }

    public void Quebrar(GameObject obj, Opcoes nenhum, RaycastHit hitInfo)
    {
        
        if (nenhum.Equals(opcoes))
        {

            if (cntrlIt.bateu == true)
            {
                vida = 50;
            }
            
            if (vida == 0)
            {
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
                Destroy(this, 2f);
            }
        }
    }
        
    
}
