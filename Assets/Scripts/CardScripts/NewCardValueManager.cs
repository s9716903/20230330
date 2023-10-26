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
    public Image InteractableImage;

    [Header("Card")]
    public Sprite[] Icon;
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    [Header("MainValue")]
    public int MainType;

    [Header("CardValue")]
    private int Type1;
    private int Type2;
    private int[] weight = new int[3] { 40, 40, 20 }; //�v���U���v

    [Header("CardState")]
    public bool isCardUp; //�d���O�_����m
    public bool usecard; //��ܨϥθӥd��
    public bool resultCard;
    public bool changeCardUpOrDown;
    public PlayerDataManager playerData;
    // Start is called before the first frame update
    void Start()
    {
        InteractableImage.enabled = false;
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
        if ((DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move) || (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.MoveResult))
        {
            InteractableImage.enabled = true;
        }
        else
        {
            InteractableImage.enabled = false;
        }
    }
    private int GetTypeWeight(int[] array, int Totalweight) //���v�v��
    {
        int rand = Random.Range(1, Totalweight + 1);
        int tmp = 0;
        for (int w = 0; w < array.Length; w++)
        {
            tmp += array[w];
            if (rand < tmp)
            {
                return w;
            }
        }
        return 0;
    }

    private void CardValueSetting()
    {
        Type1 = GetTypeWeight(weight, 100);
        Type2 = GetTypeWeight(weight, 100);
        Type1Image.sprite = Icon[Type1];
        Type2Image.sprite = Icon[Type2];
        ChangeCardValue();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if ((playerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate) && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack))
        {
            //�ƹ�����d����
            if (pointerEventData.button == PointerEventData.InputButton.Left && !NewCardTurnTopOrBottom.istheChangeUpOrDown)
            {
                //audioSource.clip = audioClip[0];
                //audioSource.Play();
                ChooseUseCard();
            }
            if (pointerEventData.button == PointerEventData.InputButton.Right && changeCardUpOrDown) //�ƹ��k��d���ɥd��½��+�󴫸��
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
        //���X�ĪGUI
        if ((playerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate) && (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack))
        {
            ShowATKZone();
            StartCoroutine(OpenCardInformation());
            transform.DOScale(1.3f, 0.2f);
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //�����ĪGUI
        SmallInformationUI.readCardInformation = false;
        LocationManager.showPlayerATKZone = false;
        transform.DOScale(1, 0.2f);
    }
    public void ChooseUseCard()
    {
        usecard = !usecard;
        UseCard();
    }
    private void UseCard() //�d���Q�ϥή�
    {
        if (usecard)
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 510, 0);
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
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 460, 0);
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
        LocationManager.ActionType = gameObject.GetComponent<NewCardValueManager>().MainType;
        if (MainType == 1)
        {
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
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 510, 0);
        }
        else
        {
            SmallInformationUI.UIPos = new Vector3(transform.position.x, 460, 0);
        }
        SmallInformationUI.readCardInformation = true;
        yield return null;
    }
}
