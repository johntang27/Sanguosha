using System;
using System.Collections;
using UnityEngine;

public class CardDataMaker : MonoBehaviour
{

    public string fileName;
    public CardData[] cardData;

    public void ExportData(string path)
    {
        string savePath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + fileName + ".xml");
        CardDataCollection dataCollection = new CardDataCollection();
        dataCollection.data = cardData;

        dataCollection.Save(path);
        Debug.Log("Card Data saved to: " + path);
    }

    public void LoadData(string path)
    {
        try
        {
            string readPath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + fileName + ".xml");
            CardDataCollection dataCollection = CardDataCollection.Load(path);
            Debug.Log("Card Data Xml read from: " + path);
            cardData = dataCollection.data;
        }
        catch (Exception e)
        {
            CardMakerUIManager.Instance.UpdateMessage("File not found");
        }
    }
}
