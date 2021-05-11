using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorEditor : MonoBehaviour
{
    CardBase.CardType cardType;
    bool dataTypeDefault = true;
    [SerializeField] TextMeshProUGUI titleInput, descInput, typeText;
    [SerializeField] List<GameObject> subListItems;
    [SerializeField] ListItemHandler handler;
    [SerializeField] Toggle suffixPrefix;

    Edit_Card selectedCard;

    public void ChangeCard(Edit_Card newCard)
    {
        if (selectedCard == newCard) { return; }
        selectedCard = newCard;
        //visual changes
        DisplaySelected();
    }

    public void DisplaySelected()
    {
        Card_Data data = selectedCard.GetData();
        titleInput.GetComponentInParent<TMP_InputField>().text = data.titleData;
        typeText.text = data.cardType.ToString();

        if (data.cardType == CardBase.CardType.Race)
        {
            //sublist
            CData_Sublist datanew = (CData_Sublist)data;
            foreach (GameObject item in subListItems) { item.SetActive(true); }
            suffixPrefix.isOn = datanew.suffix;
            string newStr =  (!datanew.suffix)?   data.titleData.Remove(0,1) : data.titleData.Remove(data.titleData.Length-1,1);
            titleInput.GetComponentInParent<TMP_InputField>().text = newStr;

            handler.DeleteAll();
            foreach (string sub in datanew.sublist)
            {
                handler.OnAdd(sub);
            }

            descInput.GetComponentInParent<TMP_InputField>().text = datanew.descData;
        }
        else
        {
            //Default
            titleInput.GetComponentInParent<TMP_InputField>().text = data.titleData;
            handler.DeleteAll();
            CData_Default datanew = (CData_Default)data;
            foreach (GameObject item in subListItems) { item.SetActive(false); }
            descInput.GetComponentInParent<TMP_InputField>().text = datanew.descData;
        }
    }

    public void DisplayClear()
    {
        titleInput.GetComponentInParent<TMP_InputField>().text = "";
        typeText.text = "";
        foreach (GameObject item in subListItems) { item.SetActive(false); }
        descInput.GetComponentInParent<TMP_InputField>().text ="";
        handler.DeleteAll();
    }


    public void OnLeave()
    {
        SceneManager.UnloadSceneAsync(4);
    }

    public void OnSave()
    {
        //create new data
        if (selectedCard.GetData().cardType == CardBase.CardType.Race)
        {
            CData_Sublist replacementData;
            replacementData = NewSublist();
            //update everyone
            selectedCard.Initiate(replacementData);
            Data.instance.UpdateCard(replacementData);
        }
        else
        {
            CData_Default replacementData;
            replacementData = NewDefault();
            //update everyone
            selectedCard.Initiate(replacementData);
            Data.instance.UpdateCard(replacementData);
        }
        Data.instance.SaveCards();
        UpdateData();
    }

    CData_Default NewDefault()
    {
        CData_Default oldDat = (CData_Default)selectedCard.GetData();
        //create new data
        CData_Default newData = ScriptableObject.CreateInstance<CData_Default>();
        newData.inPool = oldDat.inPool;
        newData.cardType = oldDat.cardType;
        newData.Custom = oldDat.Custom;
        newData.name = oldDat.name;
        newData.customID = oldDat.customID;
        newData.spriteData = oldDat.spriteData;
        //assign inputs to new data
        newData.titleData = titleInput.text;
        newData.descData = descInput.text;

        return newData;
    }


    CData_Sublist NewSublist()
    {
        CData_Sublist oldDat = (CData_Sublist)selectedCard.GetData();
        //create new data
        CData_Sublist newData = ScriptableObject.CreateInstance<CData_Sublist>();
        newData.inPool = oldDat.inPool;
        newData.cardType = oldDat.cardType;
        newData.Custom = oldDat.Custom;
        newData.name = oldDat.name;
        newData.customID = oldDat.customID;
        newData.spriteData = oldDat.spriteData;
        //assign inputs to new data
        newData.descData = descInput.text;
        newData.sublist = handler.GetListItems();
        newData.suffix = suffixPrefix.isOn;
        newData.titleData = (newData.suffix) ? titleInput.text + " " : " " + titleInput.text;

        return newData;
    }

    public void OnDelete()
    {
        DisplayClear();
        DeckBuilderHandler.instance.DeleteCard(selectedCard.GetData());
        selectedCard.OnDelete();
        UpdateData();
    }

    public void UpdateData()
    {
        Data.instance.Save();
    }
}
