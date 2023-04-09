using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public  CardValueManager cardvaluemanager; //卡片目前資料
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //卡片上下半資料

    private bool canUseThisCard; //該卡片是否能用
    public bool isUseThisCard; //是否使用該卡片
    private bool isCardUp; //卡片是否為正位置
    private int[] PlayerZone = new int[] { 0, 1, 2, 3, 4 }; //攻擊範圍

    [Header("CardValue")]
    public int ID; //卡片ID
    public bool candraw; //用完是否可抽牌
    public int[] CanAttack = new int[5]; //可攻擊位置
    public int Type; //種類
    public string Name; //名字
    public int Value; //數值

    private void OnEnable()
    {
        isCardUp = true;
        isUseThisCard = false;
        canUseThisCard = false;
    }
    // Update is called once per frame
    void Update()
    {
        //卡片正位置
        if (isCardUp == true)
        {
            cardvaluemanager = _cardValueManager[0];
        }

        //卡片逆位置
        if (isCardUp == false)
        {
            cardvaluemanager = _cardValueManager[1];
        }
        //卡片資料
        ID = cardvaluemanager.cardValue.ID;
        candraw = cardvaluemanager.cardValue.candraw;
        CanAttack = cardvaluemanager.cardValue.CanAttack;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;

        //卡牌使用判定
        if (GameManager.Playerturn == true)
        {
            //移動牌使用判定
            if (ID == 0)
            {
                if (GameManager.duelStateMode == GameState.DuelStateMode.MoveState)
                {
                    canUseThisCard = true;
                }
                else
                {
                    canUseThisCard = false;
                }
            }

            //攻擊牌判定
            if (ID == 1)
            {
                if (GameManager.duelStateMode == GameState.DuelStateMode.MainState)
                {
                    canUseThisCard = true;
                }
                else
                {
                    canUseThisCard = false;
                }
            }

            //星星/回血牌判定
            if (ID == 2 || ID == 3)
            {
                canUseThisCard = true;
            }
        }
        else
        {
            canUseThisCard = false;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //跳出大卡圖及效果文UI

        //滑鼠左鍵卡片時
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (canUseThisCard)
            {
                isUseThisCard = !isUseThisCard;
                UseCard();
            }
        }

        //滑鼠右鍵卡片時
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
                isCardUp = !isCardUp;
        }
    }
    private void UseCard() //點擊卡片時卡片向上移
    {
        if (isUseThisCard)
        {
            transform.position += new Vector3(0,0,10);
            if (ID == 1)
            {
                HandCards.TypeValue[Type + 1, 0] += Value;
            }
            else if (ID == 0)
            {
                HandCards.TypeValue[ID, 0] += Value;
            }
            else
            {
                HandCards.TypeValue[ID + 1, 0] += Value;
            }
        }
        else
        {
            transform.position -= new Vector3(0, 0,10);
            if (ID == 1)
            {
                HandCards.TypeValue[Type + 1, 0] -= Value;
            }
            else if (ID == 0)
            {
                HandCards.TypeValue[ID, 0] -= Value;
            }
            else
            {
                HandCards.TypeValue[ID+1, 0] -= Value;
            }
        }
    }
}
