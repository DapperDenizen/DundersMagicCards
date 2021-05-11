using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDisplayer : MonoBehaviour
{
    [SerializeField] EditorEditor editor;
    [SerializeField] Transform myContent;
    [SerializeField] GameObject defaultPrefab;
    [SerializeField] GameObject sublistPrefab;
    List<CData_Default> customDefault = new List<CData_Default>();
    List<CData_Sublist> customSublist = new List<CData_Sublist>();

    List<GameObject> cards = new List<GameObject>();
    

    void Start()
    {
        GetCustomFromList(Data.instance.locations_Data);
        GetCustomFromList(Data.instance.goal_Data);
        GetCustomFromList(Data.instance.villian_Data);
        GetCustomFromList(Data.instance.enemy_Data);
        GetCustomFromList(Data.instance.twist_Data);
        GetCustomFromList(Data.instance.races_Data);
        GetCustomFromList(Data.instance.backGrounds_Data);
        GetCustomFromList(Data.instance.classes_Data);
        GetCustomFromList(Data.instance.drive_Data);
        GetCustomFromList(Data.instance.entrance_Data);
        GetCustomFromList(Data.instance.combat_Data);
        GetCustomFromList(Data.instance.setback_Data);
        GetCustomFromList(Data.instance.challenge_Data);
        GetCustomFromList(Data.instance.boss_Data);
        foreach (CData_Default data in customDefault) { AddCard(data); }
        foreach (CData_Sublist data in customSublist) { AddCard(data); }
        if (cards.Count > 0)
        {
            editor.ChangeCard(cards[0].GetComponent<Edit_Card>());
        }
    }

    void GetCustomFromList(List<CData_Default> newList)
    {

        foreach (CData_Default data in newList)
        {
            if (data == null) { print(data); continue; }
            if (data.Custom)
            {
                customDefault.Add(data);
            }
        }
    }

    void GetCustomFromList(List<CData_Sublist> newList)
    {

        foreach (CData_Sublist data in newList)
        {
            if (data.Custom)
            {

                customSublist.Add(data);
            }
        }
    }

    void AddCard(CData_Default data)
    {
        cards.Add(GameObject.Instantiate(defaultPrefab, myContent));
        cards[cards.Count-1].GetComponent<CardDefault>().InitCustom(editor,data);
        cards[cards.Count - 1].GetComponentInChildren<TipHandler>().AssignType(data.cardType);
    }

    void AddCard(CData_Sublist data)
    {
        cards.Add(GameObject.Instantiate(sublistPrefab, myContent));
        cards[cards.Count - 1].GetComponent<CardSublist>().InitCustom(editor,data);
        cards[cards.Count - 1].GetComponentInChildren<TipHandler>().AssignType(data.cardType);
    }

}
