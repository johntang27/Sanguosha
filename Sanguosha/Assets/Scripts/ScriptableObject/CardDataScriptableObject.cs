using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/BaseCard")]
public class CardDataScriptableObject : ScriptableObject
{
    [SerializeField] protected string cardName;
    [SerializeField] protected CardType cardType;
    [Multiline] [SerializeField] protected string cardDescription;
    [SerializeField] protected Sprite cardSprite;

    protected string xmlFileName = "";
    protected string heroSpritePath = "";

    public CardType CardType => cardType;

    public virtual void SetScriptableObjectData()
    {
        string cardName = this.name;
        if(cardType != CardType.Heroes) xmlFileName = cardType.ToString();

        string readPath = Application.dataPath + "/Resources/" + xmlFileName + ".xml";
        CardDataCollection dataCollection = CardDataCollection.Load(readPath);
        CardData data = dataCollection.data.ToList().FirstOrDefault(x => x.cardName == cardName);
        if (data != null)
        {
            this.cardName = data.cardName;
            this.cardDescription = data.cardDescription;
            string spritePath = "Assets/Sprites/CardSprites/" + cardType.ToString() + "/" + heroSpritePath + cardName + ".jpg";
            if (!File.Exists(spritePath))
            {
                Debug.LogErrorFormat("Sprite Path not found for {0} at path: {1}", cardName, spritePath);
            }
            else
            {
                cardSprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(spritePath, typeof(Sprite));
            }
        }
        else
        {
            Debug.LogError("Hero data not found for " + cardName);
        }
    }
}
