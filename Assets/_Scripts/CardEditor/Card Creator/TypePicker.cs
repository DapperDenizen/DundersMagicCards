using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypePicker : MonoBehaviour
{
    [SerializeField ]CardCreator boss;
    int typeSelected = 0;
    int plateOpen = 0;
    [SerializeField] List<Button> typeButtons = new List<Button>();
    [SerializeField] List<Button> plateButtons = new List<Button>();
    [SerializeField] List<GameObject> plates = new List<GameObject>();
    [SerializeField] GameObject mainPlate;
    [SerializeField] ColorBlock normal;
    [SerializeField] ColorBlock selected;
    public void OnOpen()
    {
        //set up
        int lastType = boss.GetTypeIndex();
        mainPlate.SetActive(true);
        foreach (Button butt in typeButtons) { butt.colors = normal; ;  } //lazy lazy lazy
        typeButtons[lastType].colors = selected;
        //Page getter
        if (lastType <= 3)
        {
            OnPlateChange(0);
        }
        else if (lastType >= 4 && lastType <= 7)
        {
            OnPlateChange(1);
        }
        else
        {
            OnPlateChange(2);
        }
    }

    public void OnPlateChange(int i)
    {
        plates[plateOpen].SetActive(false);
        plates[i].SetActive(true);
        plateOpen = i;
        //lazy lazy lazy
        foreach (Button butt in plateButtons) { butt.interactable = true; }
        plateButtons[i].interactable = false;

    }

    public void OnTypePress(int i)
    {
        if (i != typeSelected)
        {
            boss.TypeChange(i);
        }
        OnClose();
    }

    public void OnClose()
    {
        mainPlate.SetActive(false);
    }
}
