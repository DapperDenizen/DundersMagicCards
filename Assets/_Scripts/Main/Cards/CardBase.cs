using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class CardBase : MonoBehaviour
{
    public enum CardDeck { Location,Goal,Villian,Enemy,Twist ,Race,Background,Class,Drive,Stats ,Entrance,Encounter,Boss}
    public enum CardType { Location,Goal,Villian,Enemy,Twist ,Race,Background,Class,Drive,Stats ,Entrance,Combat,Challenge,Setback,Boss}
    public CardDeck myDeck;
    public CardType myType;
    public int dataNumber =-1;
    public TextMeshProUGUI titleText;
    public Image splashImage;
    public TipHandler tipHand;
    Transform myTrans;
    float targetRot;
    bool animating = false;
    public bool locked = false;
    private void Start()
    {
        myTrans = transform;
    }
    abstract public void OnInitiate(CardDeck newtype);
    abstract public void OnReroll();

    public void LockUnlock(bool locked)
    {
        GetComponentInChildren<Button>().interactable = !locked;
        this.locked = locked;
    }

    public void FlipCard()
    {
        if (animating) { return; }
        targetRot = 0f;
        animating = true;
        StartCoroutine("PrettyFlip");
    }

    IEnumerator PrettyFlip()
    {
        bool rotDone = myTrans.rotation.eulerAngles.y == 180f ? true : false;
        bool rerollDone = rotDone;
        float startRot = myTrans.rotation.eulerAngles.y;
        float startTime = Time.time;
        float animTime = 0.9f;
        while (true)
        {
            float lerpNumb = (Time.time - startTime) / animTime;

            if (!rotDone)
            {
                float newRot = Mathf.Lerp(startRot, 180f, lerpNumb);
                myTrans.rotation = Quaternion.Euler(0, newRot, 0);
            }
            else
            {
                float newRot = Mathf.Lerp(startRot, 0, lerpNumb);
                myTrans.rotation = Quaternion.Euler(0, newRot, 0);
            }
            if (Mathf.Abs(myTrans.rotation.eulerAngles.y - 95f) < 2f && !rerollDone) { rerollDone = true;  OnReroll(); }
            if (Mathf.Abs(myTrans.rotation.eulerAngles.y - 180f) < 0.5f && !rotDone) { rotDone = true; startRot = myTrans.rotation.eulerAngles.y; startTime = Time.time;}
            if (myTrans.rotation.eulerAngles.y == 0 && rotDone) { break; }
            yield return null;
        }
        myTrans.rotation = Quaternion.Euler(0, 0, 0);
        animating = false;
    }

}
