using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDeck : MonoBehaviour
{
    static List<CData_Default> inplay = new List<CData_Default>();

    public static CData_Default[] getCardSet()
    {
        inplay.Clear();
        inplay.Add(GetEntrance());
        inplay.Add(GetEncounter());
        inplay.Add(GetEncounter());
        inplay.Add(GetBossEncounter());
        return inplay.ToArray();
    }

    public static CData_Default getCard(CardBase.CardDeck deck, CData_Default oldData)
    {
        CData_Default toReturn = null;
        switch (deck)
        {
            case CardBase.CardDeck.Entrance: toReturn = GetEntrance(); break;
            case CardBase.CardDeck.Encounter: toReturn = GetEncounter(); break;
            default: toReturn = GetBossEncounter(); break;
        }

        inplay.Remove(oldData);
        inplay.Add(toReturn);
        return toReturn;
    }

    public static CData_Default GetEntrance()
    {
        CData_Default toReturn = null;
        switch (Random.Range(1, 4))
        {
            case 1: toReturn = Data.instance.entrance_Data[Random.Range(0, Data.instance.entrance.Count)];break;
            case 2: toReturn = GetCombat(); break;
            default: toReturn = GetChallenge(); break;
        }
        //Check
        return toReturn;
    }

    public static CData_Default GetEncounter()
    {
        CData_Default toReturn = null;
        switch (Random.Range(1, 4))
        {
            case 1: toReturn = GetChallenge();break;
            case 2: toReturn = GetSetback();break;
            default: toReturn = GetCombat();break;
        }
        //Check
        return toReturn;
    } 

    public static CData_Default GetCombat()
    {
        CData_Default toReturn = Data.instance.combat[Random.Range(0, Data.instance.combat.Count)];
        if (inplay.Contains(toReturn) && ( Data.instance.combat.Count  >= 2))
        {
            return GetCombat();
        }
        return toReturn;
    }
    public static CData_Default GetChallenge()
    {
        CData_Default toReturn = Data.instance.challenge[Random.Range(0, Data.instance.challenge.Count)];
        if (inplay.Contains(toReturn) && ( Data.instance.challenge.Count >= 2))
        {
            return GetChallenge();
        }
        return toReturn;
    }
    public static CData_Default GetSetback()
    {
        CData_Default toReturn = Data.instance.setback[Random.Range(0, Data.instance.setback.Count)];
        if (inplay.Contains(toReturn) && (Data.instance.setback.Count >= 2))
        {
            return GetSetback();
        }
        return toReturn;
    }
    public static CData_Default GetBossEncounter()
    {
        CData_Default toReturn = Data.instance.boss[Random.Range(0, Data.instance.boss.Count)];
        if (inplay.Contains(toReturn) && (Data.instance.boss.Count >= 2))
        {
            return GetBossEncounter();
        }
        return toReturn;
    }


    public static bool CheckBalance(CardBase.CardType typeCheck)
    {
        int count = 0;
        foreach (CData_Default dat in inplay)
        {
            if (dat.cardType == typeCheck) { count++; }
        }

        if (count >= 2) { return false; }
        if (count == 1) { if (Random.Range(0, 2) == 1) { return false; } } //50% chance of choosing another source
        return true;
    }
}
