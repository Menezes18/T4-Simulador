using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorItem : MonoBehaviour
{
    
    public void OnItemUsed(GameObject itemGameObject)
    {
        // Obter o componente Animator do item
        Animator itemAnimator = itemGameObject.GetComponent<Animator>();

        if (itemAnimator != null)
        {
            // Definir o parâmetro booleano "Bater" como true
            itemAnimator.SetBool("Bater", true);
        }
        else
        {
            Debug.LogWarning("O item não possui um componente Animator.");
        }
    }

}
