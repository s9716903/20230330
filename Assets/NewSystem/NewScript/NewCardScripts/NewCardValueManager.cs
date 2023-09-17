using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NewCardValueManager : MonoBehaviour,IPointerDownHandler
{ 
    [Header("CardPart")]
    public GameObject CardTop1;
    public GameObject CardTop2;
    public GameObject CardBottom;
    public Image Type1;
    public TextMeshProUGUI Value1Text;
    public Image Type2;
    public TextMeshProUGUI Value2Text;
    public Image Type;
    public TextMeshProUGUI ValueText;

    public Card card;

    [Header("Card")]
    public Sprite[] Icon;
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    [Header("MainValue")]
    public string ID;
    public int MainType;
    public int MainValue;
    public int[] MainAttackZone;

    [Header("CardValue")]
    public int Type1Value;
    public int Type2Value;
    public int Value1Value;
    public int Value2Value;
    public int[] AttackZoneValue;

    [Header("CardState")]
    public bool cardcanuse; //�O�_�i�H�ϥθӥd
    public bool isCardUp; //�d���O�_����m
    public bool usecard; //��ܨϥθӥd��
    public bool changeCardUpOrDown;
    public bool isPlayer;
    public bool istwincard = true;
    public PlayerDataManager playerData;
    // Start is called before the first frame update
    void Start()
    {
        cardcanuse = false;
        usecard = false;
        isCardUp = true;
        changeCardUpOrDown = true;
        audioSource = GetComponent<AudioSource>();
        CardValueSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            playerData = PlayerUIManager.GetInstance().PlayerData;
        }
        else
        {
            playerData = EnemyUIManager.GetInstance().EnemyData;
        }

        //�d�����f��m���
        if (isCardUp)
        {
            MainType = Type1Value;
            MainValue = Value1Value;
            MainAttackZone = AttackZoneValue;
        }
        else if (!isCardUp)
        {
            MainType = Type2Value;
            MainValue = Value2Value;
        }

        //�d���ϥΧP�w
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
        {
            if (MainType != 1 || MainType != 2)
            {
                cardcanuse = true;
            }
            else
            {
                cardcanuse = false;
            }
        }
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
        {
            if (MainType != 0)
            {
                cardcanuse = true;
            }
            else
            {
                cardcanuse = false;
            }
        }
        else if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.DamageResult)
        {
            cardcanuse = true;
        }
    }
    private void CardValueSetting()
    {
        if (card is TwinCard)
        {
            var twincard = card as TwinCard;
            ID = twincard.ID.ToString();
            Type1Value = twincard.Type1;
            Type2Value = twincard.Type2;
            Value1Value = twincard.Value1;
            Value2Value = twincard.Value2;
            AttackZoneValue = twincard.AttackZone1;
            Type1.sprite = Icon[twincard.Type1];
            Value1Text.text = twincard.Value1.ToString();
            Type2.sprite = Icon[twincard.Type2];
            Value2Text.text = twincard.Value2.ToString();
            CardTop1.SetActive(true);
            CardTop2.SetActive(false);
            istwincard = true;
        }
        else if (card is HealthCard)
        {
            var healthcard = card as HealthCard;
            ID = healthcard.ID.ToString();
            Type1Value = healthcard.Type;
            Type2Value = healthcard.Type;
            Value1Value = healthcard.Value;
            Value2Value = healthcard.Value;
            ValueText.text = healthcard.Value.ToString();
            CardTop1.SetActive(false);
            CardTop2.SetActive(true);
            istwincard = false;
        }
        transform.name = ID;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //���X�j�d�ϤήĪG��UI
        if (gameObject.GetComponent<NewCardTurnTopOrBottom>().cardTopOrBottomState == CardTopOrBottomState.Top && (pointerEventData.button == PointerEventData.InputButton.Left || pointerEventData.button == PointerEventData.InputButton.Right) && playerData.playerStateMode != NewGameState.NewPlayerStateMode.PlayerDeactivate)
        {
            StartCoroutine(ReadCardInformation());
        }
        if (playerData.isPlayer1 && playerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate)
        {
            //�ƹ�����d����
            if (pointerEventData.button == PointerEventData.InputButton.Left && !NewCardTurnTopOrBottom.istheChangeUpOrDown)
            {
                //audioSource.clip = audioClip[0];
                //audioSource.Play();
                if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Damage)
                {
                    ChooseDamageDropCard();
                }
                else
                { 
                    ChooseUseCard();
                }    
            }
            if (pointerEventData.button == PointerEventData.InputButton.Right && changeCardUpOrDown) //�ƹ��k��d���ɥd��½��+�󴫸��
            {
                StartCoroutine(ReadCardInformation());
                //audioSource.clip = audioClip[1];
                //audioSource.Play();
                if (isCardUp == true)
                {
                    gameObject.GetComponent<NewCardTurnTopOrBottom>().CardStartDown();
                }
                else
                {
                    gameObject.GetComponent<NewCardTurnTopOrBottom>().CardStartUp();
                }
            }
        }
    }
    public void ChooseUseCard()
    {
        usecard = !usecard;
        if (playerData.isPlayer1)
        {
            UseCard();
        }
        if (usecard)
        {
            if (!cardcanuse)
            {
                playerData.NormalDrawAmount++;
            }
            if (cardcanuse)
            {
                if (MainType == 0)
                {
                    playerData.MoveValue += MainValue;
                    playerData.NormalDrawAmount++;
                }
                else if (MainType == 1)
                {
                    playerData.PhysicATK += MainValue;
                    playerData.NormalDrawAmount++;
                }
                else if (MainType == 2)
                {
                    playerData.MagicATK += MainValue;
                }
                else if (MainType == 3)
                {
                    playerData.Stars += MainValue;
                }
                else if (MainType == 4)
                {
                    playerData.HealthValue += MainValue;
                }
            }
        }
        if (!usecard)
        {
            if (!cardcanuse)
            {
                playerData.NormalDrawAmount--;
            }
            if (cardcanuse)
            {
                if (MainType == 0)
                {
                    playerData.MoveValue -= MainValue;
                    playerData.NormalDrawAmount--;
                }
                else if (MainType == 1)
                {
                    playerData.PhysicATK -= MainValue;
                    playerData.NormalDrawAmount--;
                }
                else if (MainType == 2)
                {
                    playerData.MagicATK -= MainValue;
                }
                else if (MainType == 3)
                {
                    playerData.Stars -= MainValue;
                }
                else if (MainType == 4)
                {
                    playerData.HealthValue -= MainValue;
                }
            }
        }
    }
   public void ChooseDamageDropCard()
    {
        usecard = !usecard;
        if (playerData.isPlayer1)
        {
            DamageDropCard();
        }
        if (usecard)
        {
            playerData.DamageDropAmount++;
        }
        if (!usecard)
        {
            playerData.DamageDropAmount--;
        }
    }
    private void UseCard() //�d���Q�ϥή�
    {
        if (usecard)
        {
            transform.position += new Vector3(0, 50, 0);
            changeCardUpOrDown = false;
            playerData.LimitedUse += 1;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited += 1;
            }*/
        }
        else
        {
            transform.position -= new Vector3(0, 50, 0);
            changeCardUpOrDown = true;
            playerData.LimitedUse -= 1;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited -= 1;
            }*/
        }
    }
    private void DamageDropCard() //�d���]����Q�˱��
    {
        if (usecard)
        {
            transform.position += new Vector3(0, 50, 0);
            changeCardUpOrDown = false;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited += 1;
            }*/
        }
        else
        {
            transform.position -= new Vector3(0, 50, 0);
            changeCardUpOrDown = true;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited -= 1;
            }*/
        }
    }
    public IEnumerator ReadCardInformation()
    {
        InformationManager.isCardInformation = true;
        if (istwincard)
        {
            InformationManager.Information1Type = Type1Value;
            InformationManager.Information1Value = Value1Value;
            InformationManager.Information2Type = Type2Value;
            InformationManager.Information2Value = Value2Value;
            InformationManager.AttackZone = AttackZoneValue;
        }
        else if (!istwincard)
        {
            InformationManager.Information1Type = Type1Value;
            InformationManager.Information1Value = Value1Value;
            InformationManager.Information2Type = Type2Value;
            InformationManager.Information2Value = Value2Value;
        }
        InformationManager.readInformation = true;
        InformationManager.isopenInformationUI = true;
        yield return null;
        InformationManager.isopenInformationUI = false;
    }
}
