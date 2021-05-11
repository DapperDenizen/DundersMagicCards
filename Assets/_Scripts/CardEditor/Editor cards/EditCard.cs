using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EditCard : MonoBehaviour
{
    [SerializeField] GameObject popup;

    public void ShowHide(bool sh)
    {
        popup.SetActive(sh);
    }

    public void OnNew()
    {
        SceneManager.LoadSceneAsync(3,LoadSceneMode.Additive);
    }

    public void OnEdit()
    {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void OnClearAll()
    {
        Data.instance.CleanAllData();
        Destroy(Data.instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
