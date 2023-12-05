using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColourMap, MeshGenerator}
    public DrawMode drawMode;

    [Header("Mapping Transform")]
    public int mapWidth;
    public int mapHeight;
    public Vector2 size;
    public float Scale;

    [Header("Height Map")]
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    [Header("Map Seed")]
    public int seed;

    public bool autoUpdate;

    public TerrainType[] regions;


    public void GenerateMap()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight,seed, Scale, octaves, persistance, lacunarity, size) ;

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions [i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.MeshGenerator)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), (TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight)));
        }

    }

    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
