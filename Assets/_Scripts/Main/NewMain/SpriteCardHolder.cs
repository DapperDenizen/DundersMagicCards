using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteCardHolder : MonoBehaviour
{
    
    //landscape functionality is currently scrapped pending additional updates for support, felt that the time investment was larger than potential benifit

    [SerializeField] Vector3[] positions;
    Transform myTrans;
    [SerializeField] ScrollBuddy sBud;
    bool portrait = true;
    bool updooted = false;
    int childCount =0;

    void Awake()
    {
        myTrans = transform;
        if (Screen.width > Screen.height)
        {
            //landscape
            portrait = false;
        }
    }

    private void Update()
    {
        if (myTrans.childCount != childCount)
        {
            childCount = myTrans.childCount;
            UpdateChildren();
        }

        if ((Screen.width > Screen.height && portrait) || Screen.width < Screen.height && !portrait)
        {
            portrait = !portrait;
            SwitchOrientation();
        }
    }

    void UpdateChildren()
    {
        if (childCount == 0) { return; }
        int i = 0;
        foreach (Transform child in myTrans.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Card")){
                child.position = positions[i];
                i++;
            }
        }
        updooted = true;
    }

    void SwitchOrientation()
    {
        if (portrait)
        {
            myTrans.position = Vector3.zero + new Vector3((sBud.currNotch*sBud.distBetween),-0.9f);
        }
        else
        {
            myTrans.position = new Vector3(-11, 0.3f, 10);
        }
    }

     void LateUpdate()
    {
        if (updooted)
        {
            myTrans.position = new Vector3(10, -0.9f);
            updooted = false;
        }
    }

}
