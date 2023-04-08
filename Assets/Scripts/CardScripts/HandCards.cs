using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCards : MonoBehaviour
{
    public GameObject PlayerDeck; //���a���P��
    public List<GameObject> HandAllCard; //��P���d��
    private GridLayoutGroup gridLayoutGroup;

     void OnEnable()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i <= 4; i++) //�_��o���i�P
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[i], this.transform); //�d���ܦ���P�l����
            HandAllCard.Add(transform.GetChild(i).gameObject); //��P�C��
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveRange(0,5); //�����P��5�i
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