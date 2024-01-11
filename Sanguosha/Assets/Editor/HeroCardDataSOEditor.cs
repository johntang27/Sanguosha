using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HeroCardDataScriptableObject))]
public class HeroCardDataSOEditor : CardDataSOEditor
{
    protected override void Read()
    {
        //string cardName = ((HeroCardDataScriptableObject)target).name;
        //CardType currentType = ((HeroCardDataScriptableObject)target).CardType;
        //string fileName = currentType != CardType.Heroes ? currentType.ToString() : "heroes_" + ((HeroCardDataScriptableObject)target).HeroFaction.ToString().ToLower();
        //string readPath = Application.dataPath + "/Resources/" + fileName + ".xml";
        //CardDataCollection dataCollection = CardDataCollection.Load(readPath);
        //CardData data = dataCollection.data.ToList().FirstOrDefault(x => x.cardName == cardName);
        //if (data != null) ((HeroCardDataScriptableObject)target).SetScriptableObjectData(data);

        ((HeroCardDataScriptableObject)target).SetScriptableObjectData();
    }
}
