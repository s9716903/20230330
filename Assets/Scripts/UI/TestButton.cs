using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameObject HandCards;
    public void ChangeState()
    {
        for (int i = 0; i < HandCards.transform.childCount; i++)
        {
            if (HandCards.transform.GetChild(i).GetComponent<CardManager>().isUseThisCard)
            {

            }
            HandCards.transform.GetChild(i).gameObject.SetActive(true);
            HandCards.transform.GetChild(i).GetComponent<CardManager>().isUseThisCard = false;
        }
        GameManager.duelStateMode++; //¶¥¬q«e¶i      
    }
}
