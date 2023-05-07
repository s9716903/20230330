﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Deck : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image DeckImage;

    public bool isDeckNull;

    public GameObject DeckCardsText; //卡片數量文字

    public GameObject[] DeckCardType; //牌組卡片種類大全
    public int[] CardQuantity; //各自卡片數量
    public List<GameObject> DeckAllCard; //牌組所有卡片(List)


    private void Awake()
    {
        isDeckNull = false;
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
        
        DeckCardsText.SetActive(false);
        DeckImage = GetComponent<Image>();
        DeckImage.enabled = true;
    }
    private void Update()
    {
        if (DeckAllCard.Count != 0)
        {
            DeckCardsText.GetComponent<TextMeshProUGUI>().text = DeckAllCard.Count.ToString();
        }

        if (DeckAllCard.Count == 0)
        {
            isDeckNull = true;
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //滑鼠游標移入
    {
        DeckCardsText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //滑鼠游標移出
    {
        DeckCardsText.SetActive(false);
    }
}
