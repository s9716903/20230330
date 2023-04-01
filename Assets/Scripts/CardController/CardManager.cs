using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public  CardValueManager cardvaluemanager; //卡片資料

    private bool isUseThisCard;
    private bool isCardUp; //卡片是否為正位置
    private int[] PlayerZone = new int[] { 0, 1, 2, 3, 4 }; //攻擊範圍
   
    public bool candraw; //用完是否可抽牌
    public int[] CanAttack = new int[5]; //可攻擊位置

    [Header("CardValue")]
    public int Type; //種類
    public string Name; //名字
    public int Value; //數值

    private void OnEnable()
    {
        isCardUp = true;
        isUseThisCard = false;
    }
    // Update is called once per frame
    void Update()
    {
        //卡片正位置
        if (isCardUp == true)
        {
            candraw = cardvaluemanager.cardvalue.candraw;
            CanAttack = cardvaluemanager.cardvalue.CanAttack;
            Type = cardvaluemanager.cardvalue.Type;
            Name = cardvaluemanager.cardvalue.Name;
            Value = cardvaluemanager.cardvalue.Value;
        }

        //卡片逆位置
        if (isCardUp == false)
        {
            candraw = cardvaluemanager.cardvalue.candraw2;
            CanAttack = cardvaluemanager.cardvalue.CanAttack2;
            Type = cardvaluemanager.cardvalue.Type2;
            Name = cardvaluemanager.cardvalue.Name2;
            Value = cardvaluemanager.cardvalue.Value2;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //滑鼠左鍵卡片時
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            isUseThisCard = !isUseThisCard;
            UseCard();
        }

        //滑鼠右鍵卡片時
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            isCardUp = !isCardUp;
        }
    }

    private void UseCard() //點擊卡片時卡片向上移(暫時測試用)
    {
        if (isUseThisCard)
        {
            transform.position += new Vector3(0,1,0);
        }
        else
        {
            transform.position -= new Vector3(0, 1, 0);
        }
    }
}
