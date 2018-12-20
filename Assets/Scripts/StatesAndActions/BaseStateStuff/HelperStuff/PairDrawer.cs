using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Pair))]
public class PairDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        EditorGUI.BeginProperty(position, label, property);
        //EditorGUILayout.LabelField("asdasd");
      
        EditorGUI.LabelField(position,property.FindPropertyRelative("first").stringValue + " is " + property.FindPropertyRelative("second").floatValue.ToString());


        EditorGUI.EndProperty();

    }

}
