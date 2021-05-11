using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    bool transitioning = false;
    bool sceneLoaded = false;
    bool transStarted = false;
    [SerializeField] GameObject plate, spinner;
    [SerializeField] Animator control;
    int lastScene;
    int newScene;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void TransitionBack()
    {
        SceneManager.LoadScene(lastScene);
        lastScene = 0;
    }

    public void Transition(int sceneTo, int sceneFrom)
    {
        plate.SetActive(true);
        lastScene = sceneFrom;
        sceneLoaded = false;
        transStarted = false;
        transitioning = true;
        control.Play("In");
        newScene = sceneTo;
    }

    private void Update()
    {
        if (transitioning)
        {
           
            if (control.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
            {
                if (!transStarted) { SceneManager.LoadScene(newScene); transStarted = true; }
                if (sceneLoaded)
                {
                    control.SetTrigger("Loaded");
                }
            }
            else 
            if (control.GetCurrentAnimatorStateInfo(0).IsName("Stagnant")&& sceneLoaded)
            {
                transitioning = false;
                plate.SetActive(false);
            }
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
      
        sceneLoaded = true;

    }
}
