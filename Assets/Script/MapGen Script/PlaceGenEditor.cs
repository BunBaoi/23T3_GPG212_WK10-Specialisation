using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlacementGenerator))]
public class PlaceGenEditor : Editor
{

    public override void OnInspectorGUI()
    {
        PlacementGenerator placeGen = (PlacementGenerator)target;

        // Shows all the stuff in the inspector
        if (DrawDefaultInspector())
        {

        }

        if (GUILayout.Button("Button"))
        {
            placeGen.Generate();
        }
        else if (GUILayout.Button("Clear"))
        {
            placeGen.Clear();
        }
    }
}
