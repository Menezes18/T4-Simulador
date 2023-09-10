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

    public GameObject[] modelos;
    
    
    private ControladorItem _controladorItem;
    void Start()
    {
        _controladorItem = FindObjectOfType<ControladorItem>();
    }

    // Update is called once per frame
    public void Quebrar(GameObject obj, Opcoes nenhum, RaycastHit hitInfo)
    {
        
        if (nenhum.Equals(opcoes))
        {
            Debug.Log("AAA");
            if (_controladorItem.bateu == true)
            {
                vida = 0;
                Debug.Log("Bateu");
            }
            
            if (vida == 0)
            {
                Debug.Log("0");
            }
        }
    }
        
    
}
