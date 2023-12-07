using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeshGenerator2 : MonoBehaviour
{
    public enum Mapping {NoiseMap}
    public Mapping mapping;

    [Header("Mapping Transform")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Vector2Int size;
    public float Scale;

    [Header("Height Map")]
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    [Header("Map Seed")]
    public int seed;

    [Header("Fall Off Settings")]
    public float falloffStart;[Range(0, 1)]


    public float falloffEnd;
    [Range(0, 1)]

    public bool autoUpdate;

    public void GenerateWorld()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = CreateVertices(); // Create each of the vertices
        mesh.triangles = CreateTriangles(); // Creates the Triangles

        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh; // Renders the Triangles Mesh


        // KEY AREA
        float[,] Mapping = PerlinNoise.GenerateNoiseMap(size.x, size.y, seed, Scale, octaves, persistance, lacunarity,size);


    }

    private Vector3[] CreateVertices()
    {
        Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];

        for (int z = 0, i = 0; z <= size.y; z++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        return vertices;
    }

    private int[] CreateTriangles()
    {
        int[] triangles = new int[size.x * size.y * 6];

        for (int z = 0, vert = 0, tris = 0; z < size.y; z++)
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

        return triangles;
    }
}
