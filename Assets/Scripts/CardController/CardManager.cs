using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardValueManager cardvaluemanager; //卡片資料
    private bool isCardUp;
    public bool candraw; //用完是否可抽牌
    public int Type; //種類
    public string Name; //名字
    public int Value; //數值
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
