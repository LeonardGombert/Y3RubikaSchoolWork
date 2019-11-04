using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ObjectPresets))]
public class PresetsEditorDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectPresets myScript = (ObjectPresets)target;
        if (GUILayout.Button("Define as base Values"))
        {
            myScript.DefineAsBase();
            //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "", false);
        }
        if (GUILayout.Button("Reset Preset"))
        {
            myScript.ResetPresets();
        }
    }
}
