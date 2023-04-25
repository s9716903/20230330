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

    public GameObject TestText; //�r����ܼƭȬO�_���T�p��

    public static int[,] TypeValue = new int[5, 1]; //���a���X���ƭ�(����(����/���z/�k�N/�P�P/��P),�ƭ�)


    void Start()
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
        for (int a = 0; a <= 4; a++) //�d���ƭȦr��
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }

        for (int b = 0; b < transform.childCount; b++) //�P�_���U���d���O�_��P�ƹ�����
        {
            var handcard = transform.GetChild(b);
            if (Player.isReady == false && Player.canMove == false)
            {
                handcard.GetComponent<CardManager>().isCardStateTrue = true;
            }
            else
            {
                handcard.GetComponent<CardManager>().isCardStateTrue = false;
            }
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

    public void PlayerIsReady()
    {
        if (DuelStateManager.canInterect && Player.canMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var targetcard = transform.GetChild(i);
                if (targetcard.GetComponent<CardManager>().isUseThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if ((targetcard.gameObject.GetComponent<CardManager>().ID != 2) && (targetcard.gameObject.GetComponent<CardManager>().ID != 3))
                    {
                        Player.DrawAmoumt++;
                    }
                }
                if (targetcard.GetComponent<CardManager>().isDropThisCard == true)
                {
                    targetcard.transform.position -= new Vector3(0, 0, 10);
                    if ((targetcard.gameObject.GetComponent<CardManager>().ID != 2) && (targetcard.gameObject.GetComponent<CardManager>().ID != 3))
                    {
                        Player.DrawAmoumt++;
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
            Player.canMove = false;
            Debug.Log("Draw:" + Player.DrawAmoumt);
        }
    }
}
