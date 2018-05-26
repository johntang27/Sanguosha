using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMakerUIManager : Singleton<CardMakerUIManager> {

    [Header("UI Elements")]
    public InputField fileNameInput;
    public InputField cardNameInput;
    public InputField cardDescriptionInput;
    public Toggle[] cardTypeToggles;
    public Toggle[] factionToggles;
    public GameObject disableFaction;
    public Transform content;
    public CardMakerButton cardButton;
    public GameObject messageWindow;
    public Text messageText;
    [Header("Variables")]
    public CardDataMaker cardMaker;
    public CardData currentCard;
    public List<CardData> cardList = new List<CardData>();
    private int currentTypeIndex = 0;
    private int currentFactionIndex = 1;
    private int currentCardSelected = 0;

    private void Start()
    {
        currentCard = new CardData();
        currentCard.cardType = CardType.Basics;
        cardTypeToggles[0].isOn = true;
        currentCard.heroFaction = HeroFaction.None;
        disableFaction.SetActive(true);
    }

    void ToggleChoices(int currentIndex, bool isCardType)
    {
        if(isCardType)
        {
            if (currentIndex == currentTypeIndex) return;
            cardTypeToggles[currentTypeIndex].isOn = false;
            currentTypeIndex = currentIndex;
        }
        else
        {
            if (currentIndex == currentFactionIndex) return;
            factionToggles[currentFactionIndex - 1].isOn = false;
            currentFactionIndex = currentIndex;
        }
    }

    public void CardNameOnSubmit()
    {
        currentCard.cardName = cardNameInput.text;
    }

    public void CardDescriptionOnSubmit()
    {
        currentCard.cardDescription = cardDescriptionInput.text;
    }

    public void FileNameOnSubmit()
    {
        cardMaker.fileName = fileNameInput.text;
    }

    public void CardTypesToggle(int toggleIndex)
    {
        ToggleChoices(toggleIndex, true);
        currentCard.cardType = (CardType)toggleIndex;
        disableFaction.SetActive(toggleIndex != 5);
        if (toggleIndex != 5)
            currentCard.heroFaction = HeroFaction.None;
        else
            currentCard.heroFaction = (HeroFaction)(currentFactionIndex);
    }

    public void FactionToggle(int toggleIndex)
    {
        ToggleChoices(toggleIndex, false);
        currentCard.heroFaction = (HeroFaction)(toggleIndex);        
    }

    public void AddCardToList()
    {
        if(cardList != null)
        {
            cardList.Add(currentCard);
            CardMakerButton btn = Instantiate(cardButton) as CardMakerButton;
            btn.transform.SetParent(content);
            btn.transform.localScale = Vector3.one;
            btn.Init(currentCard);
            currentCard = new CardData();
            currentCard.cardType = (CardType)currentTypeIndex;
            if (currentCard.cardType == CardType.Heroes)
                currentCard.heroFaction = (HeroFaction)(currentFactionIndex);
            else
                currentCard.heroFaction = HeroFaction.None;
            cardNameInput.text = "";
            cardDescriptionInput.text = "";
        }
    }

    public void RemoveCardFromList()
    {
        if(cardList.Contains(currentCard))
        {
            cardList.Remove(currentCard);
            Destroy(content.GetChild(currentCardSelected).gameObject);
        }
    }

    public void UpdateCardUI(CardData data)
    {
        currentCard = data;
        cardNameInput.text = data.cardName;
        cardDescriptionInput.text = data.cardDescription;
        currentFactionIndex = (int)(data.heroFaction);
        cardTypeToggles[(int)data.cardType].isOn = true;
        if (data.cardType == CardType.Heroes)
            factionToggles[(int)(data.heroFaction) - 1].isOn = true;
        currentCardSelected = cardList.IndexOf(data);
    }

    public void ExportCardList()
    {
        if (fileNameInput.text ==  "")
        {
            cardMaker.fileName = "NewFile";
        }
        else
        {
            cardMaker.fileName = fileNameInput.text;
        }

        string savePath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + cardMaker.fileName + ".xml");

        cardMaker.cardData = cardList.ToArray();
        cardMaker.ExportData(savePath);
        UpdateMessage("Exporting to\n" + savePath);
        ResetUI();
    }

    public void LoadCardList()
    {
        ResetUI();
        string loadPath = System.Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + cardMaker.fileName + ".xml");
        UpdateMessage("Load from\n" + loadPath);
        cardMaker.LoadData(loadPath);
        if(cardMaker.cardData.Length > 0)
        {
            for(int i = 0; i < cardMaker.cardData.Length; i++)
            {
                currentCard = cardMaker.cardData[i];
                AddCardToList();
            }
        }
    }

    public void ResetUI()
    {
        cardNameInput.text = "";
        cardDescriptionInput.text = "";
        fileNameInput.text = "";

        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        cardList.Clear();

        currentCard = new CardData();
        cardTypeToggles[0].isOn = true;
        factionToggles[currentFactionIndex - 1].isOn = false;
        currentFactionIndex = 1;
        currentTypeIndex = 0;
        currentCardSelected = 0;
        currentCard.cardType = (CardType)currentTypeIndex;
        currentCard.heroFaction = HeroFaction.None;       
    }

    public void UpdateMessage(string msg)
    {
        messageText.text = msg;
        StartCoroutine(DisplayMessageWindow());
    }
    
    IEnumerator DisplayMessageWindow()
    {
        messageWindow.SetActive(true);
        yield return new WaitForSeconds(2f);
        messageWindow.SetActive(false);
    }

    public void LoadExample()
    {
        CardDataCollection dc = CardDataCollection.LoadFromResources("Tactics");
        for(int i = 0; i < dc.data.Length; i++)
        {
            currentCard = dc.data[i];
            AddCardToList();
        }
        ToggleUIInteraction(false);
    }

    void ToggleUIInteraction(bool on)
    {
        cardNameInput.interactable = on;
        cardDescriptionInput.interactable = on;
        for (int i = 0; i < cardTypeToggles.Length; i++)
        {
            cardTypeToggles[i].interactable = on;
        }
        for(int i = 0; i < factionToggles.Length; i++)
        {
            factionToggles[i].interactable = on;
        }
    }
}
