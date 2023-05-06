using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum CardState
{
    Top,
    Bottom
}
public class CardTurnOver : MonoBehaviour
{
    public CardState cardState;

    public GameObject CardTop;
    public GameObject CardBottom;
    private float ChangeTime = 0.3f;

    public bool isChangeTopOrBottom; //是否正在翻轉中
    public static bool isChangeUpOrDown;

    public void CardInit()
    {
        if (cardState == CardState.Top)
        {
            CardTop.transform.eulerAngles = new Vector3(90, 0, 0);
            CardBottom.transform.eulerAngles = new Vector3(0, 90, 90);
            CardTop.SetActive(true);
            CardBottom.SetActive(false);
        }
        else
        {
            CardTop.transform.eulerAngles = new Vector3(0, 90, 90);
            CardBottom.transform.eulerAngles = new Vector3(90, 0, 0);
            CardTop.SetActive(false);
            CardBottom.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardState = CardState.Bottom;
        isChangeTopOrBottom = false;
        CardInit();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CardStartTop()
    {
        if (isChangeTopOrBottom)
        {
            return;
        }
        StartCoroutine(ToTop());
    }
    public void CardStartBottom()
    {
        if (isChangeTopOrBottom)
        {
            return;
        }
        StartCoroutine(ToBottom());
    }
    public void CardStartUp()
    {
        if (isChangeUpOrDown)
        {
            return;
        }
        StartCoroutine(ToUp());
    }
    public void CardStartDown()
    {
        if (isChangeUpOrDown)
        {
            return;
        }
        StartCoroutine(ToDown());
    }
    IEnumerator ToTop()
    {
        isChangeTopOrBottom = true;
        CardBottom.transform.DORotate(new Vector3(0,90,90), ChangeTime);
        for (float i = ChangeTime; i > 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        CardTop.SetActive(true);
        CardTop.transform.DORotate(new Vector3(90, 0, 0), ChangeTime);
        cardState = CardState.Top;
        CardBottom.SetActive(false);
        isChangeTopOrBottom = false;
    }
    IEnumerator ToBottom()
    {
        isChangeTopOrBottom = true;
        CardTop.transform.DORotate(new Vector3(0, 90, 90), ChangeTime);
        for (float i = ChangeTime; i > 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        CardBottom.SetActive(true);
        CardBottom.transform.DORotate(new Vector3(90, 0, 0), ChangeTime);
        cardState = CardState.Bottom;
        CardTop.SetActive(false);
        isChangeTopOrBottom = false;
    }
    IEnumerator ToUp()
    {
        isChangeUpOrDown = true;
        CardTop.transform.DORotate(new Vector3(90, 0, 0), ChangeTime);
        for (float i = ChangeTime; i > 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        isChangeUpOrDown = false;
        gameObject.GetComponent<CardManager>().isCardUp = true;
    }
    IEnumerator ToDown()
    {
        isChangeUpOrDown = true;
        CardTop.transform.DORotate(new Vector3(90, 0, 180), ChangeTime);
        for (float i = ChangeTime; i > 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        isChangeUpOrDown = false;
        gameObject.GetComponent<CardManager>().isCardUp = false;
    }
}
