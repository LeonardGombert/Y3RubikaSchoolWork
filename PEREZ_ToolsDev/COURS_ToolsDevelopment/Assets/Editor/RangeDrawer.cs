using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(RangeAttribute))]
public class RangeDrawer : PropertyDrawer
{
    /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeAttribute range = attribute as RangeAttribute;

        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, range.min, range.max, label);
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.Slider(position, property, Convert.ToInt32(range.min), Convert.ToInt32(range.max), label);

        else
            EditorGUI.LabelField(position, label.text, "Use range with float or int");
    }*/
}
