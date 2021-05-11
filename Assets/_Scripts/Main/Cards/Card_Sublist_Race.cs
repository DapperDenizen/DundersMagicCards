using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Sublist_Race : Card_SubList
{
    [SerializeField] TextMeshProUGUI descText;
    public override void OnInitiate(CardDeck newType)
    {
        if (Data.instance.GetTypeLength(newType) == 0 && dataNumber == -1) { Data.instance.TypeEmpty(CardDeck.Race); }
        base.OnInitiate(newType: newType);
        CData_Sublist tempData = (CData_Sublist)Data.instance.GetData(CardDeck.Race, dataNumber);
        string title = tempData.titleData;
        if (tempData.hasSublist)
        {
            title = tempData.suffix ? title +tempData.sublist[Random.Range(0, tempData.sublist.Count)]: tempData.sublist[Random.Range(0, tempData.sublist.Count)] + title;
        }

        titleText.text = title;
        descText.text = (tempData.descData.Length == 1) ? tempData.descData : '"' + tempData.descData + '"';
        splashImage.sprite = tempData.spriteData;
        tipHand.AssignType(tempData.cardType);
    }

    public override void OnReroll()
    {
        OnInitiate(myDeck);
    }

}
