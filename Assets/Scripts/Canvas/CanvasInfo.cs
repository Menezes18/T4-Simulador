using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasInfo : MonoBehaviour
{
    public Outline _outline;
    public string message;
    public Sprite image;

    public UnityEvent onInteraction;
    private void Start()
    {
        DisableOutline();
        //_outline = GetComponent<Outline>();
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }
    public void DisableOutline()
    {
        _outline.enabled = false;
    }

    public void EnableOutline()
    {
        _outline.enabled = true;
    }
}