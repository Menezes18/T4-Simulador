using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TMP_Text tutorialText;
    public string completionText = "Tutorial concluído!";
    public string[] tutorialTexts;
    public Outline[] outlines;
    private int currentStep = 0;
    private bool tutorialCompleted = false;
    public GameObject[] destroy;

    public void Awake()
    {
        UpdateOutline();
    }

    void Start()
    {
        // Inicializa o texto do tutorial.
        UpdateTutorialText();
        // Inicializa a posição do bloco de tutorial no início.
        
    }

    void Update()
    {
        UpdateOutline();
        // Verifica se o tutorial foi concluído.
        if (tutorialCompleted)
        {
            return;
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            currentStep++;

            // Verifica se todos os passos do tutorial foram concluídos.
            if (currentStep >= tutorialTexts.Length)
            {
                tutorialCompleted = true;
                tutorialText.text = completionText;
                Debug.Log(completionText);
                // Aqui você pode adicionar qualquer ação que deseja realizar após o tutorial.
            }
            else
            {
                quebrar();
                UpdateTutorialText();
                //destroy[currentStep -1].SetActive(true);
                UpdateOutline();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu com o bloco de tutorial.
        if (other.CompareTag("Player") && !tutorialCompleted)
        {
            
        }
    }

    void UpdateTutorialText()
    {
        tutorialText.text = tutorialTexts[currentStep];
        //Debug.Log(tutorialText.text);
    }

    void UpdateOutline()
    {
        // Verifica se o Outline existe e se o índice está dentro do intervalo.
        if (outlines != null && currentStep < outlines.Length)
        {
            // Ativa o Outline, define a cor para verde e a largura para 10.
            outlines[currentStep].enabled = true;
            outlines[currentStep].OutlineMode = Outline.Mode.OutlineAll;
            outlines[currentStep].OutlineColor = Color.green;
            outlines[currentStep].OutlineWidth = 10f;
        }
    }
    
    public void quebrar()
    {
        Destroy(destroy[currentStep - 1]);
    }

}
