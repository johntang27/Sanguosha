using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMakerButton : MonoBehaviour {

    public Text cardNameText;
    public CardData cardData;

    public void Init(CardData data)
    {
        cardData = data;
        cardNameText.text = cardData.cardName;
    }

    public void ButtonClicked()
    {
        CardMakerUIManager.Instance.UpdateCardUI(cardData);
    }
}
