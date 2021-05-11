using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Card_Editor : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI buttonText;
    public GameObject hider;
    public GameObject customMarking;
    public abstract Card_Editor Initiate(Card_Data data);

    public abstract void OnButtonPress();
    public abstract void OnDelete();
    public abstract bool CardCheck(Card_Data data);
}

