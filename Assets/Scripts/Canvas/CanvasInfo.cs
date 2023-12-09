using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasInfo : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public Transform alvo;
    public TextMeshProUGUI textCanvas;
    public Image image;
    public string text;
    public bool painel;
    public Sprite novaSprite; // Adicione esta variável para a nova sprite que você deseja atribuir

    public void Start()
    {
        AlterarTextoImage();
    }
    public void canvasDesativar()
    {
        painel = false;
        canvas.SetActive(false);
    }
    public void canvasAtivar()
    {
        painel = true;
        canvas.SetActive(true);
    }

    void Update()
    {
        if (painel)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            alvo = player.transform;

            if (alvo != null)
            {
                if (alvo != null)
                {
                    
                    Vector3 direcaoInvertida = alvo.position - canvas.transform.position;
                    canvas.transform.rotation = Quaternion.LookRotation(-direcaoInvertida.normalized, Vector3.up);
                }
            }
        }
    }

    void AlterarTextoImage()
    {
        textCanvas.text = text;

        // Verifica se uma nova sprite foi atribuída
        if (novaSprite != null)
        {
            // Atribui a nova sprite ao componente Image
            image.sprite = novaSprite;
        }
    }
}