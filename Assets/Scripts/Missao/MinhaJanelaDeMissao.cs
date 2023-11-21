using UnityEditor;
using UnityEngine;

namespace MissaoName
{
    public class MinhaJanelaDeMissao : EditorWindow
    {
        private GerenciadorDeMissao gerenciador;
        private string termoPesquisa = "";
        private int indiceMissaoSelecionada = -1;

        [MenuItem("Window/Minha Janela de Missão")]
        public static void MostrarJanela()
        {
            GetWindow<MinhaJanelaDeMissao>("Missão");
        }

        private void OnEnable()
        {
            gerenciador = FindObjectOfType<GerenciadorDeMissao>();
        }

        private void OnGUI()
        {
            if (gerenciador == null)
            {
                EditorGUILayout.LabelField("Adicione o Gerenciador de Missão à sua cena.");
                return;
            }

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Editor de Missões", EditorStyles.boldLabel);

            // Agrupar elementos visualmente para aplicar um estilo de fundo
            EditorGUILayout.BeginVertical("box");

            // Campo de pesquisa
            GUILayout.BeginHorizontal();
            termoPesquisa = EditorGUILayout.TextField("Pesquisar Missão:", termoPesquisa);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Exibir missões com base no termo de pesquisa
            for (int i = 0; i < gerenciador.missoes.Length; i++)
            {
                if (string.IsNullOrEmpty(termoPesquisa) || gerenciador.missoes[i].nome.Contains(termoPesquisa))
                {
                    GUIStyle style = new GUIStyle(EditorStyles.helpBox);
                    style.normal.background = MakeTex(4, 1, new Color(0.8f, 0.8f, 0.8f, 1f));

                    EditorGUILayout.BeginVertical(style);

                    GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.normal.textColor = Color.blue; // Altere a cor conforme necessário

                    EditorGUILayout.LabelField("Nome: " + gerenciador.missoes[i].nome, labelStyle);
                    EditorGUILayout.LabelField("Descrição: " + gerenciador.missoes[i].descricao, labelStyle);
                    EditorGUILayout.LabelField("Recompensa: " + gerenciador.missoes[i].recompensa.ToString(), labelStyle);
                    EditorGUILayout.LabelField("Concluída: " + gerenciador.missoes[i].concluida.ToString(), labelStyle);

                    EditorGUILayout.Space();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Editar Missão"))
                    {
                        AbrirJanelaDeEdicao(i);
                    }
                    GUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndVertical(); // Fim do grupo com fundo

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Outras Operações", EditorStyles.boldLabel);

            if (GUILayout.Button("Adicionar Missão"))
            {
                gerenciador.AdicionarMissao();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Limpar Missões"))
            {
                gerenciador.LimparMissoes();
            }

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Ordenar Missões", EditorStyles.boldLabel);

            if (GUILayout.Button("Ordenar por Nome"))
            {
                gerenciador.OrdenarMissaoPorNome();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Atualizar UI"))
            {
                gerenciador.AtualizarUI();
            }
        }

        private void AbrirJanelaDeEdicao(int indiceMissao)
        {
            indiceMissaoSelecionada = indiceMissao;
            MissaoEdicaoWindow janelaEdicao = GetWindow<MissaoEdicaoWindow>("Editar Missão");
            janelaEdicao.DefinirMissao(gerenciador, indiceMissao);
        }

        private Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = color;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}