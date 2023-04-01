using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
    public GameObject PlayerDeck; //���a���P��
    public List<GameObject> HandAllCard; //��P���d��

     void OnEnable()
    {
        for (int i = 0; i <= 4; i++) //�_��o���i�P
        {
            Instantiate(PlayerDeck.GetComponent<Deck>().DeckAllCard[i], this.transform); //�d���ܦ���P�l����
            HandAllCard.Add(transform.GetChild(i).gameObject); //��P�C��
        }
        PlayerDeck.GetComponent<Deck>().DeckAllCard.RemoveRange(0,4); //�����P�հ_��ƶq
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
