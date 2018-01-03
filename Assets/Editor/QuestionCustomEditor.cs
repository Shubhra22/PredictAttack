using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(QuestionManager))]
public class QuestionCustomEditor : Editor 
{
    QuestionManager[] questionManager;
   
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("questionGenerator"), true);

        serializedObject.ApplyModifiedProperties();



    }
}
