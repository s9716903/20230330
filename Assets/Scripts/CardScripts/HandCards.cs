using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    public List<GameObject> HandAllCard; //手牌所有卡片(List)
    public GameObject PlayerDeck; //對應的玩家牌組
    public GameObject ThisPlayer; //對應的玩家
    public GameObject ThisEnemy; //對應的敵人
    public GameObject ThisTrashCardZone; //對應的棄牌區

    private GridLayoutGroup gridLayoutGroup; //手牌區大小

    public GameObject TestText; //字體顯示數值是否正確計算

    public int[,] TypeValue;


    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        ThisPlayer.GetComponent<Player>().NormalDrawAmount = 5;
        StartCoroutine(NormalDraw());
    }
    // Update is called once per frame
    void Update()
    {
        TypeValue = new int[5, 1] { { ThisPlayer.GetComponent<Player>().MoveValue }, { ThisEnemy.GetComponent<Player>().PhysicDamage }, { ThisEnemy.GetComponent<Player>().MagicDamage }, { ThisPlayer.GetComponent<Player>().Stars }, { ThisPlayer.GetComponent<Player>().HealthDrawAmount } }; //玩家打出的數值(種類(移動/物理/法術/星星/抽牌),數值)
        for (int a = 0; a <= 4; a++) //卡片數值字體
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        for (int b = 0; b < transform.childCount; b++) //判斷底下的卡片是否能與滑鼠互動
        {
            var handcard = transform.GetChild(b);
            if (ThisPlayer.GetComponent<Player>().isReady == false && ThisPlayer.GetComponent<Player>().canMove == false)
            {
                handcard.GetComponent<CardManager>().isPlayerUse = true;
            }
            else
            {
                handcard.GetComponent<CardManager>().isPlayerUse = false;
            }
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
        if (ThisPlayer.GetComponent<Player>().canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var handcard = transform.GetChild(i).GetComponent<CardManager>();
                if (handcard.isUseThisCard)
                {
                    if (handcard.ID == 0)
                    {
                        ThisPlayer.GetComponent<Player>().MoveValue += handcard.Value;
                    }
                    else if (handcard.ID == 1)
                    {
                        if (handcard.Type == 0)
                        {
                            ThisEnemy.GetComponent<Player>().PhysicDamage += handcard.Value;
                        }
                        else if (handcard.Type == 1)
                        {
                            ThisEnemy.GetComponent<Player>().MagicDamage += handcard.Value;
                        }
                    }
                    else if(handcard.ID == 2)
                    {
                        ThisPlayer.GetComponent<Player>().Stars += handcard.Value;
                    }
                    else if (handcard.ID == 3)
                    {
                        ThisPlayer.GetComponent<Player>().HealthDrawAmount += handcard.Value;
                    }
                }
            }
        }
        else if (!ThisPlayer.GetComponent<Player>().canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var handcard = transform.GetChild(i).GetComponent<CardManager>();
                if (handcard.isUseThisCard)
                {
                    if (handcard.ID == 0)
                    {
                        ThisPlayer.GetComponent<Player>().MoveValue -= handcard.Value;
                    }
                    else if (handcard.ID == 1)
                    {
                        if (handcard.Type == 0)
                        {
                            ThisEnemy.GetComponent<Player>().PhysicDamage -= handcard.Value;
                        }
                        else if (handcard.Type == 1)
                        {
                            ThisEnemy.GetComponent<Player>().MagicDamage -= handcard.Value;
                        }
                    }
                    else if (handcard.ID == 2)
                    {
                        ThisPlayer.GetComponent<Player>().Stars += handcard.Value;
                    }
                    else if (handcard.ID == 3)
                    {
                        ThisPlayer.GetComponent<Player>().HealthDrawAmount -= handcard.Value;
                    }
                }
            }
        }
    }

    public void PlayerIsReady() //If press targetlocation,player won't do anything,card will ready
    {
        if (DuelStateManager.canInterect && ThisPlayer.GetComponent<Player>().canMove && DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if (targetcard.gameObject.GetComponent<CardManager>().candraw == true)
                    {
                        ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    }
                }
                if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if (targetcard.gameObject.GetComponent<CardManager>().candraw == true)
                    {
                        ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    }
                }
            }
            for (int j = 0; j < transform.childCount; j++)
            {
                var targetcard = transform.GetChild(j);
                if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
                {
                    targetcard.GetComponent<CardManager>().isUseThisCard = false;
                    targetcard.GetComponent<CardManager>().isDropThisCard = false;
                    targetcard.gameObject.SetActive(false);
                }
                targetcard.GetComponent<CardManager>().isUseThisCard = false;
                targetcard.GetComponent<CardManager>().isDropThisCard = false;
            }
            ThisPlayer.GetComponent<Player>().canMove = false;
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
        else if (!ThisPlayer.GetComponent<Player>().canMove && !ThisPlayer.GetComponent<Player>().isReady && StateTimer.stopStateTime == true && DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
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
            ThisPlayer.GetComponent<Player>().canMove = false;
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
    }

    public void PlayerAttackIsReady() //If press targetlocation,player won't do anything,card will ready
    {
        if (DuelStateManager.canInterect && ThisPlayer.GetComponent<Player>().canMove && DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if (targetcard.gameObject.GetComponent<CardManager>().candraw == true)
                    {
                        ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    }
                }
                if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if (targetcard.gameObject.GetComponent<CardManager>().candraw == true)
                    {
                        ThisPlayer.GetComponent<Player>().NormalDrawAmount++;
                    }
                }
            }
            for (int j = 0; j < transform.childCount; j++)
            {
                var targetcard = transform.GetChild(j);
                if ((targetcard.GetComponent<CardManager>().isUseThisCard == false) && (targetcard.GetComponent<CardManager>().isDropThisCard == false))
                {
                    targetcard.GetComponent<CardManager>().isUseThisCard = false;
                    targetcard.GetComponent<CardManager>().isDropThisCard = false;
                    targetcard.gameObject.SetActive(false);
                }
                targetcard.GetComponent<CardManager>().isUseThisCard = false;
                targetcard.GetComponent<CardManager>().isDropThisCard = false;
            }
            ThisPlayer.GetComponent<Player>().canMove = false;
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
        else if (!ThisPlayer.GetComponent<Player>().canMove && !ThisPlayer.GetComponent<Player>().isReady && StateTimer.stopStateTime == true && DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
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
            ThisPlayer.GetComponent<Player>().canMove = false;
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
    }


    public void HealthDrawCard()
    {
        StartCoroutine(HealthDraw());
        ThisPlayer.GetComponent<Player>().HealthDrawAmount = 0;
    }

    public void NormalDrawCard()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Draw)
        {
            ThisPlayer.GetComponent<Player>().NormalDrawAmount = 0;
            ThisPlayer.GetComponent<Player>().NormalDrawAmount += 1;
            StartCoroutine(NormalDraw());
        }
        else
        {
            for (int i = 0; i < ThisPlayer.GetComponent<Player>().NormalDrawAmount; i++) //Draw card
            {
                StartCoroutine(NormalDraw());
            }
        }
    }

    public void ShowHandCard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Destroy(transform.GetChild(i).gameObject);
                ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Add(HandAllCard[i]);//手牌加入棄牌List
                HandAllCard[i] = null; //牌組List中移除卡牌
            }
            else if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }    
        }
        HandAllCard.RemoveAll(HandAllCard => HandAllCard == null);
    }

    public IEnumerator NormalDraw()
    {
        for (int i = 0; i < ThisPlayer.GetComponent<Player>().NormalDrawAmount; i++)
        {
            var deckcard = PlayerDeck.GetComponent<Deck>().DeckAllCard[0];
            Instantiate(deckcard, PlayerDeck.transform); //卡片變成手牌子物件
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != transform.position)
            {
                thisdeckcard.transform.position = Vector3.MoveTowards(thisdeckcard.transform.position, transform.position, 250 * Time.deltaTime);
                yield return 0;
            }
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //卡牌加入手牌List
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //卡片變成手牌子物件
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //牌組List中移除卡牌*/
            Destroy(thisdeckcard);
            transform.GetChild(index:transform.childCount-1).GetComponent<CardTurnOver>().CardStartTop();
            yield return 0;
        }
    }
    public IEnumerator HealthDraw()
    {
        for (int i = 0; i < ThisPlayer.GetComponent<Player>().HealthDrawAmount; i++)
        {
            var deckcard = PlayerDeck.GetComponent<Deck>().DeckAllCard[0];
            Instantiate(deckcard, PlayerDeck.transform); //卡片變成手牌子物件
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != transform.position)
            {
                thisdeckcard.transform.position = Vector3.MoveTowards(thisdeckcard.transform.position, transform.position, 250 * Time.deltaTime);
                yield return 0;
            }
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //卡牌加入手牌List
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //卡片變成手牌子物件
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //牌組List中移除卡牌*/
            Destroy(thisdeckcard);
            transform.GetChild(index: transform.childCount - 1).GetComponent<CardTurnOver>().CardStartTop();
            yield return 0;
        }
    }
}
