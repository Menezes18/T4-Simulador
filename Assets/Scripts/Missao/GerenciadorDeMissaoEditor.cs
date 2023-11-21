using UnityEditor;
using UnityEngine;

namespace MissaoName {

    [CustomEditor(typeof(GerenciadorDeMissao))]
    public class GerenciadorDeMissaoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GerenciadorDeMissao gerenciador = (GerenciadorDeMissao)target;

            DrawDefaultInspector();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Editor de Missões", EditorStyles.boldLabel);

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

            EditorGUILayout.LabelField("Filtrar Missões", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            gerenciador.termoPesquisa = EditorGUILayout.TextField("Pesquisar Missão:", gerenciador.termoPesquisa);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            if (GUILayout.Button("Atualizar UI"))
            {
                gerenciador.AtualizarUI();
            }
        }
    }
}
