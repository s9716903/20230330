using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUIManager : MonoBehaviour
{
    public GameObject BattleStateManager;
    public GameObject StateText;
    public GameObject DuelTimer;
    public GameObject ReadyButton;
    public GameObject MoveResultUI;
    //public GameObject ATKResultUI;
    //public GameObject DuelEndUI;
    public GameObject InformationText;

    public static bool showStateText; //顯示階段文字
    public static bool startMoveStateResult; //開始呈現結算
    public static bool startAttackStateResult;
    public static bool resultEnd; //結束結算
    public static bool player1lose;
    public static bool player2lose;
    public static bool PracticeEnd;
    // Start is called before the first frame update
    void Start()
    {
        showStateText = false;
        PracticeLimtedSetting.LimitedOn = false;
        startMoveStateResult = false;
        startAttackStateResult = false;
        resultEnd = false;
        player1lose = false;
        player2lose = false;
        PracticeEnd = false;

        BattleStateManager.SetActive(false);
        DuelTimer.SetActive(false);
        ReadyButton.SetActive(false);
        StateText.SetActive(false);
        MoveResultUI.SetActive(false);
        //ATKResultUI.SetActive(false);
        //DuelEndUI.SetActive(false);
        InformationText.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //StateText.GetComponent<TextMeshProUGUI>().text = DuelBattleManager.duelStateMode.ToString();
        if (PlayerUIManager.GetInstance().readyToDuel && EnemyUIManager.GetInstance().readyToDuel)
        {
            StartCoroutine(StartDuel());
        }
        if (showStateText)
        {
            StartCoroutine(ShowStateText());
        }
        if (startMoveStateResult)
        {
            StartCoroutine(MoveStateResult());
        }
        if (startAttackStateResult)
        {
            //StartCoroutine(AttackStateResult());
        }
        /*if (startMoveStateResult)
        {
            StartCoroutine(MoveStateResult());
        }
        if (startAttackStateResult)
        {
            StartCoroutine(AttackStateResult());
        }*/

        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.DamageResult)
        {
            InformationText.SetActive(true);
        }
    }
    public IEnumerator StartDuel()
    {
        PlayerUIManager.GetInstance().readyToDuel = false;
        EnemyUIManager.GetInstance().readyToDuel = false;
        DuelTimer.SetActive(true);
        ReadyButton.SetActive(true);
        yield return new WaitForSeconds(1f);
        BattleStateManager.SetActive(true);
        yield return 0;
    }
    public IEnumerator ShowStateText()
    {
        showStateText = false;
        StateText.GetComponent<TextMeshProUGUI>().text = DuelBattleManager.duelStateMode.ToString();
        StateText.SetActive(true);
        yield return new WaitForSeconds(1f);
        StateText.SetActive(false);
    }

    public IEnumerator MoveStateResult()
    {
        startMoveStateResult = false;
        var ThePlayer = PlayerUIManager.GetInstance().PlayerData;
        var TheEnemy = EnemyUIManager.GetInstance().EnemyData;
        MoveResultUI.SetActive(true);
        yield return StartCoroutine(MoveResultUI.GetComponent<NewMoveResult>().StartResult());
        MoveResultUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        //PlayerUIManager.GetInstance().ShowHandCard();
        //enemy_handcards.ShowHandCard();
        yield return new WaitForSeconds(1f);
        PlayerUIManager.GetInstance().HealthDrawCard();
        EnemyUIManager.GetInstance().HealthDrawCard();
        yield return 0;
    }
    /*public IEnumerator AttackStateResult()
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
                DuelStateManager.listenDuelMusic = false;
                ATKResultUI.SetActive(false);
                DuelEndUI.SetActive(true);
                yield break;
            }
            else if (player2lose)
            {
                DuelStateManager.listenDuelMusic = false;
                ATKResultUI.SetActive(false);
                DuelEndUI.SetActive(true);
                yield break;
            }
            else if (PracticeEnd)
            {
                DuelStateManager.listenDuelMusic = false;
                ATKResultUI.SetActive(false);
                PracticeDialodue.practiceduel = 8;
                PracticeDialodue.DialogueStart = true;
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
    }*/
}
