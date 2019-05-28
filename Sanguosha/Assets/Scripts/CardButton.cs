using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour {

    public Image cardImage;
    public CardData cardData;
    public string spritePath;

    public void Init(CardData data, string folderName = "")
    {
        cardData = data;
        string spriteName = cardData.cardName;
        if (cardData.portraitOverride != "") spriteName = cardData.portraitOverride;
        spriteName = spriteName.Replace(" ", "_");

        if(folderName != "")
            spritePath = data.cardType.ToString() + "/" + folderName + "/" + spriteName.ToLower();
        else
            spritePath = data.cardType.ToString() + "/" + spriteName.ToLower();

        cardImage.sprite = Resources.Load(spritePath, typeof(Sprite)) as Sprite;
    }

    public void ViewCard()
    {
        UIManager.Instance.UpdateCardView(cardData, spritePath);
    }
}
