using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataUpdater : MonoBehaviour
{
    [SerializeField] private CardDataScriptableObject[] cardDataScriptableObjects;

    public void UpdateAllDataFromXml()
    {
        if(cardDataScriptableObjects.Length > 0)
        {
            for(int i =  0; i < cardDataScriptableObjects.Length; i++)
            {
                cardDataScriptableObjects[i].SetScriptableObjectData();
            }
        }
    }
}
