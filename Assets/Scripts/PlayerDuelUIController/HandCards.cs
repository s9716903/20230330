using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    public List<GameObject> HandAllCard; //�Ӫ��a��P(List)
    public GameObject PlayerDeck; //�Ӫ��a�P��
    public GameObject ThisPlayer; //�Ӫ��a�Ѥl
    public GameObject ThisEnemy; //�Ӫ��a�Ĥ�Ѥl
    public GameObject ThisTrashCardZone; //�Ӫ��a��P��

    private GridLayoutGroup gridLayoutGroup; //HandCardZone Group

    public GameObject TestText; //���a�i�o�����ƭ�

    public int[,] TypeValue;  //�d���ƭȺ���


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
        }; //���a���X���ƭ�(����(����/���z/�k�N/�P�P/��P),�ƭ�)
        
        for (int a = 0; a <= 4; a++) //�d���ƭȦr��
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        for (int b = 0; b < transform.childCount; b++) //�P�_���U���d���O�_��P�ƹ�����
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

            //��P�V�h���Y��C�i�P�����Z
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
        //�ǳƤ����d���ƭ��`�M�p��/�����O�_���y���ˮ`
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

    public void PlayerIdleReady()
    {
        if (ThisPlayer.GetComponent<Player>().canMove == false && ThisPlayer.GetComponent<Player>().isReady == false)
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
                ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Add(HandAllCard[i]);//��P�[�J��PList
                HandAllCard[i] = null; //�P��List�������d�P
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
            if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return TrashCardBackDeck();
            }
            if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }
            var deckcard = PlayerDeck.GetComponent<Deck>().DeckAllCard[0];
            Instantiate(deckcard, PlayerDeck.transform); //�d���ܦ���P�l����
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != transform.position)
            {
                thisdeckcard.transform.position = Vector3.MoveTowards(thisdeckcard.transform.position, transform.position, 250 * Time.deltaTime);
                yield return 0;
            }
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P
            Destroy(thisdeckcard);
            if (ThisPlayer.name == "Player")
            {
                transform.GetChild(index: transform.childCount - 1).GetComponent<CardTurnOver>().CardStartTop();
            }
            yield return 0;
        }
        ThisPlayer.GetComponent<Player>().NormalDrawAmount = 0;
    }
    public IEnumerator HealthDraw()
    {
        for (int i = 0; i < ThisPlayer.GetComponent<Player>().HealthDrawAmount; i++)
        {
            if (PlayerDeck.GetComponent<Deck>().isDeckNull)
            {
                yield return StartCoroutine(TrashCardBackDeck());
                yield return TrashCardBackDeck();
            }
            if (PlayerDeck.GetComponent<Deck>().DeckAllCard.Count == 1)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = false;
            }
            var deckcard = PlayerDeck.GetComponent<Deck>().DeckAllCard[0];
            Instantiate(deckcard, PlayerDeck.transform); //�d���ܦ���P�l����
            var thisdeckcard = PlayerDeck.transform.GetChild(0).gameObject;
            while (thisdeckcard.transform.position != transform.position)
            {
                thisdeckcard.transform.position = Vector3.MoveTowards(thisdeckcard.transform.position, transform.position, 250 * Time.deltaTime);
                yield return 0;
            }
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P*/
            Destroy(thisdeckcard);
            if (ThisPlayer.name == "Player")
            {
                transform.GetChild(index: transform.childCount - 1).GetComponent<CardTurnOver>().CardStartTop();
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        for (int j = 0; j < ThisPlayer.GetComponent<Player>().HealthDrawAmount; j++)
        {
            transform.GetChild(index: transform.childCount - (j+1)).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        ThisPlayer.GetComponent<Player>().HealthDrawAmount = 0;
    }
    public IEnumerator TrashCardBackDeck()
    {
        for (int i = 0; i < ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.Count + 1; i++)
        {
            var trashcard = ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject[0];
            Instantiate(trashcard, ThisTrashCardZone.transform); //�d���ܦ���P�Ϥl����
            var thistrashcard = ThisTrashCardZone.transform.GetChild(1).gameObject;
            while (thistrashcard.transform.position != PlayerDeck.transform.position)
            {
                thistrashcard.transform.position = Vector3.MoveTowards(thistrashcard.transform.position, PlayerDeck.transform.position, 300 * Time.deltaTime);
                yield return 0;
            }
            PlayerDeck.GetComponent<Deck>().DeckAllCard.Add(ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject[0]); //�d�P�[�J�P��List
            if (i == 0)
            {
                PlayerDeck.GetComponent<Deck>().DeckImage.enabled = true;
            }
            ThisTrashCardZone.GetComponent<TrashCard>().TrashCardsObject.RemoveAt(0); //�P��List�������d�P*/
            Destroy(thistrashcard);
            yield return new WaitForSeconds(0);
        }
        yield return 0;
        PlayerDeck.GetComponent<Deck>().isDeckNull = false;
    }
}