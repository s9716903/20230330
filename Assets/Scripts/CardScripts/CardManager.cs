using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour,IPointerClickHandler
{
    public CardValueManager cardvaluemanager; //currentCardValue
    public CardValueManager[] _cardValueManager = new CardValueManager[2]; //CardValue(Up and Down)

    //CardState
    public bool isCardUp; //�d���O�_������m(�P�_�ΤW�b�٬O�U�b�ĪG)
    public bool canUseThisCard; //�ӥd���O�_���(�P�_���ɯ�_�ϥ�(�D���)) 
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

    /*[Header("CardIcon")]
    public GameObject Icon1Value;
    public GameObject Icon2Value;
    public List<Sprite> IconList;*/
    public GameObject Icon1Sprite;
    public GameObject Icon2Sprite;

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
    private void Start()
    {
        /*if (_cardValueManager[0].cardValue.ID == 0)
        {
            Icon1Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[0].cardValue.ID];
            Icon1Value.GetComponent<TextMeshPro>().text = _cardValueManager[0].cardValue.Value.ToString();
        }
        if (_cardValueManager[1].cardValue.ID == 0)
        {
            Icon2Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[1].cardValue.ID];
            Icon2Value.GetComponent<TextMeshPro>().text = _cardValueManager[1].cardValue.Value.ToString();
        }
        if (_cardValueManager[0].cardValue.ID == 1)
        {
            Icon1Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[0].cardValue.Type + 1];
            Icon1Value.GetComponent<TextMeshPro>().text = _cardValueManager[0].cardValue.Value.ToString();
        }
        if (_cardValueManager[1].cardValue.ID == 1)
        {
            Icon2Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[1].cardValue.Type + 1];
            Icon2Value.GetComponent<TextMeshPro>().text = _cardValueManager[1].cardValue.Value.ToString();
        }
        if (_cardValueManager[0].cardValue.ID >= 2)
        {
            Icon1Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[0].cardValue.ID + 1];
            Icon1Value.GetComponent<TextMeshPro>().text = _cardValueManager[0].cardValue.Value.ToString();
        }
        if (_cardValueManager[1].cardValue.ID >= 2)
        {
            Icon2Sprite.GetComponent<SpriteRenderer>().sprite = IconList[_cardValueManager[1].cardValue.ID + 1];
            Icon2Value.GetComponent<TextMeshPro>().text = _cardValueManager[1].cardValue.Value.ToString();
        }*/
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
            if (gameObject.GetComponent<CardTurnOver>().cardState == CardState.Top && pointerEventData.button == PointerEventData.InputButton.Left)
            {
                StartCoroutine(ReadCardInformation());
            }

            if (isPlayerUse)
            {
                if (DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
                {
                    //�ƹ�����d����
                    if (pointerEventData.button == PointerEventData.InputButton.Left && !CardTurnOver.isChangeUpOrDown)
                    {
                        ChooseUseCard();
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
                else if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
                {
                    //�ƹ�����d����
                    if (pointerEventData.button == PointerEventData.InputButton.Left)
                    {
                        ChooseDamageDropCard();
                    }
                }
            }
        }
    }
    public void ChooseUseCard()
    {
        if (!canUseThisCard)
        {
            isDropThisCard = !isDropThisCard;
            if (isPlayerUse)
            {
                DropCard();
            }
        }
        else
        {
            isUseThisCard = !isUseThisCard;
            if (isPlayerUse)
            {
                UseCard();
            }
        }
    }

    public void ChooseDamageDropCard()
    {
        DamagedDropCard = !DamagedDropCard;
        if (isPlayerUse)
        {
            DamageDropCard();
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
        if (DamagedDropCard)
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
    public IEnumerator ReadCardInformation()
    {
        var CardInformation = GameObject.Find("InformationUI").GetComponent<InformationUI>();
        CardInformation.CardIconUp.sprite = Icon1Sprite.GetComponent<SpriteRenderer>().sprite;
        CardInformation.CardIconDown.sprite = Icon2Sprite.GetComponent<SpriteRenderer>().sprite;
        CardInformation.CardValueUp.text = ":" + _cardValueManager[0].cardValue.Value.ToString();
        CardInformation.CardValueDown.text = ":" + _cardValueManager[1].cardValue.Value.ToString();
        InformationUI.readCardInformation = true;
        yield return 0;
    }
}
