using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
    [SerializeField] MeshFilter mf;
    public void BuildMesh(List<Transform> positions)
    {
        var mesh = new Mesh();
        mf.mesh = mesh;
        var vertices = new Vector3[4];


        for (int i = 0; i < positions.Count; i++)
        {
            vertices[i] = positions[i].position;
        }
        mesh.vertices = vertices;

        var tris = new int[12]
        {
            // lower left triangle
            0, 1, 2,
            // upper right triangle
            2, 3, 0,

            //backwards so the mesh becomes 2-sided (for collisions)
            // lower left triangle
            0, 3, 2,
            // upper right triangle
            2, 1, 0
        };
        mesh.triangles = tris;

        var normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        gameObject.AddComponent<MeshCollider>();
    }
}
