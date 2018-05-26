using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [Header("Card Selection")]
    public GameObject cardSelection;
    public CardButton cardBtnPrefab;
    public Transform content;
    [Header("Card Viewer")]
    public GameObject cardViewer;
    public Image cardImage;
    public Text cardNameText;
    public Text descriptionText;
    [Header("Card Data")]
    public CardData[] basicCards;
    public CardData[] tacticCards;
    public CardData[] delayTacticCards;
    public CardData[] weaponCards;
    public CardData[] armorCards;
    public CardData[] mountCards;
    public CardData[] heroes_ColorlessCards;

    private void Start()
    {
        CardDataCollection basicData = CardDataCollection.LoadFromResources("Basics");
        basicCards = basicData.data;

        CardDataCollection tacticData = CardDataCollection.LoadFromResources("Tactics");
        tacticCards = tacticData.data;

        CardDataCollection dtData = CardDataCollection.LoadFromResources("DelayTactics");
        delayTacticCards = dtData.data;

        CardDataCollection wData = CardDataCollection.LoadFromResources("Weapons");
        weaponCards = wData.data;

        CardDataCollection aData = CardDataCollection.LoadFromResources("Armors");
        armorCards = aData.data;

        CardDataCollection mData = CardDataCollection.LoadFromResources("Mounts");
        mountCards = mData.data;

        CardDataCollection hcData = CardDataCollection.LoadFromResources("heroes_c");
        heroes_ColorlessCards = hcData.data;
    }

    public void CreateCardScrollList(string type)
    {
        CardData[] currentData = new CardData[] { };
        string pathOverride = "";

        switch(type)
        {
            case "Basics":
                currentData = basicCards;
                break;
            case "Tactics":
                currentData = tacticCards;
                break;
            case "DelayTactics":
                currentData = delayTacticCards;
                break;
            case "Weapons":
                currentData = weaponCards;
                break;
            case "Armors":
                currentData = armorCards;
                break;
            case "Mounts":
                currentData = mountCards;
                break;
            case "Heroes_Colorless":
                currentData = heroes_ColorlessCards;
                pathOverride = "Colorless";
                break;
        }

        for(int i = 0; i < currentData.Length; i++)
        {
            CardButton btn = Instantiate(cardBtnPrefab) as CardButton;
            btn.Init(currentData[i], pathOverride);
            btn.transform.SetParent(content);
        }
    }

    public void ClearScrollList()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void UpdateCardView(CardData data, string spritePath)
    {
        cardSelection.SetActive(false);
        cardViewer.SetActive(true);
        cardImage.sprite = Resources.Load(spritePath, typeof(Sprite)) as Sprite;
        cardNameText.text = data.cardName;
        descriptionText.text = data.cardDescription;
    }
}
