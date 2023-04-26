using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public CardValueManager cardvaluemanager; //currentCardValue
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //CardValue(Up and Down)

    //CardState
    private bool isCardUp; //�d���O�_������m(�P�_�ΤW�b�٬O�U�b�ĪG)
    private bool canUseThisCard; //�ӥd���O�_���(�P�_���ɯ�_�ϥ�(�D���)) 

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
        
        isUseThisCard = false;
        isDropThisCard = false;
        DamagedDropCard = false;
        isPlayerUse = false;
    }
    // Update is called once per frame
    void Update()
    {
        //�d������m���
        if (isCardUp == true)
        {
            cardvaluemanager = _cardValueManager[0];
        }

        //�d���f��m���
        if (isCardUp == false)
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

            if (isPlayerUse)
            {
                if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
                {
                    //�ƹ�����d����
                    if (pointerEventData.button == PointerEventData.InputButton.Left)
                    {
                        if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
                        {
                            DamagedDropCard = !DamagedDropCard;
                            DamageDropCard();
                        }
                        if (!canUseThisCard)
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

                    //�ƹ��k��d����
                    if (pointerEventData.button == PointerEventData.InputButton.Right)
                    {
                        isCardUp = !isCardUp;
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
        }
        else
        {
            transform.position -= new Vector3(0, 0,10);
        }
    }

    public void DropCard() //�d���Q����
    {
        if (isDropThisCard)
        {
            transform.position += new Vector3(0, 0, 10);
        }
        else
        {
            transform.position -= new Vector3(0, 0, 10);
        }
    }

    public void DamageDropCard() //�d���]����Q�˱��
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
