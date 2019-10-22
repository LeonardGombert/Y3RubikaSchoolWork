using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum GameObjectTypes { Empty, Cube, Spehere, Capsule, Cylinder, Plane, Quad, Sprite}

[ExecuteInEditMode]
public class PrefabCreatorWindow : EditorWindow
{
    string myString = "Hello World";
    bool gOGroupEnabled;
    bool compGroupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    public GameObjectTypes gameObjectTypes;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/SearchBar #&S")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(PrefabCreatorWindow));
    }

    /*void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }*/

    void OnGUI()
    {
        //first category --> gameObject type
        GUILayout.Label("GameObject Type", EditorStyles.boldLabel);



        gOGroupEnabled = EditorGUILayout.BeginToggleGroup("GameObject Settings", gOGroupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        compGroupEnabled = EditorGUILayout.BeginToggleGroup("Component Settings", compGroupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("yeet"))
        {
            SpawnCharacter();
        }
    }    

    static void SpawnCharacter(GameObject gOToSpawn = null)
    {
        GameObject gO = new GameObject("Custom GameObject");
        Undo.RegisterCreatedObjectUndo(gO, "Create" + gO); //stores information about object to allow you to undo changes
        Selection.activeGameObject = gO;
    }
}
