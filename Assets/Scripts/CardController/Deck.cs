using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    private Image DeckBack; //牌組卡背
    public GameObject[] DeckCardType; //牌組卡片種類大全
    public int[] CardQuantity; //各自卡片數量
    public List<GameObject> DeckAllCard; //牌組所有卡片資料

    private void OnEnable()
    {
        DeckBack = GetComponent<Image>();
        DeckBack.enabled = true;
        //遊戲開始時將卡加入牌組
        DeckAllCard = new List<GameObject>();
        for (int i = 0; i < DeckCardType.Length; i++)
        {
            for (int j = 0; j < CardQuantity[i]; ++j)
            {
                DeckAllCard.Add(DeckCardType[i]);
            }
        }

        //遊戲開始時洗牌
        for (int k = 0; k < DeckAllCard.Count; k++)
        {
            var temp = DeckAllCard[k]; 
            int RandomIndex = Random.Range(0, DeckAllCard.Count);
            DeckAllCard[k] = DeckAllCard[RandomIndex]; 
            DeckAllCard[RandomIndex] = temp; 
        }
    }
    private void Update()
    {
        if (DeckAllCard.Count == 0)
        {
            DeckBack.enabled = false;
        }
        else
        {
            DeckBack.enabled = true;
        }
    }
}
