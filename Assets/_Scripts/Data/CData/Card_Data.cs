using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Card_Data : ScriptableObject
{
    public CardBase.CardType cardType;
    public string titleData;
    public Sprite spriteData;
    public bool inPool = true;
    public bool Custom = false;
    public string customID = "";
   
}
