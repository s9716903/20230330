using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public  CardValueManager cardvaluemanager; //卡片目前資料
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //卡片上下半資料

    private bool canUseThisCard; //該卡片是否能用(判斷此時能否用)
    public bool isUseThisCard; //是否使用該卡片(判斷是否被使用)
    public bool isDropThisCard; //是否丟棄該卡片(判斷是否被丟棄)
    private bool isCardUp; //卡片是否為正位置(判斷用上半還是下半效果)


    [Header("CardValue")]
    public int ID; //卡片ID
    public bool candraw; //用完是否可抽牌
    public int Type; //種類
    public string Name; //名字
    public int Value; //數值
    public int[] AttackZone; //攻擊範圍

    private void OnEnable()
    {
        canUseThisCard = false;
        isUseThisCard = false;
        isDropThisCard = false;  
        isCardUp = true;
    }
    // Update is called once per frame
    void Update()
    {
        //卡片正位置資料
        if (isCardUp == true)
        {
            cardvaluemanager = _cardValueManager[0];
        }

        //卡片逆位置資料
        if (isCardUp == false)
        {
            cardvaluemanager = _cardValueManager[1];
        }

        //卡片資料
        ID = cardvaluemanager.cardValue.ID;
        candraw = cardvaluemanager.cardValue.candraw;
        AttackZone = cardvaluemanager.cardValue.AttackZone;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;

        //卡牌使用判定
        if (GameManager.canInterect) //可進行動作
        {
            isDropThisCard = true; //可棄牌
            if (GameManager.playerStateType == GameState.PlayerStateMode.DoThing) //做事階段
            {
                //移動牌使用判定
                if (ID == 0)
                {
                    if (GameManager.duelStateType == GameState.DuelStateMode.Move)
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
                    if (GameManager.duelStateType == GameState.DuelStateMode.Attack)
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
        }
        else
        {
            isDropThisCard = false; //不可棄牌
            canUseThisCard = false; //不可使用牌
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //跳出大卡圖及效果文UI

        //滑鼠左鍵卡片時
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (GameManager.playerStateType == GameState.PlayerStateMode.Damage)
            {
                isDropThisCard = !isDropThisCard;
                DropCard();
            }
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
    private void UseCard() //卡片使用時卡片向上移
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
    public void DropCard() //卡片扣血選擇丟棄時往上移
    {
        if (isDropThisCard)
        {
            transform.position += new Vector3(0, 0, 10);
            //GameManager.Damaged++;
        }
        else
        {
            transform.position -= new Vector3(0, 0, 10);
            //GameManager.Damaged--;
        }
    }
}
