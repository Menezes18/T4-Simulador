using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instancia;

    private void Awake()
    {
        instancia = this;
    }

    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private Image imageGameobject;

    public void EnableText(string text, Sprite image)
    {
        imageGameobject.gameObject.SetActive(true);
        interactionText.text = text;
        imageGameobject.sprite = image;
        interactionText.gameObject.SetActive(true);
        
    }

    public void DisableText()
    {
        imageGameobject.gameObject.SetActive(false);

        interactionText.gameObject.SetActive(false);
    }

}
