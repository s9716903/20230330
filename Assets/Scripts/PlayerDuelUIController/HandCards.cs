using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    public List<GameObject> HandAllCard; //該玩家手牌(List)
    public GameObject PlayerDeck; //該玩家牌組
    public GameObject ThisPlayer; //該玩家棋子
    public GameObject ThisEnemy; //該玩家敵方棋子
    public GameObject ThisTrashCardZone; //該玩家棄牌區

    private GridLayoutGroup gridLayoutGroup; //HandCardZone Group

    public int[,] TypeValue;  //卡片數值種類


    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        ThisPlayer.GetComponent<Player>().NormalDrawAmount = 5;
        StartCoroutine(NormalDraw());
    }
    // Update is called once per frame
    void Update()
    {
        TypeValue = new int[5, 1] {
            { ThisPlayer.GetComponent<Player>().MoveValue }, 
            { ThisEnemy.GetComponent<Player>().PhysicDamage }, 
            { ThisEnemy.GetComponent<Player>().MagicDamage }, 
            { ThisPlayer.GetComponent<Player>().Stars }, 
            { ThisPlayer.GetComponent<Player>().HealthDrawAmount }
        }; //玩家打出的數值(種類(移動/物理/法術/星星/抽牌),數值)

        ThisPlayer.GetComponent<Player>().Hp = HandAllCard.Count;

        for (int b = 0; b < transform.childCount; b++) //判斷底下的卡片是否能與滑鼠互動
        {
            var handcard = transform.GetChild(b);
            if (ThisPlayer.GetComponent<Player>().isReady == false && ThisPlayer.GetComponent<Player>().canMove == false && ThisPlayer.name == "Player")
            {
                handcard.GetComponent<CardManager>().isPlayerUse = true;
            }
            else
            {
                handcard.GetComponent<CardManager>().isPlayerUse = false;
            }
        }

            //手牌越多時縮減每張牌的間距
        if (gridLayoutGroup.cellSize.x <= 70)
        {
            gridLayoutGroup.cellSize = new Vector2(70, 100);
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(130 - (transform.childCount * 5), 100);
        }
    }
    public void PlayerCardValueReady() //If Card by choose,CardValue will show
    {
        //準備中的卡片數值總和計算/攻擊是否有造成傷害
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
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
                    else if (handcard.ID == 2)
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
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var handcard = transform.GetChild(i).GetComponent<CardManager>();
                var PlayerLocation = ThisPlayer.GetComponent<Player>().TargetLocation;
                var EnemyLocation = ThisEnemy.GetComponent<Player>().TargetLocation;
                if (handcard.isUseThisCard)
                {
                    if (handcard.ID == 1)
                    {
                        if (handcard.Type == 0)
                        {
                            for (int a = 0; a < handcard.AttackZone.Length; a++)
                            {
                                if (PlayerLocation + handcard.AttackZone[a] == EnemyLocation)
                                {
                                    ThisEnemy.GetComponent<Player>().PhysicDamage += handcard.Value;
                                }
                            }
                        }
                        else if (handcard.Type == 1)
                        {
                            for (int b = 0; b < handcard.AttackZone.Length; b++)
                            {
                                if (handcard.AttackZone[b] == EnemyLocation)
                                {
                                    ThisEnemy.GetComponent<Player>().MagicDamage += handcard.Value;
                                }
                            }
                        }
                    }
                    else if (handcard.ID == 2)
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
    }

    public void PlayerIsReady()
    {
        if (ThisPlayer.GetComponent<Player>().canMove && !ThisPlayer.GetComponent<Player>().isReady && DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
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
                    targetcard.gameObject.SetActive(false);
                }
            }
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
    }

    public void PlayerAttackReady()
    {
        if (ThisPlayer.GetComponent<Player>().canMove && ThisPlayer.GetComponent<Player>().isReady && DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
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
                    targetcard.gameObject.SetActive(false);
                }
            }
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
    }
    public void PlayerDamageReady()
    {
        if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().DamagedDropCard == true)
                {
                    ThisPlayer.GetComponent<Player>().DamageDropAmount += 1;
                }
            }
            if (ThisPlayer.GetComponent<Player>().DamageDropAmount == ThisPlayer.GetComponent<Player>().AllDamaged)
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    var targetcard = transform.GetChild(j);
                    if (targetcard.GetComponent<CardManager>().DamagedDropCard == true)
                    {
                        targetcard.transform.position -= new Vector3(0, 0, 10);
                    }
                    else
                    {
                        targetcard.gameObject.SetActive(false);
                    }
                    ThisPlayer.GetComponent<Player>().canMove = true;
                    ThisPlayer.GetComponent<Player>().isReady = true;
                }
            }
            else
            {
                ThisPlayer.GetComponent<Player>().DamageDropAmount = 0;
            }
        }
    }

    public void PlayerIdleReady()
    {
        if (ThisPlayer.GetComponent<Player>().canMove == false && ThisPlayer.GetComponent<Player>().isReady == false && DuelStateManager.duelStateType != GameState.DuelStateMode.AttackResult)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    targetcard.GetComponent<CardManager>().isUseThisCard = false;
                }
                if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    targetcard.GetComponent<CardManager>().isDropThisCard = false;
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
            Debug.Log(ThisPlayer.name + "Draw:" + ThisPlayer.GetComponent<Player>().NormalDrawAmount);
        }
        else if (ThisPlayer.GetComponent<Player>().canMove == false && ThisPlayer.GetComponent<Player>().isReady == false && DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult)
        {
            var needDrop = ThisPlayer.GetComponent<Player>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().DamagedDropCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    targetcard.GetComponent<CardManager>().DamagedDropCard = false;
                }
            }
            for (int j = 0; j < needDrop.AllDamaged; j++)
            {
                var targetcard = transform.GetChild(j);
                targetcard.GetComponent<CardManager>().DamagedDropCard = true;
            }
            for (int k = 0; k < transform.childCount; k++)
            {
                var targetcard = transform.GetChild(k);
                if (targetcard.GetComponent<CardManager>().DamagedDropCard == false)
                {
                    targetcard.gameObject.SetActive(false);
                }
            }
        }
    }


    public void HealthDrawCard()
    {
        StartCoroutine(HealthDraw());
    }

    public void NormalDrawCard()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Draw)
        {
            ThisPlayer.GetComponent<Player>().NormalDrawAmount += 1;
            StartCoroutine(NormalDraw());
        }
        else
        {
            StartCoroutine(NormalDraw());
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
            if (ThisPlayer.GetComponent<Player>().Hp == ThisPlayer.GetComponent<Player>().MaxHp)
            {
                ThisPlayer.GetComponent<Player>().NormalDrawAmount = 0;
                yield return new WaitForSeconds(0.5f);
                ThisPlayer.GetComponent<Player>().isReady = true;
                yield break;
            }
            if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return new WaitForSeconds(1);
            }
            if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }
            var deckcard = PlayerDeck.GetComponent<Deck>().DeckAllCard[0];
            Instantiate(deckcard, PlayerDeck.transform); //卡片變成手牌子物件
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != transform.position)
            {
                thisdeckcard.transform.position = Vector3.MoveTowards(thisdeckcard.transform.position, transform.position, 250 * Time.deltaTime);
                yield return 0;
            }
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //卡牌加入手牌List
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], transform); //卡片變成手牌子物件
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //牌組List中移除卡牌
            Destroy(thisdeckcard);
            if (ThisPlayer.name == "Player")
            {
                transform.GetChild(index: transform.childCount - 1).GetComponent<CardTurnOver>().CardStartTop();
            }
            yield return 0;
        }
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Draw)
        {
            yield return new WaitForSeconds(1f);
            ThisPlayer.GetComponent<Player>().isReady = true;
        }
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.End)
        {
            if (ThisPlayer.GetComponent<Player>().NormalDrawAmount == 0)
            {
                yield return new WaitForSeconds(0.5f);
                ThisPlayer.GetComponent<Player>().isReady = true;
            }
            else
            {
                yield return new WaitForSeconds(1f);
                ThisPlayer.GetComponent<Player>().isReady = true;
            }
        }
        ThisPlayer.GetComponent<Player>().NormalDrawAmount = 0;
    }
    public IEnumerator HealthDraw()
    {
        for (int i = 0; i < ThisPlayer.GetComponent<Player>().HealthDrawAmount; i++)
        {
            if(ThisPlayer.GetComponent<Player>().Hp == ThisPlayer.GetComponent<Player>().MaxHp)
            {
                ThisPlayer.GetComponent<Player>().HealthDrawAmount = 0;
                yield return new WaitForSeconds(0.5f);
                ThisPlayer.GetComponent<Player>().isReady = true;
                yield break;
            }
            if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return new WaitForSeconds(1);
            }
            if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }
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
            if (ThisPlayer.name == "Player")
            {
                transform.GetChild(index: transform.childCount - 1).GetComponent<CardTurnOver>().CardStartTop();
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        ThisPlayer.GetComponent<Player>().HealthDrawAmount = 0;
        ThisPlayer.GetComponent<Player>().isReady = true;
    }
    public IEnumerator TrashCardBackDeck()
    {

            var trashcard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject[0];
            Instantiate(trashcard, ThisTrashCardZone.transform); //卡片變成棄牌區子物件
            var thistrashcard = ThisTrashCardZone.transform.GetChild(1).gameObject;
            while (thistrashcard.transform.position != PlayerDeck.transform.position)
            {
                thistrashcard.transform.position = Vector3.MoveTowards(thistrashcard.transform.position, PlayerDeck.transform.position, 300 * Time.deltaTime);
                yield return 0;
            }
            PlayerDeck.GetComponent<Deck>().DeckAllCard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.GetRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //卡牌加入牌組List
            PlayerDeck.GetComponent<Deck>().DeckImage.enabled = true;
            ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.RemoveRange(0, ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count); //牌組List中移除卡牌*/
            Destroy(thistrashcard);
            yield return new WaitForSeconds(0);
        yield return 0;
        PlayerDeck.GetComponent<Deck>().isDeckNull = false;
    }
}
