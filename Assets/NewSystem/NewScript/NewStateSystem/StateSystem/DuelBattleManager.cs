using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelBattleManager : MonoBehaviour
{
    //狀態機相關
    public static DuelBattleIState CurrentDuelState; //目前決鬥狀態(進入/持續/退出)
    public static Dictionary<NewGameState.NewDuelStateMode, DuelBattleIState> DuelIState = new Dictionary<NewGameState.NewDuelStateMode, DuelBattleIState>();
    public static NewGameState.NewDuelStateMode duelStateMode;

    //決鬥場景相關
    private AudioSource BattleMusic;
    public static bool listenDuelMusic;

    //玩家相關資料
    public static TextAsset Player1Deck;
    public static TextAsset Player2Deck;
    //public Image Player1Job;
    //public Image Player2Job;
    private void Awake()
    {
        DuelIState.Clear();
        DuelIState.Add(NewGameState.NewDuelStateMode.Draw, new NewDrawState());
        DuelIState.Add(NewGameState.NewDuelStateMode.Move, new NewMoveState());
        DuelIState.Add(NewGameState.NewDuelStateMode.MoveResult, new NewMoveResultState());
        DuelIState.Add(NewGameState.NewDuelStateMode.Attack, new NewAttackState());
        DuelIState.Add(NewGameState.NewDuelStateMode.AttackResult, new NewAttackResultState());
        DuelIState.Add(NewGameState.NewDuelStateMode.Damage, new NewDamageState());
        DuelIState.Add(NewGameState.NewDuelStateMode.DamageResult, new DamageResultState());
        DuelIState.Add(NewGameState.NewDuelStateMode.End, new NewEndState());
    }
    private void Start()
    {
        TranslateDuelState(duelStateMode);
    }
    private void Update()
    {
        CurrentDuelState.UpdateState();
    }
    public static void TranslateDuelState(NewGameState.NewDuelStateMode type) //切換決鬥階段時所執行
    {
        CurrentDuelState = DuelIState[type];
        CurrentDuelState.EnterState();
    }
}
