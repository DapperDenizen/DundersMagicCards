using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMProHelper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI primary;
    [SerializeField] List<TextMeshProUGUI> secondary;

    private void Start()
    {
        Invoke("CorrectSize", 0.2f);
    }

    public void CorrectSize()
    {
        foreach (TextMeshProUGUI tm in secondary)
        {
            tm.fontSize = primary.fontSize;
        }
    }
}
