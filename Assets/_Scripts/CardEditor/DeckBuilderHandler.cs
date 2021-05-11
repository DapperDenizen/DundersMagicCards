using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeckBuilderHandler : MonoBehaviour
{
    public static DeckBuilderHandler instance;

    [SerializeField] Transform contentZone;
    //Prefabs
    //holder
    [SerializeField] GameObject holderNamePrefab;
    [SerializeField] GameObject holderDeckPrefab;
    //cards
    [SerializeField] GameObject deckCardPrefab;
    [SerializeField] GameObject defaultCardPrefab;
    [SerializeField] GameObject sublistCardPrefab;
    [SerializeField] GameObject addRemoveCardPrefab;

    Dictionary<Card_Default.CardType, HorizontalRefs> horizontals = new Dictionary<CardBase.CardType, HorizontalRefs>();
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        instance = this;
    }

    void Start()
    {
        //Adventure
        horizontals.Add(CardBase.CardType.Location, InstatiateSet("Locations", Data.instance.locations_Data));       
        horizontals.Add(CardBase.CardType.Goal, InstatiateSet("Goals", Data.instance.goal_Data));
        horizontals.Add(CardBase.CardType.Villian, InstatiateSet("Villians", Data.instance.villian_Data));
        horizontals.Add(CardBase.CardType.Enemy, InstatiateSet("Enemies", Data.instance.enemy_Data));
        horizontals.Add(CardBase.CardType.Twist, InstatiateSet("Twists", Data.instance.twist_Data));

        //PC
        horizontals.Add(CardBase.CardType.Race, InstatiateSet("Races", Data.instance.races_Data));
        horizontals.Add(CardBase.CardType.Background, InstatiateSet("Backgrounds", Data.instance.backGrounds_Data));
        horizontals.Add(CardBase.CardType.Class, InstatiateSet("Classes", Data.instance.classes_Data));
        horizontals.Add(CardBase.CardType.Drive, InstatiateSet("Drives", Data.instance.drive_Data));

        //Dungeon
        horizontals.Add(CardBase.CardType.Entrance, InstatiateSet("Entrance", Data.instance.entrance_Data));
        horizontals.Add(CardBase.CardType.Combat, InstatiateSet("Combat", Data.instance.combat_Data));
        horizontals.Add(CardBase.CardType.Setback, InstatiateSet("Setback", Data.instance.setback_Data));
        horizontals.Add(CardBase.CardType.Challenge, InstatiateSet("Challenge", Data.instance.challenge_Data));
        horizontals.Add(CardBase.CardType.Boss, InstatiateSet("Boss", Data.instance.boss_Data));

        foreach (HorizontalRefs href in horizontals.Values)
        {
            href.ShowHide(false);
        }
        InstatiateDecks();
    }

    public void ToMain()
    {
        Data.instance.Save();
        Data.instance.transitioner.TransitionBack();
    }

    public void ResetCards()
    {
        Data.instance.ResetAll();
        SceneManager.LoadScene(2);
    }

    public void AddNew(Card_Default.CardType type)
    {
        Card_Data toSend = null;

        switch (type)
        {
            case CardBase.CardType.Location: toSend = Data.instance.locations_Data[Data.instance.locations_Data.Count-1]; break;
            case CardBase.CardType.Goal: toSend = Data.instance.goal_Data[Data.instance.goal_Data.Count - 1]; break;
            case CardBase.CardType.Villian: toSend = Data.instance.villian_Data[Data.instance.villian_Data.Count - 1]; break;
            case CardBase.CardType.Enemy: toSend = Data.instance.enemy_Data[Data.instance.enemy_Data.Count - 1]; break;
            case CardBase.CardType.Twist: toSend = Data.instance.twist_Data[Data.instance.twist_Data.Count - 1]; break;
                //
            case CardBase.CardType.Race: toSend = Data.instance.races_Data[Data.instance.races_Data.Count - 1]; break;
            case CardBase.CardType.Background: toSend = Data.instance.backGrounds_Data[Data.instance.backGrounds_Data.Count - 1]; break;
            case CardBase.CardType.Class: toSend = Data.instance.classes_Data[Data.instance.classes_Data.Count - 1]; break;
                //
            case CardBase.CardType.Entrance: toSend = Data.instance.entrance_Data[Data.instance.entrance_Data.Count - 1]; break;
            case CardBase.CardType.Combat: toSend = Data.instance.combat_Data[Data.instance.combat_Data.Count - 1]; break;
            case CardBase.CardType.Challenge: toSend = Data.instance.challenge_Data[Data.instance.challenge_Data.Count - 1]; break;
            case CardBase.CardType.Setback: toSend = Data.instance.setback_Data[Data.instance.setback_Data.Count - 1]; break;
            case CardBase.CardType.Boss: toSend = Data.instance.boss_Data[Data.instance.boss_Data.Count - 1]; break;

            default: toSend = Data.instance.drive_Data[Data.instance.drive_Data.Count - 1]; break;
        }

        horizontals[type].AddCard(toSend);
        Data.instance.SaveCards();
    }

    public void DeleteCard(Card_Data card)
    {
        horizontals[card.cardType].RemoveCard(card);
    }

    void InstatiateDecks()
    {
        GameObject deckSet = GameObject.Instantiate(holderDeckPrefab, contentZone);
        deckSet.transform.SetSiblingIndex(0);
        HorizontalRefs reference = deckSet.GetComponent<HorizontalRefs>();
        //Go through horizontals and set up deck sets
        foreach (Card_Default.CardType type in horizontals.Keys)
        {
            GameObject obj = GameObject.Instantiate(deckCardPrefab, reference.myContent);
            obj.GetComponent<CardBack_Editor>().Setup(type);
            obj.transform.SetSiblingIndex(reference.myContent.childCount - 2);
        }
        GameObject addRemove = GameObject.Instantiate(addRemoveCardPrefab, reference.myContent);
        addRemove.transform.SetAsFirstSibling();
    }
    
    HorizontalRefs InstatiateSet(string name, List<CData_Default> dataList)
    {
      HorizontalRefs toReturn = GameObject.Instantiate(holderNamePrefab, contentZone).GetComponent<HorizontalRefs>();
      toReturn.Setup(ConvertToData<CData_Default>(dataList), defaultCardPrefab,name);
      return toReturn;
    }

    HorizontalRefs InstatiateSet(string name, List<CData_Sublist> dataList)
    {
        HorizontalRefs toReturn = GameObject.Instantiate(holderNamePrefab, contentZone).GetComponent<HorizontalRefs>();
        toReturn.Setup(ConvertToData<CData_Sublist>(dataList), sublistCardPrefab, name);
        return toReturn;
    }

    List<Card_Data> ConvertToData<T>(List<T> incoming)
    {
        List<Card_Data> toReturn = new List<Card_Data>();
        foreach (T item in incoming)
        {
            toReturn.Add(item as Card_Data);
        }
        return toReturn;
    }

    public void ShowDeck(CardBase.CardType deck)
    {
        horizontals[deck].ShowHide();
    }

}
