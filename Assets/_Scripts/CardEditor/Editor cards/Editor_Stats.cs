using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor_Stats : MonoBehaviour
{
    [SerializeField] Button bDefault, bColville, bAnarchy;
    void Awake()
    {
        UpdateRollType((int)Data.instance.statType);
    }


    public void UpdateRollType(int input)
    {

        Data.instance.statType = (Data.StatType)input;

        switch (Data.instance.statType)
        {
            case Data.StatType.Colville: bDefault.interactable = true; bColville.interactable = false; bAnarchy.interactable = true; break;
            case Data.StatType.Anarchy: bDefault.interactable = true; bColville.interactable = true; bAnarchy.interactable = false; break;
            default: bDefault.interactable = false; bColville.interactable = true; bAnarchy.interactable = true;break;
        }

    }

}
