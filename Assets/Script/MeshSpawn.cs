using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshSpawn : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    [SerializeField] Vector2Int size;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(size.x + 1) * (size.y + 1)]; // Creating each of the triangles clockwise rotation E.g (0,0)(1,0)(1,1)(1,0)


        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[size.x * size.y * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < size.y; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size.x + 1;
                triangles[tris + 5] = vert + size.x + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }


    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }


    /*
    private void OnDrawGizmos() // Creating all the Vertices
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
    */
}
