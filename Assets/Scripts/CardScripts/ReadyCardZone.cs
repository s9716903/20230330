using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyCardZone : MonoBehaviour
{
    public GameObject TestText; //字體顯示數值是否正確計算

    public List<GameObject> ReadyCards; //準備完成的所有卡片
    public static int DrawAmoumt; //結束時抽牌數
    public static int[,] TypeValue = new int[5,1]; //種類(移動/物理/法術/星星/抽牌),數值
    // Start is called before the first frame update
    void Start()
    {
        ReadyCards.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (ReadyCards.Count != 0)
        {
            for (int i = 0; i < ReadyCards.Count; i++)
            {
                var T = ReadyCards[i].GetComponent<CardManager>().Type;
                var V = ReadyCards[i].GetComponent<CardManager>().Value;
                TypeValue[T, 0] += V;
            }
        }*/
        for (int a = 0; a <= 4; a++)
        {
            TestText.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text = TypeValue[a, 0].ToString();
        }
        if (GameManager.duelStateMode == GameState.DuelStateMode.MoveState)
        {
            
        }
        if (GameManager.duelStateMode == GameState.DuelStateMode.MainState)
        {

        }
        Debug.Log(TypeValue[0, 0]);
    }
}
