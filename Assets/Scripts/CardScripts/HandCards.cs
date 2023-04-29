using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    public List<GameObject> HandAllCard; //��P�Ҧ��d��(List)
    public GameObject PlayerDeck; //���������a�P��
    public GameObject ThisPlayer; //���������a
    public GameObject ThisEnemy; //�������ĤH
    public GameObject ThisTrashCardZone; //��������P��

    private GridLayoutGroup gridLayoutGroup; //��P�Ϥj�p

    public GameObject TestText; //�r����ܼƭȬO�_���T�p��

    public int[,] TypeValue;


    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //�_��o���i�P
        {
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P
        }
    }
    // Update is called once per frame
    void Update()
    {
        TypeValue = new int[5, 1] { { ThisPlayer.GetComponent<Player>().MoveValue }, { ThisEnemy.GetComponent<Player>().PhysicDamage }, { ThisEnemy.GetComponent<Player>().MagicDamage }, { ThisPlayer.GetComponent<Player>().Stars }, { ThisPlayer.GetComponent<Player>().HealthDrawAmount } }; //���a���X���ƭ�(����(����/���z/�k�N/�P�P/��P),�ƭ�)
        for (int a = 0; a <= 4; a++) //�d���ƭȦr��
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        for (int b = 0; b < transform.childCount; b++) //�P�_���U���d���O�_��P�ƹ�����
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

        /*if (transform.childCount > 6) //��P�V�h���Y��C�i�P�����Z
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
        }*/


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
        for (int i = 0; i < ThisPlayer.GetComponent<Player>().HealthDrawAmount; i++) //Draw card
        {
            HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P
        }
        ThisPlayer.GetComponent<Player>().HealthDrawAmount = 0;
    }

    public void NormalDrawCard()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Draw)
        {
            ThisPlayer.GetComponent<Player>().NormalDrawAmount += 1;
            for (int i = 0; i < ThisPlayer.GetComponent<Player>().NormalDrawAmount; i++) //Draw card
            {
                HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], transform); //�d���ܦ���P�l����
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P
            }
        }
        else
        {
            for (int i = 0; i < ThisPlayer.GetComponent<Player>().NormalDrawAmount; i++) //Draw card
            {
                HandAllCard.Add(PlayerDeck.GetComponent<Deck>().DeckAllCard[0]); //�d�P�[�J��PList
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�P��List�������d�P
            }
        }
        ThisPlayer.GetComponent<Player>().NormalDrawAmount = 0;
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
}
