using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmallInformationUI : MonoBehaviour
{
    [Header("InformationType")]
    public GameObject CardInformation;
    public GameObject CharacterInformation;

    [Header("CardInformationPart")]
    public Image CardIcon1;
    public Image CardIcon2;
    public Sprite[] Icons;
    public TextMeshProUGUI CardUpText;
    public TextMeshProUGUI CardDownText;
    public static int CardType1;
    public static int CardType2;
    public static bool CardUp;

    [Header("CharacterInformationPart")]
    public GameObject SkillImage;
    public GameObject SkillLists;
    public TextMeshProUGUI PhyATKText;
    public TextMeshProUGUI MagicATKText;
    public TextMeshProUGUI Skill;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI DefenseText;
    public TextMeshProUGUI MoveText;
    public static string TextPATK;
    public static string TextMATK;
    public static string TextHP;
    public static string TextDefense;
    public static string TextMove;

    public static Vector3 UIPos;
    public static bool readCardInformation;
    public static bool readCharacterInformation;
    // Start is called before the first frame update
    void Start()
    {
        CardInformation.SetActive(false);
        CharacterInformation.SetActive(false);
        CardUp = true;
        readCardInformation = false;
        readCharacterInformation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (readCardInformation)
        {
            CardInformation.transform.position = UIPos;
            CardInformations();
            CharacterInformation.SetActive(false);
            CardInformation.SetActive(true);
        }
        if(!readCardInformation)
        {
            CardInformation.SetActive(false);
        }
        if (readCharacterInformation)
        {
            CharacterInformation.transform.position = UIPos;
            CharacterInformations();
            CardInformation.SetActive(false);
            CharacterInformation.SetActive(true);
        }
        if (!readCharacterInformation)
        {
            CharacterInformation.SetActive(false);
        }

        if (PlayerUIManager.GetInstance().PlayerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerDeactivate)
        {
            readCardInformation = false;
            readCharacterInformation = false;
        }

    }
    public void CardInformations()
    {
        var playerdata = PlayerUIManager.GetInstance().PlayerData;
        var typevalue = new int[] {playerdata.Defense,playerdata.PhysicATK, playerdata.MagicATK};
        var typebuff = playerdata.BuffValue;
        
        CardIcon1.sprite = Icons[CardType1];
        CardIcon2.sprite = Icons[CardType2];
        CardUpText.text = (typevalue[CardType1] + typebuff[CardType1]).ToString();
        CardDownText.text = (typevalue[CardType2] + typebuff[CardType2]).ToString();
        if (CardUp)
        {
            if ((typevalue[CardType1] + typebuff[CardType1]) > typevalue[CardType1])
            {
                CardUpText.color = new Color32(0, 255, 0, 255);
            }
            else if ((typevalue[CardType1] + typebuff[CardType1]) < typevalue[CardType1])
            {
                CardUpText.color = new Color32(255, 0, 0, 255);
            }
            else
            {
                CardUpText.color = new Color32(255, 255, 255, 255);
            }
            CardDownText.color = new Color32(100, 100, 100, 255);
        }
        else
        {
            CardUpText.color = new Color32(100, 100, 100, 255);
            if ((typevalue[CardType2] + typebuff[CardType2]) > typevalue[CardType2])
            {
                CardDownText.color = new Color32(0, 255, 0, 255);
            }
            else if ((typevalue[CardType2] + typebuff[CardType2]) < typevalue[CardType2])
            {
                CardDownText.color = new Color32(255, 0, 0, 255);
            }
            else
            {
                CardDownText.color = new Color32(255, 255, 255, 255);
            }
        }
    }
    public void CharacterInformations()
    {
        PhyATKText.text = TextPATK;
        MagicATKText.text = TextMATK;
        HPText.text = TextHP;
        DefenseText.text = TextDefense;
        MoveText.text = TextMove;
    }
}
