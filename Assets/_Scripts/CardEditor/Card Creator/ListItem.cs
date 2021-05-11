using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ListItem : MonoBehaviour
{
    public ListItemHandler boss;
    public TextMeshProUGUI value;

    public void OnDelete()
    {
        boss.OnDelete(this);
    }
}
