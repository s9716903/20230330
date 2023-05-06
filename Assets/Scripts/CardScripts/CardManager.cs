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
    public bool isCardUp; //�d���O�_������m(�P�_�ΤW�b�٬O�U�b�ĪG)
    private bool canUseThisCard; //�ӥd���O�_���(�P�_���ɯ�_�ϥ�(�D���)) 
    private bool canChangeUpOrDown; 

    //CardState(UsingState)
    public bool isUseThisCard; //�O�_�ϥθӥd��(�P�_�O�_�Q�ϥ�)(�ھڥd�������i�ɵP)
    public bool isDropThisCard; //�O�_���ӥd��(�P�_�O�_�Q���)(�i�ɵP)
    public bool DamagedDropCard; //�O�_�]���˱˱�ӥd��(�P�_�O�_���˥��)(���i�ɵP)

    //CardStateTrue
    public bool isPlayerUse; //�P�_�O�_��P���a�ƹ�����


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
        //�d�����f��m���
        if (isCardUp == true)
        {
            cardvaluemanager = _cardValueManager[0];
        }
        else
        {
            cardvaluemanager = _cardValueManager[1];
        }

        //�d�����
        ID = cardvaluemanager.cardValue.ID;
        candraw = cardvaluemanager.cardValue.candraw;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;
        AttackZone = cardvaluemanager.cardValue.AttackZone;

        //�d�P�ϥΧP�w
        if (DuelStateManager.canInterect) //�i�i��ʧ@
        {
            if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing) //���ƶ��q
            {
                //���ʵP�ϥΧP�w
                if (ID == 0)
                {
                    if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
                    {
                        canUseThisCard = true; //�i�ϥ�
                    }
                    else
                    {
                        canUseThisCard = false;
                    }
                }

                //�����P�P�w
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

                //�P�P/�^��P�P�w
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
            //���X�j�d�ϤήĪG��UI
            if (gameObject.GetComponent<CardTurnOver>().cardState == CardState.Top)
            {
                InformationUI.readCardInformation = true;
            }

            if (isPlayerUse)
            {
                if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
                {
                    //�ƹ�����d����
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
                    if (pointerEventData.button == PointerEventData.InputButton.Right && canChangeUpOrDown) //�ƹ��k��d���ɥd��½��+�󴫸��
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
    private void UseCard() //�d���Q�ϥή�
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

    public void DropCard() //�d���Q����
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

    public void DamageDropCard() //�d���]����Q�˱��
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
