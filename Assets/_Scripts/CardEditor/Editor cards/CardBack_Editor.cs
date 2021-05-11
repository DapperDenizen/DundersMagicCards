using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBack_Editor : MonoBehaviour
{
    public CardBase.CardType myType;
    public TipHandler tipHandler;
    [SerializeField] GameObject hider;
    bool hidden = false;

    public void Setup( CardBase.CardType type)
    {
        myType = type;
        tipHandler.AssignType(type);
    }

    public void OnButtonPressed()
    {
        hidden = !hidden;
        hider.SetActive(hidden);
        DeckBuilderHandler.instance.ShowDeck(myType);
    }
}
