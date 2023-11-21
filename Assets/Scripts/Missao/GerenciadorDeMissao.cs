using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MissaoName
{
    public class GerenciadorDeMissao : MonoBehaviour
    {
        [System.Serializable]
        public class Missao
        {
            public string nome;
            public string descricao;
            public int recompensa;
            public bool concluida;
        }

        public Text nomeMissaoText;
        public Text descricaoMissaoText;
        public Text recompensaText;
        public Button concluirMissaoButton;

        public Missao[] missoes;

        private int indiceMissaoAtual = 0;

        // Adicionando a variável termoPesquisa
        public string termoPesquisa;

        public void AdicionarMissao()
        {
            Missao novaMissao = new Missao();
            missoes = missoes.Concat(new Missao[] { novaMissao }).ToArray();
        }

        public void ExcluirMissao(int indice)
        {
            if (indice >= 0 && indice < missoes.Length)
            {
                missoes = missoes.Where((missao, i) => i != indice).ToArray();
            }
        }

        public void LimparMissoes()
        {
            missoes = new Missao[0];
        }

        public void OrdenarMissaoPorNome()
        {
            missoes = missoes.OrderBy(m => m.nome).ToArray();
        }

        public void AtualizarUI()
        {
            nomeMissaoText.text = missoes[indiceMissaoAtual].nome;
            descricaoMissaoText.text = missoes[indiceMissaoAtual].descricao;
            recompensaText.text = "Recompensa: " + missoes[indiceMissaoAtual].recompensa.ToString();
            concluirMissaoButton.interactable = !missoes[indiceMissaoAtual].concluida;
        }

        public void ConcluirMissao()
        {
            missoes[indiceMissaoAtual].concluida = true;
            indiceMissaoAtual++;

            if (indiceMissaoAtual >= missoes.Length) { indiceMissaoAtual = 0; }

            AtualizarUI();
        }
    }
}
