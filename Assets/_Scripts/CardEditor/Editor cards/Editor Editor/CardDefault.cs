using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDefault : Edit_Card
{
    CData_Default cData;

    public void InitCustom(EditorEditor ed, Card_Data dat)
    {
        editor = ed;
        Initiate(dat);
    }

    public override Card_Editor Initiate(Card_Data data)
    {
        this.cData = (CData_Default)data;
        AssignData();
        if (cData.Custom)
        {
            customMarking.SetActive(true);
        }
        return this;
    }

    public override void OnDelete()
    {
        Data.instance.RemoveCard(cData);
        Destroy(gameObject);
    }

    public override void AssignData()
    {
        title.text = cData.titleData;
        desc.text = '"' + cData.descData + '"';
    }


    public override Card_Data GetData()
    {
        return (Card_Data)cData;
    }

    public override bool CardCheck(Card_Data data)
    {
        if (data == (Card_Data)cData) { Destroy(gameObject);  return true; }
        return false;
    }
}
