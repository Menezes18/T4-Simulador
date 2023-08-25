using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorItem : MonoBehaviour
{
    
    public void OnItemUsed(GameObject itemGameObject)
    {
        // Obter o componente Animator do item
        Animator itemAnimator = itemGameObject.GetComponent<Animator>();

        if (itemAnimator != null)
        {
            Debug.Log("Aa");
            
                itemAnimator.SetBool("Bater", true);

        }
        else
        {
            Debug.LogWarning("O item não possui um componente Animator.");
        }
    }

}
