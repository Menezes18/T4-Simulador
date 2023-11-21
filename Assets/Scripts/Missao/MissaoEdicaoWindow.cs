using UnityEditor;
using UnityEngine;
using static MissaoName.GerenciadorDeMissao;

namespace MissaoName
{
    public class MissaoEdicaoWindow : EditorWindow
    {
        private GerenciadorDeMissao gerenciador;
        private int indiceMissao;
        private string nome;
        private string descricao;
        private int recompensa;
        private bool concluida;

        public void DefinirMissao(GerenciadorDeMissao gerenciador, int indiceMissao)
        {
            this.gerenciador = gerenciador;
            this.indiceMissao = indiceMissao;

            if (indiceMissao >= 0 && indiceMissao < gerenciador.missoes.Length)
            {
                var missao = gerenciador.missoes[indiceMissao];
                nome = missao.nome;
                descricao = missao.descricao;
                recompensa = missao.recompensa;
                concluida = missao.concluida;
            }
            else
            {
                nome = "";
                descricao = "";
                recompensa = 0;
                concluida = false;
            }
        }

        private void OnGUI()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Editar Missão", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            nome = EditorGUILayout.TextField("Nome:", nome);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            descricao = EditorGUILayout.TextField("Descrição:", descricao);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            recompensa = EditorGUILayout.IntField("Recompensa:", recompensa);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            concluida = EditorGUILayout.Toggle("Concluída:", concluida);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Salvar"))
            {
                if (indiceMissao >= 0 && indiceMissao < gerenciador.missoes.Length)
                {
                    var missao = gerenciador.missoes[indiceMissao];
                    missao.nome = nome;
                    missao.descricao = descricao;
                    missao.recompensa = recompensa;
                    missao.concluida = concluida;

                    gerenciador.AtualizarUI();
                }

                Close();
            }

            if (GUILayout.Button("Excluir"))
            {
                if (EditorUtility.DisplayDialog("Excluir Missão", "Tem certeza de que deseja excluir esta missão?", "Sim", "Cancelar"))
                {
                    gerenciador.ExcluirMissao(indiceMissao);
                    Close();
                }
            }

            if (GUILayout.Button("Cancelar"))
            {
                Close();
            }
            GUILayout.EndHorizontal();
        }
    }
}