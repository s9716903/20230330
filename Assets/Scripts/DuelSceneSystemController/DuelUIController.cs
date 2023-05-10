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
    public GameObject ReadyButton;
    public GameObject MoveResultUI;
    public GameObject ATKResultUI;

    public static bool startMoveStateResult;
    public static bool startAttackStateResult;
    public static bool resultEnd;

    // Start is called before the first frame update
    void Start()
    {
        startMoveStateResult = false;
        startAttackStateResult = false;
        resultEnd = false;

        MoveResultUI.SetActive(false);
        ATKResultUI.SetActive(false);
        Duelstatemanager.SetActive(false);
        DuelTimer.SetActive(false);
        ReadyButton.SetActive(false);
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
            StartCoroutine(MoveStateResult());
        }
        if (startAttackStateResult)
        {
            StartCoroutine(AttackStateResult());
        }

    }
    public IEnumerator StartDuel()
    {
        Player1.GetComponent<PlayerUI>().readyToDuel = false;
        Player2.GetComponent<PlayerUI>().readyToDuel = false;
        DuelTimer.SetActive(true);
        ReadyButton.SetActive(true);
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
        startMoveStateResult = false;
        MoveResultUI.SetActive(true);
        yield return new WaitForSeconds(3);
        MoveResultUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        var ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        var TheEnemy = GameObject.Find("Enemy").GetComponent<Player>();
        ThePlayer.TargetLocation = ThePlayer.MoveToLocation;
        TheEnemy.TargetLocation = TheEnemy.MoveToLocation;
        yield return 0;
    }
    public IEnumerator AttackStateResult()
    {
        startAttackStateResult = false;
        ATKResultUI.SetActive(true);
        yield return new WaitForSeconds(3);
        ATKResultUI.SetActive(false);
        yield return 0;
    }
}
