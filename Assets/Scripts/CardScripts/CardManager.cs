using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public  CardValueManager cardvaluemanager; //�d���ثe���
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //�d���W�U�b���

    private bool canUseThisCard; //�ӥd���O�_���
    public bool isUseThisCard; //�O�_�ϥθӥd��
    public bool isDropThisCard; //�O�_���ӥd��
    private bool isCardUp; //�d���O�_������m
    private GameManager manager;

    private int[] PlayerZone = new int[] { 0, 1, 2, 3, 4 }; //�����d��

    [Header("CardValue")]
    public int ID; //�d��ID
    public bool candraw; //�Χ��O�_�i��P
    public int[] CanAttack = new int[5]; //�i������m
    public int Type; //����
    public string Name; //�W�r
    public int Value; //�ƭ�

    public CardManager(GameManager manager)
    {
        this.manager = manager;
    }

    private void OnEnable()
    {
        isDropThisCard = false;
        isCardUp = true; 
        isUseThisCard = false; 
        canUseThisCard = false; 
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
        CanAttack = cardvaluemanager.cardValue.CanAttack;
        Type = cardvaluemanager.cardValue.Type;
        Name = cardvaluemanager.cardValue.Name;
        Value = cardvaluemanager.cardValue.Value;

        //�d�P�ϥΧP�w
        if (GameManager.canInterect)
        {
            //���ʵP�ϥΧP�w
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

            //�����P�P�w
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

            //�P�P/�^��P�P�w
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
        //���X�j�d�ϤήĪG��UI

        //�ƹ�����d����
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (GameManager.playerStateType == GameState.PlayerStateMode.Damage)
            {
                isDropThisCard = !isDropThisCard;

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
    private void UseCard() //�d���ϥήɥd���V�W��
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
    public void DropCard() //�d�������ܥ��ɩ��W��
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
}
