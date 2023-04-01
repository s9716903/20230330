using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    //序列化CardValue資料並轉成ScriptableObject
    public CardValue cardvalue; 
}
[System.Serializable]
public class CardValue
{
    //卡片上半資料
    public bool candraw; //用完是否可抽牌
    public int Type; //種類
    public string Name; //名字
    public int Value; //數值
    public int[] CanAttack = new int[5]; //可攻擊位置

    //卡片下半資料
    public bool candraw2;
    public int Type2;
    public string Name2;
    public int Value2;
    public int[] CanAttack2 = new int[5];
}
