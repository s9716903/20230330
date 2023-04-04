using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    //序列化CardValue資料並轉成ScriptableObject
    public CardValue cardValue;
}
[System.Serializable]
public class CardValue //基礎卡片資料
{
    //卡片資料
    public int ID; //卡片ID
    public bool candraw; //用完是否可抽牌
    public string Name; //名字
    public int Value; //數值
    public int Type; //攻擊種類
    public int[] CanAttack = new int[5]; //可攻擊位置
}
