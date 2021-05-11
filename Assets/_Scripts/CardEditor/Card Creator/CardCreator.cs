using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CardCreator : MonoBehaviour
{
    CardBase.CardType cardType;
    bool dataTypeDefault = true;
    [SerializeField] TextMeshProUGUI titleInput, DescInput;
    [SerializeField] List<GameObject> subListItems;
    [SerializeField] ListItemHandler handler;
    [SerializeField] Toggle suffixPrefix;
    [SerializeField] TextMeshProUGUI typeText;

    public void OnButtonCreate()
    {
        if (dataTypeDefault)
        {
            //Generate Card
            CData_Default newCard = ScriptableObject.CreateInstance<CData_Default>();
            newCard.titleData = titleInput.text;
            newCard.descData = DescInput.text;
            newCard.spriteData = Data.instance.defaultImage;
            newCard.Custom = true;
            newCard.inPool = true;
            newCard.cardType = cardType;
            newCard.name = "Custom_" + titleInput.text;
            newCard.customID = newCard.titleData.ToUpper() + "_CUSTOM_" + System.DateTime.Now.ToString("d");
            AddCard(newCard);
        }
        else
        {
            //Generate Card
            CData_Sublist newCard = ScriptableObject.CreateInstance<CData_Sublist>();
            newCard.descData = DescInput.text;
            newCard.sublist = handler.GetListItems();
            newCard.suffix = !suffixPrefix.isOn;
            newCard.titleData = (newCard.suffix) ?  titleInput.text + " ": " " + titleInput.text ;
            newCard.spriteData = Data.instance.defaultImage;
            newCard.cardType = cardType;
            newCard.Custom = true;
            newCard.inPool = true;
            newCard.name = "Custom_" + titleInput.text;
            newCard.customID = newCard.titleData.ToUpper() + "_CUSTOM_" + System.DateTime.Now.ToString("d");
            AddCard(newCard);
        }
    }

    void AddCard(CData_Default newCard)
    {
        switch (newCard.cardType)
        {
            case CardBase.CardType.Background: Data.instance.backGrounds_Data.Add(newCard);break;
            case CardBase.CardType.Class: Data.instance.classes_Data.Add(newCard); break;
            case CardBase.CardType.Drive: Data.instance.drive_Data.Add(newCard); break;
            case CardBase.CardType.Enemy: Data.instance.enemy_Data.Add(newCard); break;
            case CardBase.CardType.Goal: Data.instance.goal_Data.Add(newCard); break;
            case CardBase.CardType.Location: Data.instance.locations_Data.Add(newCard); break;
            case CardBase.CardType.Twist: Data.instance.twist_Data.Add(newCard); break;
            case CardBase.CardType.Entrance: Data.instance.entrance_Data.Add(newCard); break;
            case CardBase.CardType.Challenge: Data.instance.challenge_Data.Add(newCard); break;
            case CardBase.CardType.Setback: Data.instance.setback_Data.Add(newCard); break;
            case CardBase.CardType.Combat: Data.instance.combat_Data.Add(newCard); break;
            case CardBase.CardType.Boss: Data.instance.boss_Data.Add(newCard); break;
            default: Data.instance.villian_Data.Add(newCard); break;
        }
        //Save card
        string cardID = newCard.customID;
        PlayerPrefs.SetString(cardID, JsonUtility.ToJson(newCard));
        Data.instance.customDCardNames.Add(cardID);
        AddComplete();
    }
    void AddCard(CData_Sublist newCard)
    {
        Data.instance.races_Data.Add(newCard);
        //Save card
        string cardID = newCard.titleData.ToUpper() + "_CUSTOM_" + System.DateTime.Now.ToString("d");
        PlayerPrefs.SetString(cardID, JsonUtility.ToJson(newCard));
        AddComplete();
    }

    void AddComplete()
    {
        DeckBuilderHandler.instance.AddNew(cardType);
        Data.instance.Save();
        OnButtonExit();
    }

    public void ResetValues()
    {
        titleInput.text = "";
        DescInput.text = "";
        suffixPrefix.isOn = true;
    }

    public void TypeChange(Dropdown value)
    {
        CardBase.CardType newType;
        bool dtypeDefault = true;
        switch (value.value)
        {
            case 0: newType = CardBase.CardType.Location; dtypeDefault = true; break;
            case 1: newType = CardBase.CardType.Goal; dtypeDefault = true; break;
            case 2: newType = CardBase.CardType.Villian; dtypeDefault = true; break;
            case 3: newType = CardBase.CardType.Enemy; dtypeDefault = true; break;
            case 4: newType = CardBase.CardType.Twist; dtypeDefault = true; break;
            case 5: newType = CardBase.CardType.Race; dtypeDefault = false; break;
            case 6: newType = CardBase.CardType.Background; dtypeDefault = true; break;
            case 7: newType = CardBase.CardType.Class; dtypeDefault = true; break;
            case 8: newType = CardBase.CardType.Drive; dtypeDefault = true; break;
            case 9: newType = CardBase.CardType.Entrance; dtypeDefault = true; break;
            case 10: newType = CardBase.CardType.Challenge; dtypeDefault = true; break;
            case 11: newType = CardBase.CardType.Setback; dtypeDefault = true; break;
            case 12: newType = CardBase.CardType.Combat; dtypeDefault = true; break;
            default: newType = CardBase.CardType.Boss; dtypeDefault = true; break;

        }
        if (dtypeDefault != dataTypeDefault)
        {
            //Data change!
            dataTypeDefault = dtypeDefault;
            ShowHideList();

        }
        cardType = newType;
    }

    public void TypeChange(int value)
    {
        CardBase.CardType newType;
        bool dtypeDefault = true;
        switch (value)
        {
            case 0: newType = CardBase.CardType.Location; dtypeDefault = true; break;
            case 1: newType = CardBase.CardType.Goal; dtypeDefault = true; break;
            case 2: newType = CardBase.CardType.Villian; dtypeDefault = true; break;
            case 3: newType = CardBase.CardType.Enemy; dtypeDefault = true; break;
            case 4: newType = CardBase.CardType.Race; dtypeDefault = false; break;
            case 5: newType = CardBase.CardType.Background; dtypeDefault = true; break;
            case 6: newType = CardBase.CardType.Class; dtypeDefault = true; break;
            case 7: newType = CardBase.CardType.Drive; dtypeDefault = true; break;
            case 8: newType = CardBase.CardType.Entrance; dtypeDefault = true; break;
            case 9: newType = CardBase.CardType.Combat; dtypeDefault = true; break;
            case 10: newType = CardBase.CardType.Challenge; dtypeDefault = true; break;
            case 11: newType = CardBase.CardType.Setback; dtypeDefault = true; break;
            case 12: newType = CardBase.CardType.Boss; dtypeDefault = true; break;
            default: newType = CardBase.CardType.Twist; dtypeDefault = true; break;

        }
        if (dtypeDefault != dataTypeDefault)
        {
            //Data change!
            dataTypeDefault = dtypeDefault;
            ShowHideList();

        }
        cardType = newType;
        typeText.text = cardType.ToString();
    }

    public int GetTypeIndex()
    {
        switch (cardType)
        {
            case CardBase.CardType.Location: return 0;
            case CardBase.CardType.Goal: return 1;
            case CardBase.CardType.Villian: return 2;
            case CardBase.CardType.Enemy: return 3;
            case CardBase.CardType.Race: return 4;
            case CardBase.CardType.Background: return 5;
            case CardBase.CardType.Class: return 6;
            case CardBase.CardType.Drive: return 7;
            case CardBase.CardType.Entrance: return 8;
            case CardBase.CardType.Combat: return 9;
            case CardBase.CardType.Challenge: return 10;
            case CardBase.CardType.Setback: return 11;
            case CardBase.CardType.Boss: return 12;
            default: return 13;
        }
    }

    void ShowHideList()
    {
        foreach (GameObject obj in subListItems)
        {
            obj.SetActive(!dataTypeDefault);
        }
    }

    public void OnButtonExit()
    {
        SceneManager.UnloadSceneAsync(3);
    }

}
