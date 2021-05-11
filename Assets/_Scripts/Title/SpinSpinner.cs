using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpinner : MonoBehaviour
{
    [SerializeField] List<CardBase> defaults;
    [SerializeField] Transform spinTrans;
    bool animating = false;

    //Rotation stuff
    float targetRot;
    float animTime = 3f;
    //Waiting stuff
    [SerializeField] float waitTime = 1f;
    [SerializeField] float offset = 1f;
    float targetTime = 0;
    //New card stuff
    int notch = 0;

    void Start()
    {
        targetTime = Time.time + offset;
        if (!Data.instance.setup) { Data.instance.SetUpLists(); }
        foreach (CardBase card in defaults)
        {

            card.OnReroll();
        }
    }

    void Update()
    {
        
        if (!animating)
        {
            //check wait time
            if (Time.time >= targetTime)
            {
                //Start animation!
                StopAllCoroutines();
                targetRot = targetRot + 45f;
                animating = true;
                StartCoroutine("SpinToNext");
            }
        }
    }

    void Incrment()
    {
        spinTrans.rotation = Quaternion.Euler(0, 0, targetRot);
        targetTime = Time.time + waitTime;
        defaults[notch].OnReroll();
        notch++;
        if (notch >= defaults.Count) { notch = 0; }
        animating = false;
    }

    IEnumerator SpinToNext()
    {
        float startRot = spinTrans.rotation.eulerAngles.z; 
        float startTime = Time.time;
       
        while (true)
        {
            if ((Vector3.Distance(new Vector3(0, spinTrans.eulerAngles.y, targetRot), spinTrans.eulerAngles) < 0.2f) || targetRot == 360 && spinTrans.rotation.eulerAngles.z < 0.2f)
            {
                if (targetRot == 360) { targetRot = 0; }
                Incrment();
                break;
            }
            
            float lerpNumb = (Time.time - startTime) / animTime;
            float newZ = Mathf.Lerp (startRot, targetRot, lerpNumb);
            spinTrans.rotation =  Quaternion.Euler(0, spinTrans.rotation.eulerAngles.y, newZ);
           
            yield return null;
        }
    }

}
