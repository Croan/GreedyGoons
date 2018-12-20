using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Transition))]
public class TransitionEditor : Editor
{


    private SerializedProperty statelist;
    private SerializedProperty toState;
    private SerializedProperty targetVar;
    private SerializedProperty targetVal;
    private SerializedProperty comp;
    private SerializedProperty trigger;

    private SerializedProperty conditions;
    private void OnEnable()
    {
        statelist = serializedObject.FindProperty("fromStates");
        toState = serializedObject.FindProperty("toState");
       // targetVar = serializedObject.FindProperty("targetVar");
       // targetVal = serializedObject.FindProperty("targetValue");
       // comp = serializedObject.FindProperty("comp");
       // trigger = serializedObject.FindProperty("trigger");

        conditions = serializedObject.FindProperty("conditions");
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        serializedObject.Update();
        var someClass = target as Transition;


        
        EditorGUILayout.PropertyField(statelist, true);
       // EditorGUILayout.HelpBox("Specify the states that this transition will be attached to.", MessageType.Info, false);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(toState, true);

        
        //EditorGUILayout.PropertyField(targetVar, true);
        //EditorGUILayout.PropertyField(comp, true);

        // EditorGUILayout.PropertyField(targetVal, true);
        //EditorGUILayout.PropertyField(trigger, true);
        // EditorGUILayout.HelpBox("will the target value be reset to false (0) after it is used in a transition?", MessageType.Info, false);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(conditions, true);


        



        serializedObject.ApplyModifiedProperties();
        //someClass.SetupTransition();
    }
}


/*
[CustomEditor(typeof(Transition))]
public class TransitionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        var controller = target as Transition;
        EditorGUIUtility.LookLikeInspector();
        
        SerializedProperty tps = serializedObject.FindProperty("fuck");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tps, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        EditorGUIUtility.LookLikeControls();
        // ...
    }
}
*/
