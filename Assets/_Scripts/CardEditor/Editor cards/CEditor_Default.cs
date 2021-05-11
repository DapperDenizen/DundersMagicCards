using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CEditor_Default : Card_Editor
{
    CData_Default cData;

    [SerializeField] TextMeshProUGUI desc;
    public override Card_Editor Initiate(Card_Data data)
    {
        this.cData = (CData_Default)data;
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
