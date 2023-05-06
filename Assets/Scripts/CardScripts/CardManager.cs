using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public CardValueManager cardvaluemanager; //currentCardValue
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //CardValue(Up and Down)

    //CardState
    public bool isCardUp; //卡片是否為正位置(判斷用上半還是下半效果)
    private bool canUseThisCard; //該卡片是否能用(判斷此時能否使用(非丟棄)) 
    private bool canChangeUpOrDown; 

    //CardState(UsingState)
    public bool isUseThisCard; //是否使用該卡片(判斷是否被使用)(根據卡片種類可補牌)
    public bool isDropThisCard; //是否丟棄該卡片(判斷是否被丟棄)(可補牌)
    public bool DamagedDropCard; //是否因受傷捨棄該卡片(判斷是否受傷丟棄)(不可補牌)

    //CardStateTrue
    public bool isPlayerUse; //判斷是否能與玩家滑鼠互動


    [Header("CardValue")]
    public int ID; 
    public bool candraw; 
    public int Type; 
    public string Name; 
    public int Value; 
    public int[] AttackZone; 

    private void OnEnable()
    {     
        isCardUp = true;
        canUseThisCard = false;
        canChangeUpOrDown = true;

        isUseThisCard = false;
        isDropThisCard = false;
        DamagedDropCard = false;
        isPlayerUse = false;
    }
    // Update is called once per frame
    void Update()
    {
        //卡片正逆位置資料
        if (isCardUp == true)
        {
            cardvaluemanager = _cardValueManager[0];
        }
        else
        {
            cardvaluemanager = _cardValueManager[1];
        }

        //卡片資料
        ID = cardvaluemanager.cardValue.ID;
        candraw = cardvaluemanager.cardValue.candraw;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;
        AttackZone = cardvaluemanager.cardValue.AttackZone;

        //卡牌使用判定
        if (DuelStateManager.canInterect) //可進行動作
        {
            if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing) //做事階段
            {
                //移動牌使用判定
                if (ID == 0)
                {
                    if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
                    {
                        canUseThisCard = true; //可使用
                    }
                    else
                    {
                        canUseThisCard = false;
                    }
                }

                //攻擊牌判定
                if (ID == 1)
                {
                    if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
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
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (DuelStateManager.canInterect)
        {
            //跳出大卡圖及效果文UI
            if (gameObject.GetComponent<CardTurnOver>().cardState == CardState.Top)
            {
                InformationUI.readCardInformation = true;
            }

            if (isPlayerUse)
            {
                if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
                {
                    //滑鼠左鍵卡片時
                    if (pointerEventData.button == PointerEventData.InputButton.Left && !CardTurnOver.isChangeUpOrDown)
                    {
                        if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
                        {
                            DamagedDropCard = !DamagedDropCard;
                            DamageDropCard();
                        }
                        else
                        {
                            if (!canUseThisCard)
                            {
                                isDropThisCard = !isDropThisCard;
                                DropCard();
                            }
                            else
                            {
                                isUseThisCard = !isUseThisCard;
                                UseCard();
                            }
                        }
                    }
                    if (pointerEventData.button == PointerEventData.InputButton.Right && canChangeUpOrDown) //滑鼠右鍵卡片時卡片翻轉+更換資料
                    {
                        if (isCardUp == true)
                        {
                            gameObject.GetComponent<CardTurnOver>().CardStartDown();
                        }
                        else
                        {
                            gameObject.GetComponent<CardTurnOver>().CardStartUp();
                        }
                    }
                }
            }
        }
    }
    private void UseCard() //卡片被使用時
    {
        if (isUseThisCard)
        {
            transform.position += new Vector3(0,0,10);
            canChangeUpOrDown = false;
        }
        else
        {
            transform.position -= new Vector3(0, 0,10);
            canChangeUpOrDown = true;
        }
    }

    public void DropCard() //卡片被丟棄時
    {
        if (isDropThisCard)
        {
            transform.position += new Vector3(0, 0, 10);
            canChangeUpOrDown = false;
        }
        else
        {
            transform.position -= new Vector3(0, 0, 10);
            canChangeUpOrDown = true;
        }
    }

    public void DamageDropCard() //卡片因扣血被捨棄時
    {
        if (isDropThisCard)
        {
            transform.position += new Vector3(0, 0, 10);
            canChangeUpOrDown = false;
            //GameManager.Damaged++;
        }
        else
        {
            transform.position -= new Vector3(0, 0, 10);
            canChangeUpOrDown = true;
            //GameManager.Damaged--;
        }
    }
}
