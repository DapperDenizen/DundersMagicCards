using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainSetup : MonoBehaviour
{
    public enum Mode { Character = 0, Adventure = 1, FiveDungeon = 2, NA = 3 }
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject defaultCardPrefab, statCardPrefab, raceCardPrefab, dungeonCardPrefab;
    [SerializeField] Transform cardHolder;
    [SerializeField] GameObject choiceHolder, optionHolder, chooseButton, LeftArrow, RightArrow;
    [SerializeField]  Image CentreLock;
    [SerializeField] ScrollBuddy sBuddy;
    [SerializeField] ChooseAnimator choosAnim;
    Mode currMode = Mode.NA;
    List<CardBase> cards = new List<CardBase>();
    string[] titles = new string[] { "Choose your fate"};
    public bool optUp = true;


    void Start()
    {
        Data.instance.SetUpLists();
        choiceHolder.SetActive(true);
    }


    public void GenerateButton(int i)
    {
        //hide chooser
        optUp = false;
        choosAnim.OnChoice();
        currMode = (Mode)i;
        Invoke("StartGeneration", 0.9f);
    }

    void StartGeneration()
    {
        chooseButton.SetActive(true);
        sBuddy.ResetNotch();
        if (currMode == Mode.Adventure) { GenerationAdventure(); }
        if (currMode == Mode.Character) { GenerateCharacter(); }
        if (currMode == Mode.FiveDungeon) { GenerateDungeon(); }

        
    }


    void GenerationAdventure()
    {
        titles = new string[]{"Location", "Goal", "Enemy", "Villian", "Twist"};
        //Add Cards
        
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Location);
        
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Goal);
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Enemy);
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Villian);
        cards.Add(Instantiate(dungeonCardPrefab, cardHolder).GetComponent<Card_Dungeon>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Twist);

    }
    void GenerateCharacter()
    {
        titles = new string[] {"Race", "Background", "Class", "Drive", "Stats" };
        //Add Cards
        cards.Add(Instantiate(raceCardPrefab, cardHolder).GetComponent<Card_Sublist_Race>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Race);
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Background);
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Class);
        cards.Add(Instantiate(defaultCardPrefab, cardHolder).GetComponent<Card_Default>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Drive);
        cards.Add(Instantiate(statCardPrefab, cardHolder).GetComponent<Card_Stat>());
        cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Stats);

    }
    void GenerateDungeon()
    {

        titles = new string[] { "Entrance", "Encounter", "Encounter", "Climax", "Twist" };

        CData_Default[] deck = DungeonDeck.getCardSet();
        //Add Cards

        for (int i = 0; i < 4; i++)
        {
            Card_Dungeon card = Instantiate(dungeonCardPrefab, cardHolder).GetComponent<Card_Dungeon>();
            card.cData = deck[i];
            switch (i)
            {
                case 0: card.myDeck = CardBase.CardDeck.Entrance; break;
                case 3: card.myDeck = CardBase.CardDeck.Boss; break;
                default: card.myDeck = CardBase.CardDeck.Encounter; break;
            }
            card.AssignData();
            cards.Add(card);
        }
            cards.Add(Instantiate(dungeonCardPrefab, cardHolder).GetComponent<Card_Dungeon>());
            cards[cards.Count - 1].OnInitiate(CardBase.CardDeck.Twist);
    }

    public void ChoiceOpen()
    {
        optUp = true;
        ClearCards();
        sBuddy.ResetNotch();
        choosAnim.OnReset();
        chooseButton.SetActive(false);
        choiceHolder.SetActive(true);
        title.text = "Choose your fate";
        titles = new string[] { "Choose your fate", "Choose your fate", "Choose your fate", "Choose your fate", "Choose your fate" }; //this is like this to ensure 0% chance of runtime errors
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        CentreLock.gameObject.SetActive(false);
    }


    public void Back()
    {
        if (optUp)
        {
            //go to title
            ToTitle();
        }
        else
        {
            //open chooser
            ChoiceOpen();
        }
    }

    void OpenOptions()
    {
        optionHolder.SetActive(true);
    }

    public void CloseOptions()
    {
        optionHolder.SetActive(false);
    }

    public void ToEditor()
    {
        Data.instance.transitioner.Transition(2, 1);
    }
    public void ToTitle()
    {
        Data.instance.transitioner.TransitionBack();
    }
    public void UpdateTitle()
    {
        //check position of scroll
        //update text based on mode
        if (sBuddy.currNotch > titles.Length - 1)
        {
            title.text = titles[titles.Length - 1];
        }
        else
        {
            title.text = titles[sBuddy.currNotch];
        }

        UpdateArrows();
       
    }

    void UpdateArrows()
    {
        bool l = true;
        bool r = true;

        if (sBuddy.currNotch == 0)
        {
            l = false;
        }

        if (sBuddy.currNotch == titles.Length - 1)
        {
            r = false;
        }

        RightArrow.SetActive(r);
        LeftArrow.SetActive(l);
        CentreLock.gameObject.SetActive(true);
        CentreLock.sprite = cards[sBuddy.currNotch].locked ? Data.instance.artRef.lockImg : Data.instance.artRef.unlockImg;
    }

    public void Increment(int val)
    {
        sBuddy.Increment(val);
    }

    public void Reroll()
    {
        foreach (CardBase card in cards)
        {
            card.OnReroll();
        }
    }

    public void Lock()
    {
        cards[sBuddy.currNotch].LockUnlock(!cards[sBuddy.currNotch].locked);
        CentreLock.sprite = cards[sBuddy.currNotch].locked ? Data.instance.artRef.lockImg : Data.instance.artRef.unlockImg;
    }

    void ClearCards()
    {
        sBuddy.blocker.SetActive(false);
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i].gameObject);
        }
        cards.Clear();
    }
}
