using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Ingredients))]
public class IngredientDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //First thing to do
        EditorGUI.BeginProperty(position, label, property);

        //Draw the label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //To indent child fields
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //Caculate Rects
        var amountRect = new Rect(position.x, position.y, 30, position.height);
        var unitsRect = new Rect(position.x + 35, position.y, 50, position.height); //add 35 because previous Rect is 30 long + 5 px of Spacing
        var namRect = new Rect(position.x + 90, position.y, position.width - 90, position.height); /*add 90 because previous 2 rects = 85 long + 5 px of Spacing
                                                                                                   width -90 makes it so that the last item is correctly spaced out
                                                                                                   from the rightmost border of the screen, even when stretched*/
        //Draw fields
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUI.PropertyField(unitsRect, property.FindPropertyRelative("units"), GUIContent.none);
        EditorGUI.PropertyField(namRect, property.FindPropertyRelative("name"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        //Last thing to do
        EditorGUI.EndProperty();
    }
}
