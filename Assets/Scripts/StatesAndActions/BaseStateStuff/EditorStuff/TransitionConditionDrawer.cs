using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TransitionCondition))]
public class TransitionConditionDrawer : PropertyDrawer {

    private SerializedProperty targetVar;
    private SerializedProperty targetVal;
    private SerializedProperty comp;
    private SerializedProperty trigger;


    static int inc = 20;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 6;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        targetVar = property.FindPropertyRelative("targetVar");
        targetVal = property.FindPropertyRelative("targetValue");
        comp = property.FindPropertyRelative("comp");
        trigger = property.FindPropertyRelative("trigger");

        

        var rectVar = new Rect(position.x+5, position.y + 0*inc,position.width, inc);
        var rectVal = new Rect(position.x + 5, position.y + 1*inc, position.width, inc);
        var rectComp = new Rect(position.x + 5, position.y +2*inc, position.width, inc);
        var rectTrig = new Rect(position.x + 5, position.y +3*inc, position.width, inc);

        EditorGUI.PropertyField(rectVar, targetVar);
        EditorGUI.PropertyField(rectComp, comp);

        EditorGUI.PropertyField(rectVal, targetVal);
        EditorGUI.PropertyField(rectTrig, trigger);

        
    }


}
