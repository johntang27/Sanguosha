using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroCard", menuName = "ScriptableObjects/HeroCard")]
public class HeroCardDataScriptableObject : CardDataScriptableObject
{
    [SerializeField] protected HeroFaction heroFaction;

    public HeroFaction HeroFaction => heroFaction;

    public override void SetScriptableObjectData()
    {
        xmlFileName = "heroes_" + this.heroFaction.ToString().ToLower();
        heroSpritePath = heroFaction.ToString() + "/";
        base.SetScriptableObjectData();
    }
}
