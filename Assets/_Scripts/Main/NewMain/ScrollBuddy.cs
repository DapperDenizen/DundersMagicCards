using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBuddy : MonoBehaviour
{
    [SerializeField] MainSetup handler;
    [SerializeField] Transform content;
    public GameObject blocker;
    float yPos;
    //Scroll clicking variables
    public int currNotch = 0;
    [SerializeField] int targetNotch = 0;
    public float distBetween;
    float startingX;
    float initialX;
    //Animation variables
    bool animating = false;
    bool animFromButton = false;
    float targetPos = 0;
    float animTime = 0.5f;
    //Jank Scroll variables
    Vector3 mouseInitPosition;
    [SerializeField]float moveSpeed;


    //Functions
    private void Awake()
    {
        startingX = content.position.x;
        yPos = content.position.y;
    }


    private void Update()
    {
        
        //Return cause we're in the wrong rotation
        if (Screen.width > Screen.height) { return; }
        //
        Vector2 vel = Vector2.zero;
        //stop animations if we interact
        if (Input.GetMouseButtonDown(0) && animating && !animFromButton) { StopAllCoroutines(); animating = false; }
        //if we're anmimating let it do its thing
        if (animating) { targetNotch = currNotch; return; }
        if (Input.GetMouseButtonDown(0))
        {
            mouseInitPosition = Input.mousePosition;
            initialX = content.localPosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            vel = Input.mousePosition - mouseInitPosition;
            mouseInitPosition = Input.mousePosition;
            
        }
        content.position += new Vector3(vel.x * moveSpeed, 0);

        if (Input.GetMouseButton(0))
        {
            
            //This clicks over to the next notch when it passes a certain position, it then requires more movement in the opposite position to click back
            if (content.localPosition.x <= (-1 * (startingX + ((targetNotch + 0.2f) * distBetween))))
            {
                if (currNotch > targetNotch)
                {
                    if (content.localPosition.x <= (-1 * (startingX + ((targetNotch + 0.81f) * distBetween))))
                    {
                        targetNotch++;
                    }
                }
                else
                {
                    targetNotch++;
                }
            }
            else
            if (content.localPosition.x > (-1 * (startingX + ((targetNotch - 0.2f) * distBetween))))
            {
                if (currNotch < targetNotch)
                {
                    if (content.localPosition.x > (-1 * (startingX + ((targetNotch - 0.81f) * distBetween))))
                    {
                        targetNotch--; 
                    }
                }
                else
                {
                    targetNotch--; 
                }
            }
        }
        if (Mathf.Abs(initialX - content.localPosition.x) > 0.1f) { blocker.SetActive(true); }
        if (content.position.x % 5.5 != 0 && !Input.GetMouseButton(0))
        {

            if (targetNotch > 4) { targetNotch = 4; }
            if (targetNotch < 0) { targetNotch = 0; }

            targetPos = -1 * (startingX + (targetNotch * distBetween));
            currNotch = targetNotch;
            animating = true;
            StartCoroutine("PrettyAnimate");
            blocker.SetActive(false);
        }

    }

    public void Increment(int val)
    {
        if (animating) { return; }
        animFromButton = true;
        animating = true;
        targetNotch = currNotch + val;
        targetPos = -1 * (startingX + (targetNotch * distBetween));
        currNotch = targetNotch;
        StartCoroutine("PrettyAnimate");
        blocker.SetActive(false);
    }

    public void ResetNotch() { currNotch = targetNotch = 0;  content.localPosition = new Vector3(0, 0); initialX = 0; }

    IEnumerator PrettyAnimate()
    {
        float startPos = content.position.x;
        float startTime = Time.time;

        //Calc Speed!

        float spMulti = Mathf.Abs(initialX - content.position.x);
        float dist = Mathf.Abs(content.position.x - targetPos);
        while (Mathf.Abs(content.position.x - targetPos) > 0.1f)
        {
            float lerpNumb = (Time.time - startTime) / (animTime * .75f);
            float newX = Mathf.Lerp(startPos, targetPos, lerpNumb);
            content.position = new Vector3(newX, content.position.y);
            yield return null;
        }
        content.position = new Vector3(targetPos, content.position.y);
        animating = false;
        animFromButton = false;
        handler.UpdateTitle();
        initialX = content.localPosition.x;
    }

}
