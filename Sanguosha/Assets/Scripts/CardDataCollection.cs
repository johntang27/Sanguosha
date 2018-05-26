using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public enum CardType
{
    Basics = 0,
    Tactics = 1,
    DelayTactics = 2,
    Weapons = 3,
    Mounts = 4,
    Heroes = 5,
    Armors = 6
}

public enum HeroFaction
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Colorless = 4
}

[Serializable]
public class CardData
{
    public string cardName;
    public CardType cardType;
    public HeroFaction heroFaction;
    public string cardDescription;
}

[XmlRoot("CardDataCollection")]
public class CardDataCollection{

    [XmlArray("CardData"), XmlArrayItem("Card")]
    public CardData[] data;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(CardDataCollection));
        using (var stream = new FileStream(path, FileMode.Create))
            serializer.Serialize(stream, this);
    }

    public static CardDataCollection Load(string path)
    {
        var serializer = new XmlSerializer(typeof(CardDataCollection));
        using (var stream = new FileStream(path, FileMode.Open))
            return serializer.Deserialize(stream) as CardDataCollection;
    }

    public static CardDataCollection LoadFromResources(string filename)
    {
        TextAsset text = Resources.Load(filename) as TextAsset;
        var serializer = new XmlSerializer(typeof(CardDataCollection));

        using (var reader = new StringReader(text.text))
            return serializer.Deserialize(reader) as CardDataCollection;
    }
}
