using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    //�ǦC��CardValue��ƨ��নScriptableObject
    public CardValue cardvalue; 
}
[System.Serializable]
public class CardValue
{
    //�d���W�b���
    public bool candraw; //�Χ��O�_�i��P
    public int Type; //����
    public string Name; //�W�r
    public int Value; //�ƭ�
    public int[] CanAttack = new int[5]; //�i������m

    //�d���U�b���
    public bool candraw2;
    public int Type2;
    public string Name2;
    public int Value2;
    public int[] CanAttack2 = new int[5];
}
