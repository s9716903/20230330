using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCards : MonoBehaviour
{
    public GameObject PlayerDeck; //玩家的牌組
    public List<GameObject> HandAllCard; //手牌的卡片
    private GridLayoutGroup gridLayoutGroup;

     void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //起手發五張牌
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[i], this.transform); //卡片變成手牌子物件
            HandAllCard.Add(transform.GetChild(i).gameObject); //手牌列表
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveRange(0,5); //移除牌組5張
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 6)
        {
            gridLayoutGroup.cellSize = new Vector2(150 - (transform.childCount * 6), 100);
            if (gridLayoutGroup.cellSize.x <= 50)
            {
                gridLayoutGroup.cellSize = new Vector2(50, 100);
            }
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(150, 100);
        }
    }   
}
