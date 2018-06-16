using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [Header("Card Selection")]
    public int currentSelection = 0; //0-normal selection; 1-round selection
    public GameObject cardSelection;
    public CardButton cardBtnPrefab;
    public Transform content;
    public GameObject clearRoundButton;
    [Header("Card Viewer")]
    public GameObject cardViewer;
    public Image cardImage;
    public Text cardNameText;
    public Text descriptionText;
    public RectTransform viewerContent;
    public Button addCardButton;
    public Button removeCardButton;
    public CardData currentCard;
    [Header("Round Viewer")]
    public List<CardData> currentRoundHeroes;
    [Header("Card Data")]
    public CardData[] basicCards;
    public CardData[] tacticCards;
    public CardData[] delayTacticCards;
    public CardData[] weaponCards;
    public CardData[] armorCards;
    public CardData[] mountCards;
    public CardData[] treasureCards;
    public CardData[] heroes_redCards;
    public CardData[] heroes_greenCards;
    public CardData[] heroes_blueCards;
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

        CardDataCollection tData = CardDataCollection.LoadFromResources("Treasures");
        treasureCards = tData.data;

        CardDataCollection hrData = CardDataCollection.LoadFromResources("heroes_red");
        heroes_redCards = hrData.data;

        CardDataCollection hgData = CardDataCollection.LoadFromResources("heroes_green");
        heroes_greenCards = hgData.data;

        CardDataCollection hbData = CardDataCollection.LoadFromResources("heroes_blue");
        heroes_blueCards = hbData.data;

        CardDataCollection hcData = CardDataCollection.LoadFromResources("heroes_colorless");
        heroes_ColorlessCards = hcData.data;
    }

    public void CreateCardScrollList(string type)
    {
        currentSelection = 0;

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
            case "Treasures":
                currentData = treasureCards;
                break;
            case "Heroes_Red":
                currentData = heroes_redCards;
                pathOverride = "Red";
                break;
            case "Heroes_Green":
                currentData = heroes_greenCards;
                pathOverride = "Green";
                break;
            case "Heroes_Blue":
                currentData = heroes_blueCards;
                pathOverride = "Blue";
                break;
            case "Heroes_Colorless":
                currentData = heroes_ColorlessCards;
                pathOverride = "Colorless";
                break;
        }

        content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1.25f);

        for (int i = 0; i < currentData.Length; i++)
        {
            CardButton btn = Instantiate(cardBtnPrefab) as CardButton;
            btn.Init(currentData[i], pathOverride);
            btn.transform.SetParent(content,false);
        }
    }

    public void LoadCurrentRoundHeroes()
    {
        currentSelection = 1;

        clearRoundButton.SetActive(true);

        content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1.25f);

        for (int i = 0; i < currentRoundHeroes.Count; i++)
        {
            string pathFolder = currentRoundHeroes[i].heroFaction.ToString(); 
            CardButton btn = Instantiate(cardBtnPrefab) as CardButton;
            btn.Init(currentRoundHeroes[i], pathFolder);
            btn.transform.SetParent(content, false);
        }
    }

    public void ClearCurrentRound()
    {
        currentRoundHeroes.Clear();
        ClearScrollList();
    }

    public void ClearScrollList()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void UpdateCardView(CardData data, string spritePath)
    {
        currentCard = data;
        if (currentRoundHeroes.Contains(currentCard))
        {
            addCardButton.interactable = false;
            removeCardButton.interactable = true;
        }
        else
        {
            addCardButton.interactable = true;
            removeCardButton.interactable = false;
        }

        viewerContent.anchoredPosition = Vector2.zero;
        cardSelection.SetActive(false);
        cardViewer.SetActive(true);
        cardImage.sprite = Resources.Load(spritePath, typeof(Sprite)) as Sprite;
        cardNameText.text = data.cardName;
        descriptionText.text = data.cardDescription;
    }

    public void AddCardToCurrentRound()
    {
        if(currentCard != null)
        {            
            currentRoundHeroes.Add(currentCard);
            addCardButton.interactable = false;
            removeCardButton.interactable = true;

            if(currentSelection == 1)
            {
                string pathFolder = currentCard.heroFaction.ToString();
                CardButton btn = Instantiate(cardBtnPrefab) as CardButton;
                btn.Init(currentCard, pathFolder);
                btn.transform.SetParent(content, false);
            }
        }
    }

    public void RemoveCardFromRound()
    {
        if(currentCard != null)
        {
            int index = currentRoundHeroes.IndexOf(currentCard);
            currentRoundHeroes.Remove(currentCard);
            addCardButton.interactable = true;
            removeCardButton.interactable = false;
            Destroy(content.GetChild(index).gameObject);
        }
    }
}
