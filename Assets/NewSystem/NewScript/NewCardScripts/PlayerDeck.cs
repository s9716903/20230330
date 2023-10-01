using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Card> CardList = new List<Card>();
    public TextAsset decklist;
    public TextMeshProUGUI DeckCardsText;

    private void Start()
    {
        DeckCardsText.enabled = false;
    }
    private void Update()
    {
        DeckCardsText.text = CardList.Count.ToString();
        Debug.Log(CardList.Count);
    }
    // Start is called before the first frame update
    public void DeckStartSetting()
    {
        LoadDeckData();
        ShuffleDeck();
    }
    // Update is called once per frame
    public void LoadDeckData()
    {
        string[] deckvalue = decklist.text.Split('\n');
        foreach (string deckrow in deckvalue)
        {
            string[] rowArray = deckrow.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if(rowArray[0] == "twincard")
            {
                //Create TwinCard
                int ID = int.Parse(rowArray[1]);
                int Quantity = int.Parse(rowArray[2]);
                int Type1 = int.Parse(rowArray[3]);
                int Value1 = int.Parse(rowArray[4]);
                int Type2 = int.Parse(rowArray[6]);
                int Value2 = int.Parse(rowArray[7]);
                string[] ATKZone = rowArray[5].Split('/');
                int[] AttackZone1 = ATKZone.Select(int.Parse).ToArray();
                TwinCard twin = new TwinCard(ID,Type1, Value1, AttackZone1, Type2, Value2);
                for (int i = 0; i < Quantity; i++)
                {
                    CardList.Add(twin);
                    //Debug.Log(twin.Type1);
                }
            }
            else if (rowArray[0] == "healthcard")
            {
                //Create HealthCard
                int ID = int.Parse(rowArray[1]);
                int Quantity = int.Parse(rowArray[2]);
                int Type = int.Parse(rowArray[3]);
                int Value = int.Parse(rowArray[4]);
                HealthCard health = new HealthCard(ID,Type, Value);
                for (int j = 0; j < Quantity; j++)
                {
                    CardList.Add(health);
                    //Debug.Log(health.Type);
                }
            }
        }
    }
    public void ShuffleDeck()
    {
        //瑍礟
        for (int i = 0; i < CardList.Count; i++)
        {
            var temp = CardList[i];
            int RandomIndex = Random.Range(0, CardList.Count);
            CardList[i] = CardList[RandomIndex];
            CardList[RandomIndex] = temp;
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //菲公村夹簿
    {
        DeckCardsText.enabled = true;   
    }
    public void OnPointerExit(PointerEventData pointerEventData) //菲公村夹簿
    {
        DeckCardsText.enabled = false;
    }
}
