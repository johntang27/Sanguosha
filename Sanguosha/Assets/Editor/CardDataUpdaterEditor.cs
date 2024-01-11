using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDataUpdater))]
public class CardDataUpdaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //Read from file
        if (GUILayout.Button("Update All Data", GUILayout.MaxWidth(200)))
        {
            UpdateAll();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    public void UpdateAll()
    {
        ((CardDataUpdater)target).UpdateAllDataFromXml();
    }
}
