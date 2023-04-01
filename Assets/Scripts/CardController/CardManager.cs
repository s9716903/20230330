using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardValueManager cardvaluemanager; //�d�����

    private bool isUseThisCard;
    private bool isCardUp; //�d���O�_������m
    private int[] PlayerZone = new int[] { 0, 1, 2, 3, 4 }; //�����d��
   
    public bool candraw; //�Χ��O�_�i��P
    public int[] CanAttack = new int[5]; //�i������m

    [Header("CardValue")]
    public int Type; //����
    public string Name; //�W�r
    public int Value; //�ƭ�

    private void OnEnable()
    {
        isCardUp = true;
        isUseThisCard = false;
    }
    // Update is called once per frame
    void Update()
    {
        //�d������m
        if (isCardUp == true)
        {
            candraw = cardvaluemanager.cardvalue.candraw;
            CanAttack = cardvaluemanager.cardvalue.CanAttack;
            Type = cardvaluemanager.cardvalue.Type;
            Name = cardvaluemanager.cardvalue.Name;
            Value = cardvaluemanager.cardvalue.Value;
        }

        //�d���f��m
        if (isCardUp == false)
        {
            candraw = cardvaluemanager.cardvalue.candraw2;
            CanAttack = cardvaluemanager.cardvalue.CanAttack2;
            Type = cardvaluemanager.cardvalue.Type2;
            Name = cardvaluemanager.cardvalue.Name2;
            Value = cardvaluemanager.cardvalue.Value2;
        }

        //�ƹ�����d����
        if (Input.GetMouseButtonDown(0))
        {
            isUseThisCard = !isUseThisCard;
        }

        //�ƹ��k��d����
        if (Input.GetMouseButtonDown(1))
        {
            isCardUp = !isCardUp;
        }
    }
    private void UseCard()
    {
        if (isUseThisCard)
        {
            
        }
        else
        { 
            
        }
    }
}
