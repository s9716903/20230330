using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandCards : MonoBehaviour
{
    public GameObject PlayerDeck; //���a���P��
    //public List<GameObject> HandAllCard; //��P���d��
    private GridLayoutGroup gridLayoutGroup;

    public GameObject TestText; //�r����ܼƭȬO�_���T�p��

    public static int DrawAmoumt; //�����ɩ�P��
    public static int[,] TypeValue = new int[5, 1]; //����(����/���z/�k�N/�P�P/��P),�ƭ�

    void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //�_��o���i�P
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[0], this.transform); //�d���ܦ���P�l����
            PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveAt(0);
            //HandAllCard.Add(transform.GetChild(i).gameObject); //��P�C��
        }
        //PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveRange(0,5); //�����P��5�i
    }
    // Update is called once per frame
    void Update()
    {
        for (int a = 0; a <= 4; a++)
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
            else if (targetcard.GetComponent<CardManager>().isUseThisCard == false)
            {
               targetcard.gameObject.SetActive(false);
            }
        }
        Debug.Log("Draw:" + DrawAmoumt);
    }
}
