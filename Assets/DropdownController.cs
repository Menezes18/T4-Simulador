using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public TMP_Dropdown dropdown; // referência ao seu Dropdown
    public GameObject[] objetosParaAtivar; // vetor de GameObjects que você deseja ativar/desativar

    void Start()
    {
        // Adiciona um listener para chamar a função quando o valor do Dropdown mudar
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int value)
    {
        // Desativa todos os GameObjects no vetor
        foreach (GameObject obj in objetosParaAtivar)
        {
            obj.SetActive(false);
        }

        // Verifica se o valor selecionado é válido e está dentro dos limites do vetor
        if (value >= 0 && value < objetosParaAtivar.Length)
        {
            // Ativa o GameObject correspondente ao índice selecionado
            objetosParaAtivar[value].SetActive(true);
        }
    }
}