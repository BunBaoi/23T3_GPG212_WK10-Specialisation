using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        // Shows all the Stuff in the inspector
        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if(GUILayout.Button("Button"))
        {
            mapGen.GenerateMap();
        }
    }
}
