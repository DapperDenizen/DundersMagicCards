using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Dungeon : CardBase
{
    public CData_Default cData;
    public GameObject hider;
    public GameObject Ink;
    [SerializeField] TextMeshProUGUI descText;
    public override void OnInitiate(CardDeck newtype)
    {
        myDeck = newtype;
        if (Data.instance.GetTypeLength(myDeck) <=1 && dataNumber != -1) { return; }
        if (Data.instance.GetTypeLength(myDeck) == 0 && dataNumber == -1) { Data.instance.TypeEmpty(newtype); }
        int newRandom = Random.Range(0, (int)Data.instance.GetTypeLength(myDeck));
        while (newRandom == dataNumber)
        {
            newRandom = Random.Range(0, (int)Data.instance.GetTypeLength(myDeck));
        }
        dataNumber = newRandom;
        cData = (CData_Default)Data.instance.GetData(myDeck, dataNumber);
        AssignData();
    }

    public override void OnReroll()
    {
        if(myDeck == CardDeck.Twist) { OnInitiate(myDeck); return; }
        cData = DungeonDeck.getCard(myDeck,cData);
        AssignData();
    }

    public void AssignData()
    {
        titleText.text = cData.titleData;
        descText.text = (cData.descData.Length == 1) ? cData.descData : '"' + cData.descData + '"';
        tipHand.AssignType(cData.cardType);
    }

    public void HiderON() { hider.SetActive(true); }
    public void HiderOFF() { hider.SetActive(false); Ink.SetActive(false); }

}