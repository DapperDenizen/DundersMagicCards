using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsHandler : MonoBehaviour
{
    [SerializeField] GameObject holder;

    public void OnClose() { holder.SetActive(false); }
    public void OnBLOG() { Application.OpenURL("https://dundersdesign.blogspot.com/2021/05/dunders-magical-cards-how-i-fixed-my.html"); }
    public void onTWTR() { Application.OpenURL("https://twitter.com/DundersMagic"); }
}
