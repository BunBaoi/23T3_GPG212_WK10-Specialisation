using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyCustomEditor : EditorWindow
{

    [MenuItem("Tools/My Custom Editor")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<MyCustomEditor>();
        wnd.titleContent = new GUIContent("My Custom Editor");
    }

    public void CreateGUI()
    {
        // Get a list of all sprites in the project
        var allObjectGuids = AssetDatabase.FindAssets("t:Sprite");
        var allObjects = new List<Sprite>();
        foreach (var guid in allObjectGuids) 
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        // Create a two-pane view with the left pane being fixed with
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        // Add the view to the visual tree by adding it as a child to the root element
        rootVisualElement.Add(splitView);

        // A TwoPanelSplitView always needs exactly two child elements
        var leftPane = new ListView();
        splitView.Add(leftPane);        
        var rightPane = new VisualElement();
        splitView.Add(rightPane);

        // Initialise the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
    }

}
