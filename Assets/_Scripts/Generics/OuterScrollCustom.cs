using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OuterScrollCustom : ScrollRect
{
    //
    RectTransform top;
    RectTransform bottom;
    Vector3[] windowCorners = new Vector3[4];

    protected override void Start()
    {
        base.Start();

    }

    public void CalcBounds()
    {
        if (top != null) { Destroy(top.gameObject); }
        if (bottom != null) { Destroy(bottom.gameObject); }
        //Setup
        Vector3[] tempCorners = new Vector3[4];
        //Left
        content.GetWorldCorners(tempCorners);
        top = new GameObject("Top").AddComponent<RectTransform>();
        top.SetParent(viewport);
        top.position = tempCorners[1];
        //Right
        content.GetChild(content.childCount - 1).GetComponent<RectTransform>().GetWorldCorners(tempCorners);
        bottom = new GameObject("Bottom").AddComponent<RectTransform>();
        bottom.position = tempCorners[0];
        bottom.SetParent(viewport);

    }
}
