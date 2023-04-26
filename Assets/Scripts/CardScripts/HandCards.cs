using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    //public List<GameObject> HandAllCard; //��P�Ϫ��d��(List)
    public GameObject PlayerDeck; //���a�P��
    private GridLayoutGroup gridLayoutGroup; //��P�Ϥj�p

    public static int DrawAmoumt; //�����ɩ�P��

    public GameObject TestText; //�r����ܼƭȬO�_���T�p��

<<<<<<< HEAD
    public static bool isReady = false; //�ǳƧ���

    public static int[,] TypeValue = new int[5, 1]; //���a���X���ƭ�(����(����/���z/�k�N/�P�P/��P),�ƭ�)
=======
    public GameObject TrashCardZone; 

    public static int[,] TypeValue = new int[5, 1] { {Player.MoveValue },{Enemy.PhysicDamage },{Enemy.MagicDamage },{Player.Stars },{Player.DrawAmoumt} }; //���a���X���ƭ�(����(����/���z/�k�N/�P�P/��P),�ƭ�)
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b

    void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //�_��o���i�P
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�q�P�դ������d�P
            //HandAllCard.Add(transform.GetChild(i).gameObject); //��P�C��
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int a = 0; a <= 4; a++) //�d���ƭȦr��(���ե�)
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        if (transform.childCount > 6) //��P�V�h���Y��C�i�P�����Z
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
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�q�P�դ������d�P
            }
        }
        else
        {
            for (int i = 0; i < Player.DrawAmoumt; i++) //Draw card
            {
                Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
                PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0); //�q�P�դ������d�P
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
