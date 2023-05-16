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
    public GameObject DuelEndUI;
    public GameObject DamageDropText;

    public static bool startMoveStateResult;
    public static bool startAttackStateResult;
    public static bool resultEnd;
    public static bool player1lose;
    public static bool player2lose;
    public static bool PracticeEnd;

    // Start is called before the first frame update
    void Start()
    {
        PracticeLimtedSetting.LimitedOn = false;
        startMoveStateResult = false;
        startAttackStateResult = false;
        resultEnd = false;
        player1lose = false;
        player2lose = false;
        PracticeEnd = false;
        

        MoveResultUI.SetActive(false);
        ATKResultUI.SetActive(false);
        DuelEndUI.SetActive(false);
        Duelstatemanager.SetActive(false);
        DuelTimer.SetActive(false);
        ReadyButton.SetActive(false);
        StateText.SetActive(false);
        DamageDropText.SetActive(false);
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

        if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
        {
            DamageDropText.SetActive(true);
        }
        else
        {
            DamageDropText.SetActive(false);
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
        var ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        var TheEnemy = GameObject.Find("Enemy").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        Player1.SetActive(false);
        Player2.SetActive(false);
        MoveResultUI.SetActive(true);
        yield return StartCoroutine(MoveResultUI.GetComponent<MoveResult>().StartResult());
        MoveResultUI.SetActive(false);
        Player1.SetActive(true);
        Player2.SetActive(true);
        yield return new WaitForSeconds(1f);
        player_handcards.ShowHandCard();
        enemy_handcards.ShowHandCard();
        ThePlayer.TargetLocation = ThePlayer.MoveToLocation;
        TheEnemy.TargetLocation = TheEnemy.MoveToLocation;
        yield return new WaitForSeconds(1f);
        player_handcards.HealthDrawCard();
        enemy_handcards.HealthDrawCard();
        yield return 0;
    }
    public IEnumerator AttackStateResult()
    {
        startAttackStateResult = false;
        var ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        var TheEnemy = GameObject.Find("Enemy").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        Player1.SetActive(false);
        Player2.SetActive(false);
        ReadyButton.SetActive(false);
        ATKResultUI.SetActive(true);
        yield return StartCoroutine(ATKResultUI.GetComponent<AttackResult>().StartResult());
        if (player1lose || player2lose || PracticeEnd)
        {
            if (player1lose)
            {
                ATKResultUI.SetActive(false);
                DuelEndUI.SetActive(true);
                yield break;
            }
            else if (player2lose)
            {
                ATKResultUI.SetActive(false);
                DuelEndUI.SetActive(true);
                yield break;
            }
            else if (PracticeEnd)
            {
                ATKResultUI.SetActive(false);
                yield break;
            }
        }
        else
        {
            ATKResultUI.SetActive(false);
            Player1.SetActive(true);
            Player2.SetActive(true);
            ReadyButton.SetActive(true);
            player_handcards.ShowHandCard();
            enemy_handcards.ShowHandCard();
            yield return new WaitForSeconds(1f);
            player_handcards.HealthDrawCard();
            enemy_handcards.HealthDrawCard();
            yield return 0;
        }
    }
}
