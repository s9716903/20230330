using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardValueManager cardvaluemanager; //�d�����
    private bool isCardUp;
    public bool candraw; //�Χ��O�_�i��P
    public int Type; //����
    public string Name; //�W�r
    public int Value; //�ƭ�
    private void OnEnable()
    {
        isCardUp = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCardUp == true)
        {
            candraw = cardvaluemanager.cardvalue.candraw;
            Type = cardvaluemanager.cardvalue.Type;
            Name = cardvaluemanager.cardvalue.Name;
            Value = cardvaluemanager.cardvalue.Value;
        }
        if (isCardUp == false)
        {
            candraw = cardvaluemanager.cardvalue.candraw2;
            Type = cardvaluemanager.cardvalue.Type2;
            Name = cardvaluemanager.cardvalue.Name2;
            Value = cardvaluemanager.cardvalue.Value2;
        }

        if (Input.GetMouseButtonDown(0))
        { 
        
        }
        if (Input.GetMouseButtonDown(1))
        {
            isCardUp = !isCardUp;
        }
    }
}
