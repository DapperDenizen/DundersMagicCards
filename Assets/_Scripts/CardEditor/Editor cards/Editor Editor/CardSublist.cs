using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSublist : Edit_Card
{
    CData_Sublist cData;
    [SerializeField] TextMeshProUGUI list;

    public void InitCustom(EditorEditor ed, Card_Data dat)
    {
        editor = ed;
        Initiate(dat);
    }

    public override Card_Editor Initiate(Card_Data data)
    {
        this.cData = (CData_Sublist)data;
        cData.inPool = !cData.inPool;
        OnButtonPress();
        AssignData();
        if (cData.Custom)
        {
            customMarking.SetActive(true);
        }
        return this;
    }

    public override void AssignData()
    {
        title.text = cData.titleData;
        desc.text = '"' + cData.descData + '"';
        string listString = "";
        foreach (string sub in cData.sublist)
        {
            listString += sub + "\n";
        }
        list.text = listString;
    }

    public override Card_Data GetData()
    {
        return (Card_Data)cData;
    }

    public override void OnDelete()
    {
        Data.instance.RemoveCard(cData);
        Destroy(gameObject);
    }

    public override bool CardCheck(Card_Data data)
    {
        if (data == (Card_Data)cData) { Destroy(gameObject); return true; }
        return false;
    }
}

