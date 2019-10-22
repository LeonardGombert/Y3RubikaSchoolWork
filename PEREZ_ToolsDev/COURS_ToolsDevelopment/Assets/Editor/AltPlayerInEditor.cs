using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAlternatif))]
[CanEditMultipleObjects]
public class AltPlayerInEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerAlternatif altPlayer = (PlayerAlternatif)target;

        altPlayer.attack = EditorGUILayout.IntSlider("Attack", altPlayer.attack, 0, 100);
        ProgressBar(altPlayer.attack/100, "Attack");
        altPlayer.defense = EditorGUILayout.IntSlider("Defense", altPlayer.defense, 0, 100);
        ProgressBar(altPlayer.defense / 100, "Defense");

        bool allowSceneObjects = !EditorUtility.IsPersistent(target);
        altPlayer.weapon = (GameObject)EditorGUILayout.ObjectField("Weapon", altPlayer.weapon, typeof(GameObject), allowSceneObjects);
    }

    void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TestField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
