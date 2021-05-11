using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipHandler : MonoBehaviour
{
    public  TextMeshProUGUI typeTMP;
    [SerializeField] Image typeImg;

    public virtual void AssignType(CardBase.CardType type)
    {
        switch (type)
        {
            //Adventure
            case CardBase.CardType.Location: typeImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Location"; break;
            case CardBase.CardType.Goal: typeImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Goal"; break;
            case CardBase.CardType.Villian: typeImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Villian"; break;
            case CardBase.CardType.Enemy: typeImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Enemy"; break;
            case CardBase.CardType.Twist: typeImg.color = Data.instance.artRef.twistTip; typeTMP.text = "Twist"; break;
            //Character
            case CardBase.CardType.Race: typeImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Race"; break;
            case CardBase.CardType.Background: typeImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Background"; break;
            case CardBase.CardType.Class: typeImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Class"; break;
            case CardBase.CardType.Drive: typeImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Drive"; break;
            case CardBase.CardType.Stats: typeImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Stats"; break;
            //Dungeon
            case CardBase.CardType.Entrance: typeImg.color = Data.instance.artRef.entranceTip; typeTMP.text = "Entrance"; break;
            case CardBase.CardType.Combat: typeImg.color = Data.instance.artRef.combatTip; typeTMP.text = "Combat"; break;
            case CardBase.CardType.Challenge: typeImg.color = Data.instance.artRef.challengeTip; typeTMP.text = "Challenge"; break;
            case CardBase.CardType.Setback: typeImg.color = Data.instance.artRef.setbackTip; typeTMP.text = "Setback"; break;
            case CardBase.CardType.Boss: typeImg.color = Data.instance.artRef.climaxTip; typeTMP.text = "Boss"; break;
        }

    }
}
