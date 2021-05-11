using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewSublist", menuName = "Card/Sublist", order = 2)]
public class CData_Sublist : Card_Data
{
    public string descData;
    public bool hasSublist { get { return sublist.Count != 0; } }
    public List<string> sublist;
    public bool suffix;
}
