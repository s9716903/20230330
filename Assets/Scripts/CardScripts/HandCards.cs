using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    //public List<GameObject> HandAllCard; //手牌區的卡片(List)
    public GameObject PlayerDeck; //玩家牌組
    private GridLayoutGroup gridLayoutGroup; //手牌區大小

    public static int DrawAmoumt; //結束時抽牌數

    public GameObject TestText; //字體顯示數值是否正確計算

    
    public static int[,] TypeValue = new int[5, 1]; //玩家打出的數值(種類(移動/物理/法術/星星/抽牌),數值)

    void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //起手發五張牌
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //卡片變成手牌子物件
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //從牌組中移除卡牌
            //HandAllCard.Add(transform.GetChild(i).gameObject); //手牌列表
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int a = 0; a <= 4; a++) //卡片數值字體(測試用)
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        if (transform.childCount > 6) //手牌越多時縮減每張牌的間距
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

    public void StateReady()
    {
        /*for (int i = 0; i < HandAllCard.Count; i++) 
        {
            if (HandAllCard[i].GetComponent<CardManager>().isUseThisCard == true)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else if (HandAllCard[i].GetComponent<CardManager>().isUseThisCard == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }*/
        for (int i = 0; i < transform.childCount; i++)
        {
            var targetcard = transform.GetChild(i);
            if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
            {
                targetcard.gameObject.SetActive(true);
                if ((targetcard.gameObject.GetComponent<CardManager>().ID != 2) && (targetcard.gameObject.GetComponent<CardManager>().ID != 3))
                {
                   DrawAmoumt++;
                }
            }

            if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
            {
                targetcard.gameObject.SetActive(true);
                if ((targetcard.gameObject.GetComponent<CardManager>().ID != 2) && (targetcard.gameObject.GetComponent<CardManager>().ID != 3))
                {
                    DrawAmoumt++;
                }
            }
            
            if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
            {
               targetcard.gameObject.SetActive(false);
            }
        }
        Debug.Log("Draw:" + DrawAmoumt);
    }
}
