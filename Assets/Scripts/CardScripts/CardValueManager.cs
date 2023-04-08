using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    //�ǦC��CardValue��ƨ��নScriptableObject
    public CardValue cardValue;
}
[System.Serializable]
public class CardValue //��¦�d�����
{
    //�d�����
    public int ID; //�d��ID
    public bool candraw; //�Χ��O�_�i��P
    public string Name; //�W�r
    public int Value; //�ƭ�
    public int Type; //��������
    public int[] CanAttack = new int[5]; //�i������m
}
