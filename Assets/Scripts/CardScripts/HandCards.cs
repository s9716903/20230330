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

<<<<<<< HEAD
    public static bool isReady = false; //準備完成

    public static int[,] TypeValue = new int[5, 1]; //玩家打出的數值(種類(移動/物理/法術/星星/抽牌),數值)
=======
    public GameObject TrashCardZone; 

    public static int[,] TypeValue = new int[5, 1] { {Player.MoveValue },{Enemy.PhysicDamage },{Enemy.MagicDamage },{Player.Stars },{Player.DrawAmoumt} }; //玩家打出的數值(種類(移動/物理/法術/星星/抽牌),數值)
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b

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
    public void PlayerCardValueReady() //If press ready,CardValue will show
    {
        if (Player.canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var handcard = transform.GetChild(i).GetComponent<CardManager>();
                if (handcard.isUseThisCard)
                {
                    if (handcard.ID == 1)
                    {
                        TypeValue[handcard.Type + 1, 0] += handcard.Value;
                    }
                    else if (handcard.ID == 0)
                    {
                        TypeValue[handcard.ID, 0] += handcard.Value;
                    }
                    else
                    {
                        TypeValue[handcard.ID + 1, 0] += handcard.Value;
                    }
                }
            }
        }
        else if (!Player.canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var handcard = transform.GetChild(i).GetComponent<CardManager>();
                if (handcard.isUseThisCard)
                {
                    if (handcard.ID == 1)
                    {
                        TypeValue[handcard.Type + 1, 0] -= handcard.Value;
                    }
                    else if (handcard.ID == 0)
                    {
                        TypeValue[handcard.ID, 0] -= handcard.Value;
                    }
                    else
                    {
                        TypeValue[handcard.ID + 1, 0] -= handcard.Value;
                    }
                }
            }
        }
    }

<<<<<<< HEAD
    public void StateReady()
=======
    public void PlayerIsReady() //If press targetlocation,player won't do anything,card will ready
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b
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
<<<<<<< HEAD
        Debug.Log("Draw:" + DrawAmoumt);
=======
        else if (!Player.canMove && !Player.isReady && StateTimer.stopStateTime == true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
                {
                    targetcard.GetComponent<CardManager>().isUseThisCard = false;
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                }
                if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
                {
                    targetcard.GetComponent<CardManager>().isDropThisCard = false;
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                }
            }
            for (int j = 0; j < transform.childCount; j++)
            {
                var targetcard = transform.GetChild(j);
                if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
            Player.canMove = false;
            Debug.Log("Draw:" + Player.DrawAmoumt);
        }
    }

    public void DrawCard()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Draw)
        {
            Player.DrawAmoumt += 1;
            for (int i = 0; i < Player.DrawAmoumt; i++) //Draw card
            {
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //卡片變成手牌子物件
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //從牌組中移除卡牌
            }
        }
        else
        {
            for (int i = 0; i < Player.DrawAmoumt; i++) //Draw card
            {
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //卡片變成手牌子物件
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //從牌組中移除卡牌
            }
        }
        Player.DrawAmoumt = 0;
    }
    public void ShowHandCard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                TrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Add(transform.GetChild(i).gameObject);
            }
            else if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }    
        }
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b
    }
}
