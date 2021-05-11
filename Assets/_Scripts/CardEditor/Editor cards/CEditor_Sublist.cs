using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CEditor_Sublist : Card_Editor
{
    CData_Sublist cData;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] TextMeshProUGUI list;
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

    void AssignData()
    {
        title.text = cData.titleData;
        desc.text = (cData.descData.Length == 1) ? cData.descData : '"' + cData.descData + '"';
        string listString = "";
        foreach (string sub in cData.sublist)
        {
            listString += sub + "\n";
        }
        list.text = listString;
    }

    public override void OnButtonPress()
    {
        if (!Data.instance.CanDisable(cData)) { return; }
        cData.inPool = !cData.inPool;
        if (cData.inPool)
        {
            hider.SetActive(false);
            buttonText.text = "Disable";
        }
        else
        {
            hider.SetActive(true);
            buttonText.text = "Enable";
        }
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
