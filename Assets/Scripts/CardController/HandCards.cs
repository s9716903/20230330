using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
    public GameObject PlayerDeck; //玩家的牌組
    public List<GameObject> HandAllCard; //手牌的卡片

     void OnEnable()
    {
        for (int i = 0; i <= 4; i++) //起手發五張牌
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[i], this.transform); //卡片變成手牌子物件
            HandAllCard.Add(transform.GetChild(i).gameObject); //手牌列表
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveRange(0,4); //移除牌組起手數量
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
