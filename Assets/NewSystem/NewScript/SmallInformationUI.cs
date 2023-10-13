using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmallInformationUI : MonoBehaviour
{
    [Header("InformationType")]
    public GameObject CardInformation;

    [Header("CardInformationPart")]
    public Image CardIcon1;
    public Image CardIcon2;
    public Sprite[] Icons;
    public TextMeshProUGUI CardUpText;
    public TextMeshProUGUI CardDownText;

    public static int CardType1;
    public static int CardType2;

    public static bool CardUp;
    public static bool readInformation;

    public static Vector3 UIPos;
    // Start is called before the first frame update
    void Start()
    {
        CardUp = true;
        readInformation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (readInformation)
        {
            CardInformation.transform.position = UIPos;
            CardInformation.SetActive(true);
        }
        if (!readInformation)
        {
            CardInformation.SetActive(false);
        }
        if (PlayerUIManager.GetInstance().PlayerData.playerStateMode == NewGameState.NewPlayerStateMode.PlayerDeactivate)
        {
            readInformation = false;
        }
        Informations();
    }
    public void Informations()
    {
        CardIcon1.sprite = Icons[CardType1];
        CardIcon2.sprite = Icons[CardType2];
        if (CardUp)
        {
            CardUpText.color = new Color32(255, 255, 255, 255);
            CardDownText.color = new Color32(100, 100, 100, 255);
        }
        else
        {
            CardUpText.color = new Color32(100, 100, 100, 255);
            CardDownText.color = new Color32(255, 255, 255, 255);
        }
    }
}
