using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    public CardValue cardvalue; //�ǦC��CardValue��ƨ��নScriptableObject
}
[System.Serializable]
public class CardValue
{
    public bool candraw; //�Χ��O�_�i��P(�W�b)
    public int Type; //����(�W�b)
    public string Name; //�W�r(�W�b)
    public int Value; //�ƭ�(�W�b)
    public bool[] AttackZone;
    public bool candraw2; //�Χ��O�_�i��P(�U�b)
    public int Type2; //����(�U�b)
    public string Name2; //�W�r(�U�b)
    public int Value2; //�ƭ�(�U�b)
    public bool[] AttackZone2;
}
