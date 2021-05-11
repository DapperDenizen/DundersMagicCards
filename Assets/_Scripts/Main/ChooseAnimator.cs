using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAnimator : MonoBehaviour
{
    bool animating;
    Animator myAnim;
    Transform myTrans;
    [SerializeField] PositionSet[] positionsP, positionsL;
    bool portrait = false;
    

    void Start()
    {
        myTrans = transform;
        myAnim = GetComponent<Animator>();
        if (Screen.width < Screen.height)
        {
            //landscape
            portrait = true; 
        }
        SwitchOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        if (animating) { return; }
        if ((Screen.width > Screen.height && portrait) || Screen.width < Screen.height && !portrait)
        {
            portrait = !portrait;
            SwitchOrientation();
        }
    }

    void SwitchOrientation()
    {
        int i = 0;
        if (!portrait)
        {
            //landscape
            foreach (Transform child in myTrans.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Card"))
                {
                    child.position = positionsL[i].pos;
                    child.rotation = Quaternion.Euler(0, 0, positionsL[i].rot);
                    i++;
                }
            }
        }
        else
        {
            //Portrait
            foreach (Transform child in myTrans.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Card"))
                {
                    child.position = positionsP[i].pos;
                    child.rotation = Quaternion.Euler(0, 0, positionsP[i].rot);
                    i++;
                }
            }
        }
    }

    public void OnChoice()
    {
        myAnim.SetBool("Portrait", portrait);
        myAnim.SetTrigger("Animate");
    }

    public void OnReset()
    {
        myAnim.SetTrigger("Reset");
    }
}

[System.Serializable]
struct PositionSet
{
    public Vector3 pos;
    public float rot;
}
