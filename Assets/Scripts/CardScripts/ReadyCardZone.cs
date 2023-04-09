using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyCardZone : MonoBehaviour
{
    public GameObject TestText; //�r����ܼƭȬO�_���T�p��

    public List<GameObject> ReadyCards; //�ǳƧ������Ҧ��d��
    public static int DrawAmoumt; //�����ɩ�P��
    public static int[,] TypeValue = new int[5,1]; //����(����/���z/�k�N/�P�P/��P),�ƭ�
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
