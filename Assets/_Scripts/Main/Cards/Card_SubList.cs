using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_SubList : CardBase
{

    public override void OnInitiate(CardDeck newType)
    {
        myDeck = newType;
        if (Data.instance.GetTypeLength(myDeck) == 1 && dataNumber != -1) { return; }

        if (Data.instance.GetTypeLength(myDeck) == 0)
        {
            dataNumber = 0;
        }
        else
        {
            int newRandom = Random.Range(0, (int)Data.instance.GetTypeLength(myDeck)-1);
            while (newRandom == dataNumber)
            {
                newRandom = Random.Range(0, (int)Data.instance.GetTypeLength(myDeck)-1);
            }
            dataNumber = newRandom;
        }
    }

}
