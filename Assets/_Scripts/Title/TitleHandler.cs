using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleHandler : MonoBehaviour
{
    [SerializeField] GameObject credits;

    public void ToScene(int to)
    {
        if (to == 2) { Data.instance.transitioner.Transition(to,0); return; }
        SceneManager.LoadScene(to); return;
    }

    public void OpenCreds()
    {
        credits.SetActive(true);
    }

}
