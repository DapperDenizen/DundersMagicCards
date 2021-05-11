using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Stat : CardBase
{
    public GameObject hider;
    [SerializeField] TextMeshProUGUI desc;
    //Values
    [SerializeField] List<TextMeshProUGUI> values;
    //Sets
    [SerializeField] List<TextMeshProUGUI> sets;

    public override void OnInitiate(CardDeck newType)
    {
        tipHand.AssignType(myType);
        DisplayDescription();
        List<int[]> numbers = new List<int[]>();
        //Figure out which Gen type to use
        switch (Data.instance.statType)
        {
            case Data.StatType.Anarchy: numbers = GenerateAnarchy();break;
            case Data.StatType.Colville: numbers = GenerateColville();break;
            default: numbers = GenerateDefault(); break;
        }
        //Display Roll
        int i = 0;
        foreach (TextMeshProUGUI vals in values)
        {
            vals.text = ""+AddFirstThree(numbers[i]);
            DisplaySet(numbers[i], sets[i]);
            i++;
        }
    }

    public override void OnReroll()
    {
        OnInitiate(myDeck);
    }

    //Generators
    List<int[]> GenerateAnarchy()
    {
        List<int[]> toReturn = new List<int[]>();
        for (int i = 0; i < 6; i++)
        {
            int[] temp = { Random.Range(1, 21), 0, 0, 0, };
            //sort
            for (int z = 0; z < 3; z++)
            {
                if (temp[z + 1] > temp[z])
                {
                    //swap
                    int swap = temp[z + 1];
                    temp[z + 1] = temp[z];
                    temp[z] = swap;
                }
            }
            toReturn.Add(temp);
        }
        return toReturn;
    }

    List<int[]> GenerateColville()
    {
        List<int[]> toReturn = new List<int[]>();
        for (int i = 0; i < 6; i++)
        {
            int[] toCheck = GenerateFourDSix();
            while (AddFirstThree(toCheck) <= 7f)
            {
                toCheck = GenerateFourDSix();
            }
            toReturn.Add(GenerateFourDSix());
        }
        float bonusTotal = 0;
        foreach (int[] set in toReturn)
        {
            bonusTotal += GetBonusScore(AddFirstThree(set));
        }
        if (bonusTotal > 2)
        {
            return toReturn;
        }
        else
        {
            return GenerateColville();
        }
    }

    List<int[]> GenerateDefault()
    {
        List<int[]> toReturn = new List<int[]>();
        for (int i = 0; i < 6; i++)
        {
            toReturn.Add(GenerateFourDSix());
        }
        return toReturn;
    }
    //Helpers
    int[] GenerateFourDSix()
    {
        List<int> toReturn = new List<int>();
        //Generate
        for (int i = 0; i < 4; i++)
        {
            toReturn.Add(Random.Range(1, 7));
        }
        //sort
        for (int i = 0; i < 3; i++)
        {
            if (toReturn[i+1] > toReturn[i])
            {
                //swap
                int temp = toReturn[i + 1];
                toReturn[i + 1] = toReturn[i];
                toReturn[i] = temp;
            }
        }

        return toReturn.ToArray(); ;
    }

    int AddFirstThree(int[] toAdd)
    {
        return toAdd[0] + toAdd[1] + toAdd[2];
    }

    int GetBonusScore(int value)
    {
        return Mathf.RoundToInt(value - 10.5f);
    }

    void DisplaySet(int[] set, TextMeshProUGUI text)
    {
        text.text = set[0] + " + " + set[1] + " + " + set[2] + " (" + set[3] + ")";
    }

    void DisplayDescription()
    {
        switch (Data.instance.statType)
        {
            case Data.StatType.Anarchy: desc.text = "Pure Anarchy";break;
            case Data.StatType.Colville: desc.text = "Reroll below 6";break;
            default: desc.text = "4D6 drop the lowest";break;
        }
    }

    public void HiderON() { hider.SetActive(true); }
    public void HiderOFF() { hider.SetActive(false); }
}
