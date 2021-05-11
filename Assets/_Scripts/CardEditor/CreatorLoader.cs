using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorLoader : MonoBehaviour
{
    [SerializeField] GameObject holder;
    [SerializeField] CardCreator creator;

    private void Awake()
    {
        OnHide();
    }
    public void OnShow()
    {
        creator.ResetValues();
        holder.SetActive(true);
    }

    public void OnHide()
    {
        holder.SetActive(false);
    }
}
