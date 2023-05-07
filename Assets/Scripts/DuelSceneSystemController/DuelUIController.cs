using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUIController : MonoBehaviour
{
    public GameObject Duelstatemanager;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject StateText;
    public GameObject DuelTimer;
    public GameObject ResultUI;

    private bool startMoveStateResult;
    private bool startAttackStateResult;
    // Start is called before the first frame update
    void Start()
    {
        ResultUI.SetActive(false);
        startMoveStateResult = false;
        startAttackStateResult = false;
        Duelstatemanager.SetActive(false);
        DuelTimer.SetActive(false);
        StateText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StateText.GetComponent<TextMeshProUGUI>().text = DuelStateManager.duelStateType.ToString();
        if (Player1.GetComponent<PlayerUI>().readyToDuel && Player2.GetComponent<PlayerUI>().readyToDuel)
        {
            StartCoroutine(StartDuel());
        }
        if (DuelStateManager.showStateText)
        {
            StartCoroutine(ShowStateText());
        }
        if (startMoveStateResult)
        {
            startMoveStateResult = false;

        }

    }
    public IEnumerator StartDuel()
    {
        Player1.GetComponent<PlayerUI>().readyToDuel = false;
        Player2.GetComponent<PlayerUI>().readyToDuel = false;
        DuelTimer.SetActive(true);
        yield return new WaitForSeconds(1f);
        Duelstatemanager.SetActive(true);
        yield return 0;
    }
    public IEnumerator ShowStateText()
    {
        DuelStateManager.showStateText = false;
        StateText.GetComponent<TextMeshProUGUI>().text = DuelStateManager.duelStateType.ToString();
        StateText.SetActive(true);
        yield return new WaitForSeconds(1f);
        StateText.SetActive(false);
    }

    public IEnumerator MoveStateResult()
    {
        var Player1HandCard = Player1.GetComponent<PlayerUI>().PlayerHandZone.gameObject;
        var Player2HandCard = Player2.GetComponent<PlayerUI>().PlayerHandZone.gameObject;
        ResultUI.SetActive(true);
        for (int i = 0; i < Player1HandCard.transform.childCount; i++)
        {
            if (Player1HandCard.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                //Instantiate(Player1HandCard.transform.GetChild(i), ); //卡片變成手牌子物件
            }
        }
        yield return 0;
    }
}
