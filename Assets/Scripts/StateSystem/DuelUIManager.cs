using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUIManager : MonoBehaviour
{
    //DuelScene全域廣播
    public static bool showStateText; //顯示階段文字
    public static bool showInformationText; //顯示訊息文字
    public static string Information; //訊息文字內容
    public static bool stateEventStart; //階段事件執行
    public static bool BattleEnd;

    public Button PauseButton;
    public GameObject CameraManager;
    public GameObject BattleStateManager;
    public GameObject StateText;
    public GameObject MoveResult;
    public GameObject PauseUI;
    public GameObject DuelEndUI;
    public GameObject InformationText;
    // Start is called before the first frame update
    void Start()
    {
        BattleEnd = false;
        showStateText = false;
        showInformationText = false;
        stateEventStart = false;

        PauseButton.interactable = false;
        BattleStateManager.SetActive(false);
        StateText.SetActive(false);
        MoveResult.SetActive(false);
        PauseUI.SetActive(false);
        DuelEndUI.SetActive(false);
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
                StartCoroutine(StartAttackState());
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.AttackResult)
            {
                StartCoroutine(StartAttackStateResult());
            }
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.End)
            {
                StartCoroutine(EndStateResult());
            }
        }
    }
    public void PauseOrStartGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
        }
    }
    public IEnumerator StartDuel()
    {
        PlayerUIManager.GetInstance().readyToDuel = false;
        EnemyManager.GetInstance().readyToDuel = false;
        GameManager.gameManager_instance.Save();
        PauseButton.interactable = true;
        BattleStateManager.GetComponent<AudioSource>().clip = GameManager.gameManager_instance.audioClip[GameManager.gameManager_instance.audioInt];
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
        MoveResult.SetActive(true);
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
        yield return StartCoroutine(MoveResult.GetComponent<NewMoveResult>().MoveResult());
        MoveResult.SetActive(false);
        _player_data.canMove = false;
        _player_data.isReady = true;
        enemys.isReady = true;
    }
    public IEnumerator StartAttackState()
    {
        stateEventStart = false;
        yield return new WaitForSeconds(1f);
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        yield return null;
    }
    public IEnumerator StartAttackStateResult()
    {
        showInformationText = true;
        Information = "Card Result";
        stateEventStart = false;
        yield return StartCoroutine(PlayerUIManager.GetInstance().AttackReady());
        if (PlayerUIManager.GetInstance().isFirstATK)
        {
            yield return StartCoroutine(PlayerAttackResult());
            yield return 0;
            yield return StartCoroutine(EnemyAttackResult());
        }
        else
        {
            yield return StartCoroutine(EnemyAttackResult());
            yield return 0;
            yield return StartCoroutine(PlayerAttackResult());
        }
        showInformationText = false;
        yield return null;
    }
    public IEnumerator EndStateResult()
    {
        stateEventStart = false;
        yield return new WaitForSeconds(1f);
        PlayerUIManager.GetInstance().PlayerData.isReady = true;
        EnemyManager.GetInstance().isReady = true;
        yield return null;
    }
    public IEnumerator PlayerAttackResult()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        if (BattleEnd)
        {
            yield break;
        }
        if (_player_data.PlayerAction[0] == 0 && _player_data.PlayerAction[1] == 0 && _player_data.PlayerAction[2] == 0)
        {
            Information = "Player NO Action";
            yield return new WaitForSeconds(1f);
        }
        if (_player_data.PlayerAction[0] != 0)
        {
            Information = "Player Defense Result";
            _player_data.Defense += _player_data.PlayerAction[0] * (1 + _player_data.BuffValue[0]);
            _player_data.PlayerAction[0] = 0;
            yield return new WaitForSeconds(1.5f);
        }
        if (_player_data.PlayerAction[1] != 0)
        {
            var HitZone = new List<int[]>();
            Information = "Player Attack Result";
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < _player_data.ATKZone; i++)
            {
                HitZone.Add(new int[] {_player_data.PlayerLocation[0], (_player_data.PlayerLocation[1] + i + 1-4)});
            }
            _player_data.ATKHitZone = HitZone;
            yield return StartCoroutine(EnemyManager.GetInstance().EnemysHurt());
            EnemyManager.GetInstance().isEnemyDie = false;
        }
        if (BattleEnd)
        {
            DuelEndUI.SetActive(true);
            BattleStateManager.SetActive(false);
            yield break;
        }
        if (_player_data.PlayerAction[2] != 0)
        {
            Information = _player_data.SkillName;
            _player_data.Skill();
            yield return new WaitForSeconds(1.5f);
        }
        if (BattleEnd)
        {
            DuelEndUI.SetActive(true);
            BattleStateManager.SetActive(false);
            yield break;
        }
        _player_data.isReady = true;
        yield return null;
    }
    public IEnumerator EnemyAttackResult()
    {
        var enemys = EnemyManager.GetInstance();
        if (BattleEnd)
        {
            yield break;
        }
        yield return StartCoroutine(CameraManager.GetComponent<CameraManager>().EnemyAttackResult());
        if (BattleEnd)
        {
            DuelEndUI.SetActive(true);
            BattleStateManager.SetActive(false);
            yield break;
        }
        enemys.isReady = true;
        yield return null;
    }
}
