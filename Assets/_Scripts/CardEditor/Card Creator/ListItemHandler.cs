using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItemHandler : MonoBehaviour
{
    [SerializeField] GameObject prefabInput, contentGO;
    List<ListItem> inputs = new List<ListItem>();

    [SerializeField] Transform content;
    // Start is called before the first frame update

    public void OnAdd()
    {
        //make new input obj
        //assign to content
        //add to inputs list
        //GameObject obj =  Instantiate(prefabInput);// Instantiate(prefabInput, content);
        //obj.transform.parent = content;
        inputs.Add(Instantiate(prefabInput, content).GetComponent<ListItem>());
        //inputs.Add(obj.GetComponent<ListItem>());
        inputs[inputs.Count - 1].boss = this;
        int contentIndex = (content.childCount == 2) ? 0 : content.childCount - 2;
        inputs[inputs.Count - 1].transform.SetSiblingIndex(contentIndex);
        
    }

    public void OnAdd2()
    {

        inputs.Add(GameObject.Instantiate<GameObject>(prefabInput,contentGO.GetComponent<RectTransform>()).GetComponent<ListItem>());

        //RectTransform obj =  Instantiate(prefabInput,content).GetComponent<RectTransform>();// Instantiate(prefabInput, content);
        //obj.parent = contentGO.GetComponent<RectTransform>();
        //inputs.Add(Instantiate(prefabInput, content).GetComponent<ListItem>());
        //inputs.Add(obj.GetComponent<ListItem>());
        inputs[inputs.Count - 1].boss = this;
        int contentIndex = (content.childCount == 2) ? 0 : content.childCount - 2;
        inputs[inputs.Count - 1].transform.SetSiblingIndex(contentIndex);

    }

    public void OnAdd(string itemName)
    {
        inputs.Add(GameObject.Instantiate(prefabInput, content).GetComponent<ListItem>());
        inputs[inputs.Count - 1].boss = this;
        inputs[inputs.Count - 1].GetComponentInChildren<TMP_InputField>().text = itemName;
        int contentIndex = (content.childCount == 2) ? 0 : content.childCount - 2;
        inputs[inputs.Count - 1].transform.SetSiblingIndex(contentIndex);
        inputs[inputs.Count - 1].GetComponent<ListItem>().value.fontSize = 25f; //Auto size fucks up for some reason and i have a smol brain so i just hard set it :^)
    }

    public void OnDelete(ListItem toDelete)
    {
        inputs.Remove(toDelete);
        GameObject.Destroy(toDelete.gameObject);
    }

    public void DeleteAll()
    {
        for (int i = inputs.Count - 1; i > -1; i--)
        {
            inputs[i].OnDelete();
        }
        inputs.Clear();
    }

    public List<string> GetListItems()
    {
        List<string> toReturn = new List<string>();

        foreach (ListItem item in inputs)
        {
            toReturn.Add(item.value.text);
        }
        return toReturn;
    }
}
