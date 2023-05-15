using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformationUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI CardValueUp;
    public TextMeshProUGUI CardValueDown;
    public Image CardIconUp;
    public Image CardIconDown;
    public static int[] AttackZoneUP;
    public static int[] AttackZoneDown;

    public static int InformationUpID;
    public static int InformationDownID;
    public static int InformationUpAttackType;
    public static int InformationDownAttackType;
    public static string InformationUpName;
    public static string InformationDownName;

    private bool isopenInformationUI;
    public static bool readCardInformation;

    // Start is called before the first frame update
    void Start()
    {
        isopenInformationUI = false;
        readCardInformation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (readCardInformation && gameObject.GetComponent<RectTransform>().anchoredPosition.x != 300)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 0, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isopenInformationUI)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 0, 0);
            }
            else
            {
                readCardInformation = false;
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 0, 0);
            }
        }

        if (!DuelStateManager.canInterect)
        {
            isopenInformationUI = false;
            readCardInformation = false;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 0, 0);
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isopenInformationUI = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isopenInformationUI = false;
    }
    public void Clear()
    {
        GetComponent<InformationUIExplain>().ClearInformation();
    }
}
