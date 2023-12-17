using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }


    public MenuScript menuScript;

    private void Update()
    {
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    StartInteraction(interactable);
                    return;
                }
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
        Time.timeScale = 0;
        // Se houver uma interação, feche o menu se estiver aberto
        if (menuScript != null && menuScript.IsMenuActive())
        {
            menuScript.CloseMenu();
        }
    }

    void EndInteraction()
    {
        Time.timeScale = 1;
        Debug.Log("Fechar ");
        IsInteracting = false;
    }
}