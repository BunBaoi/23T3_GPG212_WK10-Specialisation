using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator2))]
public class MapGenEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MeshGenerator2 mapGen = (MeshGenerator2)target;

        // Shows all the stuff in the inspector
        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.Generate();
            }
        }

        if (GUILayout.Button("Button"))
        {
            mapGen.Generate();
        }
    }
}
