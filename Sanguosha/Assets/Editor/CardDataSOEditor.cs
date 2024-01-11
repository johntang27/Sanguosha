using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDataScriptableObject))]
public class CardDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //Read from file
        if (GUILayout.Button("Read From xml", GUILayout.MaxWidth(200)))
        {
            Read();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    protected virtual void Read()
    {
        //string cardName = ((CardDataScriptableObject)target).name;
        //CardType currentType = ((CardDataScriptableObject)target).CardType;
        //string fileName = currentType != CardType.Heroes ? currentType.ToString() : "heroes_";
        //string readPath = Application.dataPath + "/Resources/" + fileName + ".xml";
        //CardDataCollection dataCollection = CardDataCollection.Load(readPath);
        //CardData data = dataCollection.data.ToList().FirstOrDefault(x => x.cardName == cardName);
        //if (data != null) ((CardDataScriptableObject)target).SetScriptableObjectData(data);

        ((CardDataScriptableObject)target).SetScriptableObjectData();
    }
}
