using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeUIIMG : MonoBehaviour
{
    bool started = false;
    float targetA = 0;
    [SerializeField] Image img;

    public void initFadeIn()
    {
        if (started) { return; }
        StopAllCoroutines();
        started = true;
        targetA = 1;
        StartCoroutine("FadeOut");
    }

    public void initFadeOut()
    {
        if (started) { return; }
        StopAllCoroutines();
        started = true;
        targetA = 0;
        StartCoroutine("FadeOut");
    }


    IEnumerator FadeOut()
    {
        float startInt = img.color.a;
        float startTime = Time.time;
        float animTime = 0.5f;
        while (img.color.a > 0.1f)
        {
            float lerpNumb = (Time.time - startTime) / animTime;
            float newA = Mathf.Lerp(startInt, targetA, lerpNumb);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newA);
            yield return null;
        }
        started = false;
        if (img.color.a < 0.5f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            initFadeOut();
        }
    }
}
