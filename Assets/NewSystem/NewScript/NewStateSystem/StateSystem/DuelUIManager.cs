using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUIManager : MonoBehaviour
{
    public GameObject CameraManager;
    public GameObject BattleStateManager;
    public GameObject StateText;
    public GameObject ReadyButton;
    public GameObject MoveResultUI;
    //public GameObject ATKResultUI;
    //public GameObject DuelEndUI;
    public GameObject InformationText;

    public static bool showStateText; //顯示階段文字
    public static bool showInformationText; //顯示訊息文字
    public static string Information; //訊息文字內容

    public static bool stateEventStart; //階段執行
    

    
    public static bool PracticeEnd;
    // Start is called before the first frame update
    void Start()
    {
        showStateText = false;
        showInformationText = false;
        stateEventStart = false;

        PracticeEnd = false;

        BattleStateManager.SetActive(false);
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
        InformationText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Information;
        if (showInformationText)
        {
            InformationText.SetActive(true);
        }
        else
        {
            InformationText.SetActive(false);
        }
        if (PlayerUIManager.GetInstance().readyToDuel && EnemyManager.GetInstance().readyToDuel)
        {
            StartCoroutine(StartDuel());
        }
        if (showStateText)
        {
            StartCoroutine(ShowStateText());
        }
        if (stateEventStart)
        {
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Draw)
            {
                StartCoroutine(StartDrawStateResult());
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
            {
                StartCoroutine(StartMoveStateResult());
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.MoveResult)
            {
                StartCoroutine(EndMoveStateResult());
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
            {
                //StartCoroutine(AttackStateResult());
            }
        }
    }
    public IEnumerator StartDuel()
    {
        PlayerUIManager.GetInstance().readyToDuel = false;
        EnemyManager.GetInstance().readyToDuel = false;
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
        yield return new WaitForSeconds(0.6f);
        StateText.SetActive(false);
    }
    public IEnumerator StartDrawStateResult()
    {
        stateEventStart = false;
        showStateText = true;
        yield return new WaitForSeconds(1f);
        PlayerUIManager.GetInstance().NormalDrawCard();
    }
    public IEnumerator StartMoveStateResult()
    {
        stateEventStart = false;
        MoveResultUI.SetActive(true);
        showStateText = true;
        //PracticeDialodue.CardLimitedOnShow = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(CameraManager.GetComponent<CameraManager>().MoveStateLook());
    }
    public IEnumerator EndMoveStateResult()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var enemys = EnemyManager.GetInstance();
        stateEventStart = false;
        yield return StartCoroutine(MoveResultUI.GetComponent<NewMoveResult>().MoveResult());
        MoveResultUI.SetActive(false);
        _player_data.canMove = false;
        _player_data.isReady = true;
        enemys.isReady = true;
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
