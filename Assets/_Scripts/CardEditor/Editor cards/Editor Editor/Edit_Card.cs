using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Edit_Card : Card_Editor
{
    public EditorEditor editor;
    public TextMeshProUGUI desc;

    public abstract void AssignData();

    public override void OnButtonPress()
    {
        //tell editor
        editor.ChangeCard(this);
    }

    public abstract Card_Data GetData();
}
