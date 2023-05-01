using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Deck : MonoBehaviour
{
    private Image DeckImage;

    public GameObject[] DeckCardType; //牌組卡片種類大全
    public int[] CardQuantity; //各自卡片數量
    public List<GameObject> DeckAllCard; //牌組所有卡片(List)


    private void Awake()
    {
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
    private void Start()
    {
        DeckImage = GetComponent<Image>();
    }
    private void Update()
    {
        if (DeckAllCard.Count == 0)
        {
            DeckImage.enabled = false;
        }
        else
        {
            DeckImage.enabled = true;
        }
    }
}
