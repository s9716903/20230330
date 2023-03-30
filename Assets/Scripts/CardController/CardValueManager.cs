using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardValueManager : ScriptableObject
{
    public CardValue cardvalue; //序列化CardValue資料並轉成ScriptableObject
}
[System.Serializable]
public class CardValue
{
    public bool candraw; //用完是否可抽牌(上半)
    public int Type; //種類(上半)
    public string Name; //名字(上半)
    public int Value; //數值(上半)
    public bool candraw2; //用完是否可抽牌(下半)
    public int Type2; //種類(下半)
    public string Name2; //名字(下半)
    public int Value2; //數值(下半)
}
