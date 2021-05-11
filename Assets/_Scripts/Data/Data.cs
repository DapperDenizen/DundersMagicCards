using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public bool androidVersion;
    public bool setup = false;
    //Statics
    static string DATAREF_CUSTOMCARDDEFAULT_LIST = "CCD_LIST";
    static string DATAREF_CUSTOMCARDSUBLIST_LIST = "CCSL_LIST";
    static string DATAREF_POOL_LIST = "P_LIST";
    static string DATAREF_STAT_INT = "S_INT";
    //

    public static Data instance;
    //Adventure
    [SerializeField] public List<CData_Default> locations_Data;
    [SerializeField] public List<CData_Default> goal_Data;
    [SerializeField] public List<CData_Default> villian_Data;
    [SerializeField] public List<CData_Default> enemy_Data;
    [SerializeField] public List<CData_Default> twist_Data; 
    //Adventurer
    [SerializeField] public List<CData_Sublist> races_Data;
    [SerializeField] public List<CData_Default> backGrounds_Data;
    [SerializeField] public List<CData_Default> classes_Data;
    [SerializeField] public List<CData_Default> drive_Data;
    //FiveDungeon
    [SerializeField] public List<CData_Default> entrance_Data;
    [SerializeField] public List<CData_Default> combat_Data;
    [SerializeField] public List<CData_Default> setback_Data;
    [SerializeField] public List<CData_Default> challenge_Data;
    [SerializeField] public List<CData_Default> boss_Data;
    //Other
    public enum StatType {Default, Colville, Anarchy }
    public StatType statType;
    public Sprite defaultImage;
    public ArtRef artRef;

    //Accessibles
    //Adventure
    [HideInInspector] public  List<CData_Default> locations;
    [HideInInspector] public  List<CData_Default> goal;
    [HideInInspector] public List<CData_Default> villian;
    [HideInInspector] public List<CData_Default> enemy;
    [HideInInspector] public List<CData_Default> twist;
    //Adventurer
    [HideInInspector] public List<CData_Sublist> races;
    [HideInInspector] public List<CData_Default> backGrounds;
    [HideInInspector] public List<CData_Default> classes;
    [HideInInspector] public List<CData_Default> drive;
    //FiveDungeon
    [HideInInspector] public List<CData_Default> entrance;
    [HideInInspector] public List<CData_Default> combat;
    [HideInInspector] public List<CData_Default> setback;
    [HideInInspector] public List<CData_Default> challenge;
    [HideInInspector] public List<CData_Default> boss;

    //Saveable Stuff
    public List<string> customDCardNames = new List<string>();
    public List<string> customSLCardNames = new List<string>();

    //Piggy back stuff
    public SceneTransition transitioner;

    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
        }
    }

    public void SetUpLists()
    {
        setup = true;
        //Adventure
        locations = GetValidData<CData_Default>(locations_Data);
        goal = GetValidData<CData_Default>(goal_Data);
        villian = GetValidData<CData_Default>(villian_Data);
        enemy = GetValidData<CData_Default>(enemy_Data);
        twist = GetValidData<CData_Default>(twist_Data);
        //Adventuerers
        races = GetValidData<CData_Sublist>(races_Data);
        backGrounds = GetValidData<CData_Default>(backGrounds_Data);
        classes = GetValidData<CData_Default>(classes_Data);
        drive = GetValidData<CData_Default>(drive_Data);
        //Dungeon
        entrance = GetValidData<CData_Default>(entrance_Data);
        combat = GetValidData<CData_Default>(combat_Data);
        challenge = GetValidData<CData_Default>(challenge_Data);
        setback = GetValidData<CData_Default>(setback_Data);
        boss = GetValidData<CData_Default>(boss_Data);
    }

    void ResetList<T>(List<T> incoming)
    {
        Card_Data temp;
        foreach (T value in incoming)
        {
             temp = value as Card_Data;
            temp.inPool = true;
        }
    }

    List<T> GetValidData<T>(List<T> incoming)
    {
        List<T> toReturn = new List<T>();
        Card_Data temp;
        foreach (T value in incoming)
        {
                temp = value as Card_Data;
                if (temp.inPool)
                {
                    toReturn.Add(value);
                }
        }
        return toReturn;
    }

    public Card_Data GetData(CardBase.CardDeck type, int index)
    {
        switch (type)
        {
            case CardBase.CardDeck.Location: return locations[index];
            case CardBase.CardDeck.Goal: return goal[index];
            case CardBase.CardDeck.Villian: return villian[index];
            case CardBase.CardDeck.Enemy: return enemy[index];
            case CardBase.CardDeck.Twist: return twist[index];
            //
            case CardBase.CardDeck.Race: return races[index];
            case CardBase.CardDeck.Background: return backGrounds[index];
            case CardBase.CardDeck.Class: return classes[index];
            //
            case CardBase.CardDeck.Entrance: return entrance[index];
            case CardBase.CardDeck.Encounter: return combat[index];
            case CardBase.CardDeck.Boss: return boss[index];

            default: return drive[index];
        }
    }

    public int GetTypeLength(CardBase.CardDeck type)
    {
        switch (type)
        {
            case CardBase.CardDeck.Location: return  locations.Count;
            case CardBase.CardDeck.Goal: return goal.Count;
            case CardBase.CardDeck.Villian: return villian.Count;
            case CardBase.CardDeck.Enemy: return enemy.Count;
            case CardBase.CardDeck.Twist: return twist.Count;
            //
            case CardBase.CardDeck.Race: return races.Count;
            case CardBase.CardDeck.Background: return backGrounds.Count;
            case CardBase.CardDeck.Class: return classes.Count;
            case CardBase.CardDeck.Drive: return drive.Count;
            //
            case CardBase.CardDeck.Entrance: return (entrance.Count + combat.Count + challenge.Count);
            case CardBase.CardDeck.Encounter: return (setback.Count + combat.Count + challenge.Count);
            case CardBase.CardDeck.Boss: return boss.Count;

            default: return 0;
        }
    }

    public void TypeEmpty(CardBase.CardDeck type)
    {
        switch (type)
        {
            case CardBase.CardDeck.Location: locations.Add(locations_Data[0]); return;
            case CardBase.CardDeck.Goal: goal.Add(goal_Data[0]); return;
            case CardBase.CardDeck.Villian: villian.Add(villian_Data[0]); return;
            case CardBase.CardDeck.Enemy: enemy.Add(enemy_Data[0]); return;
            case CardBase.CardDeck.Twist: twist.Add(twist_Data[0]); return;
            //
            case CardBase.CardDeck.Race: races.Add(races_Data[0]); return;
            case CardBase.CardDeck.Background: backGrounds.Add(backGrounds_Data[0]); return;
            case CardBase.CardDeck.Class: classes.Add(classes_Data[0]); return;
            //
            case CardBase.CardDeck.Entrance: entrance.Add(entrance_Data[0]); return;
            case CardBase.CardDeck.Encounter: combat.Add(combat_Data[0]); challenge.Add(challenge_Data[0]); setback.Add(setback_Data[0]); return;
            case CardBase.CardDeck.Boss: boss.Add(boss_Data[0]); return;
        }
    }

    List<bool> GeneratePoolBool<T>(List<T> incoming)
    {
        List<bool> toReturn = new List<bool>();
        Card_Data temp;
        foreach (T value in incoming)
        {
            temp = value as Card_Data;
            toReturn.Add(temp.inPool);
        }
        return toReturn;
    }

    void ReadPoolBool<T>( List<T> incoming, List<bool> poolbools, bool isCustom)
    {
        Card_Data temp;
        int i = 0;
        foreach (bool value in poolbools)
        {
            if (i > incoming.Count - 1)
            {
                break;
            }

            temp = incoming[i] as Card_Data;
            if (temp == null)
            {
                i++;
                continue;
            }
            temp.inPool = value;
            i++;
        }
        if (isCustom && i > poolbools.Count)
        {
            for (int z = poolbools.Count; z < incoming.Count; z++)
            {
                temp = incoming[z] as Card_Data;
                temp.inPool = false;
            }
        }
    }

    void SetPool(List<List<bool>> incoming, bool isCustom)
    {
        ReadPoolBool<CData_Default>(locations_Data, incoming[0], isCustom);
        ReadPoolBool<CData_Default>(goal_Data, incoming[1], isCustom);
        ReadPoolBool<CData_Default>(villian_Data, incoming[2], isCustom);
        ReadPoolBool<CData_Default>(enemy_Data, incoming[3], isCustom);
        ReadPoolBool<CData_Default>(twist_Data, incoming[4], isCustom);
        ReadPoolBool<CData_Sublist>(races_Data, incoming[5], isCustom);
        ReadPoolBool<CData_Default>(backGrounds_Data, incoming[6], isCustom);
        ReadPoolBool<CData_Default>(classes_Data, incoming[7], isCustom);
        ReadPoolBool<CData_Default>(drive_Data, incoming[8], isCustom);
        ReadPoolBool<CData_Default>(entrance_Data, incoming[9], isCustom);
        ReadPoolBool<CData_Default>(combat_Data, incoming[10], isCustom);
        ReadPoolBool<CData_Default>(setback_Data, incoming[11], isCustom);
        ReadPoolBool<CData_Default>(challenge_Data, incoming[12], isCustom);
        ReadPoolBool<CData_Default>(boss_Data, incoming[13], isCustom);
    }

    public void RemoveCard(CData_Default card)
    {
        switch (card.cardType)
        {
            case CardBase.CardType.Location: locations_Data.Remove(card); break;
            case CardBase.CardType.Goal: goal_Data.Remove(card); break;
            case CardBase.CardType.Villian: villian_Data.Remove(card); break;
            case CardBase.CardType.Enemy: enemy_Data.Remove(card); break;
            case CardBase.CardType.Twist: twist_Data.Remove(card); break;

            case CardBase.CardType.Background: backGrounds_Data.Remove(card); break;
            case CardBase.CardType.Class: classes_Data.Remove(card); break;
            case CardBase.CardType.Drive: drive_Data.Remove(card); break;

            case CardBase.CardType.Entrance: entrance_Data.Remove(card); break;
            case CardBase.CardType.Setback: setback_Data.Remove(card); break;
            case CardBase.CardType.Challenge: challenge_Data.Remove(card); break;
            case CardBase.CardType.Combat: combat_Data.Remove(card); break;
            case CardBase.CardType.Boss: boss_Data.Remove(card); break;
        }
        Data.instance.customDCardNames.Remove(card.customID);
        PlayerPrefs.DeleteKey(card.customID);

    }

    public void RemoveCard(CData_Sublist card)
    {
        Data.instance.races_Data.Remove(card);
        Data.instance.customDCardNames.Remove(card.customID);
        PlayerPrefs.DeleteKey(card.customID);
    }

    public bool CanDisable( CData_Default data)
    {
        List<CData_Default> defaultList = new List<CData_Default>();
        switch (data.cardType)
        {
            case CardBase.CardType.Location: defaultList = locations_Data; break;
            case CardBase.CardType.Goal: defaultList = goal_Data; break;
            case CardBase.CardType.Villian: defaultList = villian_Data; break;
            case CardBase.CardType.Enemy: defaultList = enemy_Data; break;
            case CardBase.CardType.Twist: defaultList = twist_Data; break;

            case CardBase.CardType.Background: defaultList = backGrounds_Data; break;
            case CardBase.CardType.Class: defaultList = classes_Data; break;
            case CardBase.CardType.Drive: defaultList = drive_Data; break;

            case CardBase.CardType.Entrance: defaultList = entrance_Data; break;
            case CardBase.CardType.Combat: defaultList = combat_Data; break;
            case CardBase.CardType.Setback: defaultList = setback_Data; break;
            case CardBase.CardType.Challenge: defaultList = challenge_Data; break;
            case CardBase.CardType.Boss: defaultList = boss_Data; break;

           
        }
        foreach (CData_Default dat in defaultList) { if (dat.inPool && dat != data) { return true; } }

        return false;
    }

    public bool CanDisable(CData_Sublist data)
    {
        List<CData_Sublist> defaultList = races;
        foreach (CData_Sublist dat in defaultList) { if (dat.inPool && dat != data) { return true; } }

        return false;
    }

    //update
    public void UpdateCard(CData_Default updated)
    {
        List<CData_Default> typeList;
        switch (updated.cardType)
        {
            case CardBase.CardType.Location: typeList =locations_Data; break;
            case CardBase.CardType.Goal: typeList = goal_Data; break;
            case CardBase.CardType.Villian: typeList = villian_Data; break;
            case CardBase.CardType.Enemy: typeList = enemy_Data; break;
            case CardBase.CardType.Twist: typeList = twist_Data; break;
            case CardBase.CardType.Background: typeList = backGrounds_Data; break;
            case CardBase.CardType.Class: typeList = classes_Data; break;
            default: typeList = drive_Data; break;
        }
        for(int i = 0; i < typeList.Count; i++)
        {
            if (typeList[i].customID == updated.customID)
            {
                typeList[i] = updated;
                PlayerPrefs.SetString(updated.customID, JsonUtility.ToJson(updated));
                return;
            }
        }
    }

    public void UpdateCard(CData_Sublist updated)
    {
        List<CData_Sublist> typeList;
        switch (updated.cardType)
        {
            default: typeList = races_Data; break;
        }
        for (int i = 0; i < typeList.Count; i++)
        {
            if (typeList[i].customID == updated.customID)
            {
                typeList[i] = updated;
                PlayerPrefs.SetString(updated.customID, JsonUtility.ToJson(updated));
                return;
            }
        }
    }

    //Load / Save
    public void Save() { SavePool(); SaveStat(); SaveCards(); }
    public void Load() { LoadPool(); LoadStat(); LoadCards(); }

    public void SaveCards()
    {
        //cards are saved when made, this loads the strings to reference them
        PlayerPrefs.SetString(DATAREF_CUSTOMCARDDEFAULT_LIST,JsonHelper.ListToJson<string>(customDCardNames));
        PlayerPrefs.SetString(DATAREF_CUSTOMCARDSUBLIST_LIST,JsonHelper.ListToJson<string>(customSLCardNames));

    }

    void LoadCards()
    {
        //Load string list
        string defaultNames = PlayerPrefs.GetString(DATAREF_CUSTOMCARDDEFAULT_LIST);
        string sublistNames = PlayerPrefs.GetString(DATAREF_CUSTOMCARDSUBLIST_LIST);
        if (defaultNames != "")
        {
            customDCardNames = JsonHelper.JsonToList<string>(defaultNames);
            foreach (string identifier in customDCardNames)
            {
                //load cards
                LoadCardDefault(identifier);
            }

        }
        if (sublistNames != "")
        {
            customSLCardNames = JsonHelper.JsonToList<string>(sublistNames);
        }
    }

    void LoadCardDefault(string cardRef)
    {

        CData_Default newCard = ScriptableObject.CreateInstance<CData_Default>();
        newCard.name = "Loaded Card";
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(cardRef),newCard);
        switch (newCard.cardType)
        {
            case CardBase.CardType.Background: backGrounds_Data.Add(newCard); break;
            case CardBase.CardType.Class: classes_Data.Add(newCard); break;
            case CardBase.CardType.Drive: drive_Data.Add(newCard); break;
            case CardBase.CardType.Enemy: enemy_Data.Add(newCard); break;
            case CardBase.CardType.Goal: goal_Data.Add(newCard); break;
            case CardBase.CardType.Location: locations_Data.Add(newCard); break;
            case CardBase.CardType.Entrance: entrance_Data.Add(newCard); break;
            case CardBase.CardType.Combat: combat_Data.Add(newCard); break;
            case CardBase.CardType.Challenge: challenge_Data.Add(newCard); break;
            case CardBase.CardType.Setback: setback_Data.Add(newCard); break;
            case CardBase.CardType.Boss: boss_Data.Add(newCard); break;
            case CardBase.CardType.Twist: twist_Data.Add(newCard); break;
            default: villian_Data.Add(newCard); break;
        }
    }

    void LoadCardSublist(string cardRef)
    {
        CData_Sublist newCard = ScriptableObject.CreateInstance<CData_Sublist>();
        newCard.name = "Loaded Card";
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(cardRef), newCard);
    }

    void SaveStat()
    {
        PlayerPrefs.SetInt(DATAREF_STAT_INT, (int)statType);
    }

    void LoadStat()
    {
        statType = (StatType)PlayerPrefs.GetInt(DATAREF_STAT_INT, 0);
    }

    void SavePool()
    {
        List<List<bool>> poolSet = new List<List<bool>>();

        poolSet.Add(GeneratePoolBool<CData_Default>(locations_Data));

        poolSet.Add(GeneratePoolBool<CData_Default>(goal_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(villian_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(enemy_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(twist_Data));
        poolSet.Add(GeneratePoolBool<CData_Sublist>(races_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(backGrounds_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(classes_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(drive_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(entrance_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(combat_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(setback_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(challenge_Data));
        poolSet.Add(GeneratePoolBool<CData_Default>(boss_Data));
        PlayerPrefs.SetString(DATAREF_POOL_LIST, JsonHelper.NestedToJson<bool>(poolSet));

    }

    void LoadPool()
    {
        string jsonResult = PlayerPrefs.GetString(DATAREF_POOL_LIST, "null");
        if (jsonResult == "null")
        {
            return;
        }
        else
        {
            SetPool(JsonHelper.JsonToNested<bool>(jsonResult), false);
        }
    }

    public void ResetAll()
    {
        //Adventure
        ResetList<CData_Default>(locations_Data);
        ResetList<CData_Default>(goal_Data);
        ResetList<CData_Default>(villian_Data);
        ResetList<CData_Default>(enemy_Data);
        ResetList<CData_Default>(twist_Data);
        //Adventuerers
        ResetList<CData_Sublist>(races_Data);
        ResetList<CData_Default>(backGrounds_Data);
        ResetList<CData_Default>(classes_Data);
        ResetList<CData_Default>(drive_Data);
        //Dungeon
        ResetList<CData_Default>(entrance_Data);
        ResetList<CData_Default>(combat_Data);
        ResetList<CData_Default>(setback_Data);
        ResetList<CData_Default>(challenge_Data);
        ResetList<CData_Default>(boss_Data);
        //Stats
        statType = StatType.Default;
    }

    //Menu stuff
    [ContextMenu("Purge all Data")]
    public void CleanAllData()
    {
        PlayerPrefs.DeleteAll();
        ResetAll();
    }

}
