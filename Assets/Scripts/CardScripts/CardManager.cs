using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public  CardValueManager cardvaluemanager; //�d���ثe���
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //�d���W�U�b���

    //�d�����A��(�d���ۨ�����}��)
    public bool isUseThisCard; //�O�_�ϥθӥd��(�P�_�O�_�Q�ϥ�)(�ھڥd�������i�ɵP)
    public bool isDropThisCard; //�O�_���ӥd��(�P�_�O�_�Q���)(�i�ɵP)
    public bool DamagedDropCard; //�O�_�]���˱˱�ӥd��(�P�_�O�_���˥��)(���i�ɵP)
    private bool canUseThisCard; //�ӥd���O�_���(�P�_���ɯ�_�ϥ�(�D���)) 
    private bool isCardUp; //�d���O�_������m(�P�_�ΤW�b�٬O�U�b�ĪG)

    //�d�����A��(�Ѩ�L�}������}��)
    public bool isCardStateTrue; //�P�_�O�_�ƹ���P������


    [Header("CardValue")]
    public int ID; //�d��ID
    public bool candraw; //�Χ��O�_�i��P
    public int Type; //����
    public string Name; //�W�r
    public int Value; //�ƭ�
    public int[] AttackZone; //�����d��

    private void OnEnable()
    {
        isCardStateTrue = true;
        canUseThisCard = false;
        isUseThisCard = false;
        isDropThisCard = false;
        DamagedDropCard = false;
        isCardUp = true;
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
        AttackZone = cardvaluemanager.cardValue.AttackZone;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;

        //�d�P�ϥΧP�w
        if (DuelStateManager.canInterect && isCardStateTrue) //�i�i��ʧ@
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
        //���X�j�d�ϤήĪG��UI

        if (DuelStateManager.canInterect && isCardStateTrue)
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
    private void UseCard() //�d���ϥήɥd���V�W��
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
    public void DropCard() //�d����ܥ��ɩ��W��
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
    public void DamageDropCard() //�]�����ܱ˱�d���ɩ��W��
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
