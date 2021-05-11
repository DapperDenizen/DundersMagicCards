using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HorizontalRefs : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Transform myContent;
    public GameObject myObj;
    //This is sloppy but couldn't think of a nice way to do this
    List<Card_Data> datas;
    //Later on i should replace the entire pipeline to use Card data as its base and then cast it when we get to the end
    GameObject prefab;
    List<Card_Editor> cards = new List<Card_Editor>();
    public void Setup(List<Card_Data> datas, GameObject prefab, string label)
    {
        InitCards(datas, prefab);
        titleText.text = label;
    }
    
    public void Setup(List<Card_Data> datas, GameObject prefab)
    {
        InitCards(datas, prefab);
    }

    void InitCards(List<Card_Data> datas, GameObject prefab)
    {
        this.datas = (List<Card_Data>)datas;
        this.prefab = prefab;
        for (int i = 0; i < datas.Count; i++)
        {
            AddCard(i);
        }
    }

    public void AddCard( int index)
    {
      cards.Add(GameObject.Instantiate(prefab, myContent).GetComponent<Card_Editor>().Initiate(datas[index]));
    } 

    public void AddCard(Card_Data data)
    {
        cards.Add(GameObject.Instantiate(prefab, myContent).GetComponent<Card_Editor>().Initiate(data));
    }

    public void RemoveCard(Card_Data data)
    {
        foreach (Card_Editor card in cards)
        {
            if (card.CardCheck(data)) { return; }
        }
    }

    public void ShowHide()
    {
        myObj.SetActive(!myObj.activeInHierarchy);
    }
    public void ShowHide(bool showhide)
    {
        myObj.SetActive(showhide);
    }
}
