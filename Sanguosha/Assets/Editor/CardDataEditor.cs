using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardDataMaker))]
public class CardDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Export to xml", GUILayout.MaxWidth(200)))
        {
            Save();
        }
        //		Read from file
        if (GUILayout.Button("Read From xml", GUILayout.MaxWidth(200)))
        {
            Read();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    void Save()
    {
        string savePath = Application.dataPath + "/Resources/" + ((CardDataMaker)target).fileName + ".xml";
        CardDataCollection dataCollection = new CardDataCollection();
        dataCollection.data = ((CardDataMaker)target).cardData;

        dataCollection.Save(savePath);
        Debug.Log("Card Data saved to: " + savePath);
    }

    void Read()
    {
        string readPath = Application.dataPath + "/Resources/" + ((CardDataMaker)target).fileName + ".xml";
        CardDataCollection dataCollection = CardDataCollection.Load(readPath);
        Debug.Log("Card Data Xml read from: " + readPath);
        ((CardDataMaker)target).cardData = dataCollection.data;
    }
}
