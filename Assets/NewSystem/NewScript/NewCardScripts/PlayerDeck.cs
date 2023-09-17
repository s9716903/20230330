using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDeck : MonoBehaviour
{
    //public static PlayerDeck playerDeck;
    public List<Card> CardList = new List<Card>();
    public TextAsset decklist;

    // Start is called before the first frame update
    void Start()
    {
        LoadDeckData();
        ShuffleDeck();
        Debug.Log(CardList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //TestLoadCard();
        }
        //Debug.Log(CardList.Count);
    }
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
        //¬~µP
        for (int i = 0; i < CardList.Count; i++)
        {
            var temp = CardList[i];
            int RandomIndex = Random.Range(0, CardList.Count);
            CardList[i] = CardList[RandomIndex];
            CardList[RandomIndex] = temp;
        }
    }
    /*public void TestLoadCard()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newcard = Instantiate(CardPrefab, PlayerHandCard.transform);
            newcard.GetComponent<NewCardValueManager>().card = CardList[0];
            CardList.RemoveAt(0);
        }
    }*/
}
