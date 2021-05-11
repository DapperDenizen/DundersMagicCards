using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpriteTipHandler : TipHandler
{
    [SerializeField] SpriteRenderer typeSImg, typeSImgBack;

    public override void AssignType(CardBase.CardType type)
    {

        switch (type)
        {
            //Adventure
            case CardBase.CardType.Location: typeSImgBack.color = Data.instance.artRef.adventureTip;  typeSImg.sprite = Data.instance.artRef.locationSTip; break;
            case CardBase.CardType.Goal: typeSImgBack.color = Data.instance.artRef.adventureTip; typeSImg.sprite = Data.instance.artRef.goalSTip; break;
            case CardBase.CardType.Villian: typeSImgBack.color = Data.instance.artRef.adventureTip; typeSImg.sprite = Data.instance.artRef.villianSTip;  break;
            case CardBase.CardType.Enemy: typeSImgBack.color = Data.instance.artRef.adventureTip; typeSImg.sprite = Data.instance.artRef.enemySTip; break;
            case CardBase.CardType.Twist: typeSImgBack.color = Data.instance.artRef.twistTip; typeSImg.sprite = Data.instance.artRef.twistSTip; break;
            //Character
            case CardBase.CardType.Race: typeSImgBack.color = Data.instance.artRef.characterTip; typeSImg.sprite = Data.instance.artRef.raceSTip;  break;
            case CardBase.CardType.Background: typeSImgBack.color = Data.instance.artRef.characterTip; typeSImg.sprite = Data.instance.artRef.backgroundSTip; break;
            case CardBase.CardType.Class: typeSImgBack.color = Data.instance.artRef.characterTip; typeSImg.sprite = Data.instance.artRef.classSTip;  break;
            case CardBase.CardType.Drive: typeSImgBack.color = Data.instance.artRef.characterTip; typeSImg.sprite = Data.instance.artRef.driveSTip;  break;
            case CardBase.CardType.Stats: typeSImgBack.color = Data.instance.artRef.characterTip; typeSImg.sprite = Data.instance.artRef.statsSTip; break;
            //Dungeon
            case CardBase.CardType.Entrance: typeSImgBack.color = Data.instance.artRef.entranceTip; typeSImg.sprite = Data.instance.artRef.entranceSTip;  break;
            case CardBase.CardType.Combat: typeSImgBack.color = Data.instance.artRef.combatTip; typeSImg.sprite = Data.instance.artRef.combatSTip;  break;
            case CardBase.CardType.Challenge: typeSImgBack.color = Data.instance.artRef.challengeTip; typeSImg.sprite = Data.instance.artRef.challengeSTip;  break;
            case CardBase.CardType.Setback: typeSImgBack.color = Data.instance.artRef.setbackTip; typeSImg.sprite = Data.instance.artRef.setbackSTip;  break;
            case CardBase.CardType.Boss: typeSImgBack.color = Data.instance.artRef.climaxTip; typeSImg.sprite = Data.instance.artRef.climaxSTip;  break;
        }
        /*
        switch (type)
        {
            //Adventure
            case CardBase.CardType.Location: typeSImgBack.color = typeSImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Location"; break;
            case CardBase.CardType.Goal: typeSImgBack.color = typeSImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Goal"; break;
            case CardBase.CardType.Villian: typeSImgBack.color = typeSImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Villian"; break;
            case CardBase.CardType.Enemy: typeSImgBack.color = typeSImg.color = Data.instance.artRef.adventureTip; typeTMP.text = "Enemy"; break;
            case CardBase.CardType.Twist: typeSImgBack.color = typeSImg.color = Data.instance.artRef.twistTip; typeTMP.text = "Twist"; break;
            //Character
            case CardBase.CardType.Race: typeSImgBack.color = typeSImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Race"; break;
            case CardBase.CardType.Background: typeSImgBack.color = typeSImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Background"; break;
            case CardBase.CardType.Class: typeSImgBack.color = typeSImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Class"; break;
            case CardBase.CardType.Drive: typeSImgBack.color = typeSImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Drive"; break;
            case CardBase.CardType.Stats: typeSImgBack.color = typeSImg.color = Data.instance.artRef.characterTip; typeTMP.text = "Stats"; break;
            //Dungeon
            case CardBase.CardType.Entrance: typeSImgBack.color = typeSImg.color = Data.instance.artRef.entranceTip; typeTMP.text = "Entrance"; break;
            case CardBase.CardType.Combat: typeSImgBack.color = typeSImg.color = Data.instance.artRef.combatTip; typeTMP.text = "Combat"; break;
            case CardBase.CardType.Challenge: typeSImgBack.color = typeSImg.color = Data.instance.artRef.challengeTip; typeTMP.text = "Challenge"; break;
            case CardBase.CardType.Setback: typeSImgBack.color = typeSImg.color = Data.instance.artRef.setbackTip; typeTMP.text = "Setback"; break;
            case CardBase.CardType.Boss: typeSImgBack.color = typeSImg.color = Data.instance.artRef.climaxTip; typeTMP.text = "Boss"; break;
        }
        //*/
    }
}
