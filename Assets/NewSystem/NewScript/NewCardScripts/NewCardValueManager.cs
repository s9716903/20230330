using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class NewCardValueManager : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{ 
    [Header("CardPart")]
    public GameObject CardTop;
    public GameObject CardBottom;
    public GameObject CardResult;
    public Image Type1Image;
    public Image Type2Image;
    public Image ResultImage;

    [Header("Card")]
    public Sprite[] Icon;
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    [Header("MainValue")]
    public int MainType;

    [Header("CardValue")]
    private int Type1;
    private int Type2;

    [Header("CardState")]
    public bool isCardUp; //卡片是否正位置
    public bool usecard; //選擇使用該卡片
    public bool resultCard;
    public bool changeCardUpOrDown;
    public PlayerDataManager playerData;
    // Start is called before the first frame update
    void Start()
    {
        CardResult.SetActive(false);
        usecard = false;
        isCardUp = true;
        resultCard = false;
        changeCardUpOrDown = true;
        audioSource = GetComponent<AudioSource>();
        CardValueSetting();
    }

    // Update is called once per frame
    void Update()
    {
        playerData = PlayerUIManager.GetInstance().PlayerData;
        if (resultCard)
        {
            ResultImage.sprite = Icon[MainType];
            CardResult.SetActive(true);
            CardTop.SetActive(false);
            CardBottom.SetActive(false);
        }
    }
    private void CardValueSetting()
    {
        Type1 = Random.Range(0,3);
        Type2 = Random.Range(0,3);
        Type1Image.sprite = Icon[Type1];
        Type2Image.sprite = Icon[Type2];
        ChangeCardValue();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if ((playerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate) && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack))
        {
            //滑鼠左鍵卡片時
            if (pointerEventData.button == PointerEventData.InputButton.Left && !NewCardTurnTopOrBottom.istheChangeUpOrDown)
            {
                //audioSource.clip = audioClip[0];
                //audioSource.Play();
                ChooseUseCard();
            }
            if (pointerEventData.button == PointerEventData.InputButton.Right && changeCardUpOrDown) //滑鼠右鍵卡片時卡片翻轉+更換資料
            {
                //audioSource.clip = audioClip[1];
                //audioSource.Play();
                if (isCardUp == true)
                {
                    isCardUp = false;
                    ChangeCardValue();
                    ShowATKZone();
                    gameObject.GetComponent<NewCardTurnTopOrBottom>().CardStartDown();

                }
                else
                {
                    isCardUp = true;
                    ChangeCardValue();
                    ShowATKZone();
                    gameObject.GetComponent<NewCardTurnTopOrBottom>().CardStartUp();
                }
            }
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //跳出效果UI
        if ((playerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate) && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack))
        {
            ShowATKZone();
            StartCoroutine(OpenCardInformation());
            transform.DOScale(1.3f, 0.2f);
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //關閉效果UI
        SmallInformationUI.readCardInformation = false;
        LocationManager.showPlayerATKZone = false;
        transform.DOScale(1, 0.2f);
    }
    public void ChooseUseCard()
    {
        usecard = !usecard;
        UseCard();
    }
    private void UseCard() //卡片被使用時
    {
        if (usecard)
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 390, 0);
            transform.position += new Vector3(0, 50, 0);
            changeCardUpOrDown = false;
            //playerData.LimitedUse += 1;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited += 1;
            }*/
        }
        else
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 340, 0);
            transform.position -= new Vector3(0, 50, 0);
            changeCardUpOrDown = true;
            //playerData.LimitedUse -= 1;
            /*if (PracticeLimtedSetting.LimitedOn)
            {
                ReadyButton.PracticeLimited -= 1;
            }*/
        }
    }
    public void ShowATKZone()
    {
        if (MainType == 1 || MainType == 2)
        {
            LocationManager.ATKType = gameObject.GetComponent<NewCardValueManager>().MainType;
            LocationManager.showPlayerATKZone = true;
        }
        else
        {
            LocationManager.showPlayerATKZone = false;
        }
    }
    public void ChangeCardValue()
    {
        if (isCardUp)
        {
            MainType = Type1;
        }
        else
        {
            MainType = Type2;
        }
    }
    public IEnumerator OpenCardInformation()
    {
        SmallInformationUI.readCharacterInformation = false;
        SmallInformationUI.CardType1 = Type1;
        SmallInformationUI.CardType2 = Type2;
        SmallInformationUI.CardUp = isCardUp;
        if (usecard)
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 390, 0);
        }
        else
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 340, 0);
        }
        SmallInformationUI.readCardInformation = true;
        yield return null;
    }
}
