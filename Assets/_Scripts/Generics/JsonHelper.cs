using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tex -2019- :^)

public class JsonHelper 
{

    /// <summary>
    /// Changed a list of Json Objects into a JsonHelper String
    /// </summary>
    /// <typeparam name="T"> List List Type </typeparam>
    /// <param name="incoming"> Nested List </param>
    /// <returns></returns>
    public static string NestedToJson<T>(List<List<T>> incoming)
    {
        string outGoing = "";

        foreach (List<T> data in incoming)
        {
            outGoing += ListToJson<T>(data)+"&";
        }
        return outGoing;
    }

    /// <summary>
    /// Changes a JsonHelper string into a list of type T
    /// </summary>
    /// <typeparam name="T"> List List Type </typeparam>
    /// <param name="incoming"> JsonHelper String </param>
    /// <returns></returns>
    public static List<List<T>> JsonToNested<T>(string incoming)
    {
        List<List<T>> outGoing = new List<List<T>>();
        string[] toConvert = incoming.Split('&');
        foreach (string entry in toConvert)
        {
            if (entry == "") { continue; }
            outGoing.Add(JsonToList<T>(entry));
        }

        return outGoing;
    }

    /// <summary>
    ///  Converts a List of Type T String to a Json String
    /// </summary>
    /// <typeparam name="T"> List Type </typeparam>
    /// <param name="incoming"> incoming list </param>
    /// <returns></returns>
    public static string ListToJson<T>(List<T> incoming)
    {

        HelperWrapper<List<T>> toJson = new HelperWrapper<List<T>>(incoming);
        return JsonUtility.ToJson(toJson); ;
    }

    /// <summary>
    ///  Converts a Json String to a List of Type T
    /// </summary>
    /// <typeparam name="T">List type </typeparam>
    /// <param name="incoming"> incoming string </param>
    /// <returns></returns>

    public static List<T> JsonToList<T>(string incoming)
    {
        HelperWrapper<List<T>> fromJson = JsonUtility.FromJson<HelperWrapper<List<T>>>(incoming);
        return fromJson.data;
    }


    public static string DataToJson<T>(T incoming)
    {
        HelperWrapper<T> toJson = new HelperWrapper<T>(incoming);
        return JsonUtility.ToJson(toJson); ;
    }

    public static T JsonToData<T>(string incoming)
    {
        HelperWrapper<T> fromJson = JsonUtility.FromJson<HelperWrapper<T>>(incoming);
        return fromJson.data;
    }
}

[System.Serializable]
public class HelperWrapper<T>
{
    public T data;
    public HelperWrapper(T data)
    {
        this.data = data;
    }
}
