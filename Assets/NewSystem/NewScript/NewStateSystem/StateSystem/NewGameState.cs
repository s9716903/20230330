using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DuelBattleIState : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void UpdateState();
}
public class NewGameState : MonoBehaviour
{
    public enum NewDuelStateMode
    {
        Draw,
        Move,
        MoveResult,
        Attack,
        AttackResult,
        End,
    }
    public enum NewPlayerStateMode //玩家狀態
    {
        PlayerActivate,
        PlayerReady,
        PlayerDeactivate,
    }
}
public class NewDrawState : DuelBattleIState
{
    public override void EnterState()
    {
        //EnemyAIController.AIDoThing = false;
        if (PlayerUIManager.GetInstance().HandCardAmount < 5)
        {
            PlayerUIManager.GetInstance().PlayerData.DrawAmount += (5 - PlayerUIManager.GetInstance().HandCardAmount);
        }
        else
        {
            PlayerUIManager.GetInstance().PlayerData.DrawAmount += 1;
        }
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyManager.GetInstance().isReady = true;
        DuelUIManager.stateEventStart = true;
        /*if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 0)
        {
            PracticeDialodue.practiceduel = 1;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Move;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewMoveState : DuelBattleIState //移動階段(引用IState的運行模式)
{
    public override void EnterState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        _player_data.isReady = false;
        _enemyManager.isReady = false;
        _player_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        DuelUIManager.stateEventStart = true;
        /*if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 0)
        {
            PracticeDialodue.practiceduel = 2;
            PracticeDialodue.DialogueStart = true;
        }
        else if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 1)
        {
            PracticeDialodue.practiceduel = 6;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.canMove = false;
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.MoveResult;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
    public virtual IEnumerator MoveStateStart()
    {
        yield return new WaitForSeconds(1f);
    }
}
public class NewMoveResultState : DuelBattleIState //移動階段(引用IState的運行模式)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyManager.GetInstance().isReady = false;
        DuelUIManager.stateEventStart = true;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Attack;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewAttackState : DuelBattleIState //移動階段(引用IState的運行模式)
{
    public override void EnterState()
    {
        DuelUIManager.showStateText = true;
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyManager.GetInstance().isReady = true;
        /*if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 0)
        {
            PracticeDialodue.practiceduel = 3;
            PracticeDialodue.DialogueStart = true;
        }
        else if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 1)
        {
            PracticeDialodue.practiceduel = 7;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.AttackResult;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewAttackResultState : DuelBattleIState //移動階段(引用IState的運行模式)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyManager.GetInstance().isReady = false;
        //DuelUIController.startAttackStateResult = true;
        //EnemyAIController.AIDoThing = false;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            //DuelUIController.resultEnd = true;
        }
        /*if (DuelUIController.resultEnd)
        {
            DuelUIController.resultEnd = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.End;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }*/
    }
}
public class NewEndState : DuelBattleIState //移動階段(引用IState的運行模式)
{
    public override void EnterState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        //PracticeLimtedSetting.PracticeTurn += 1;
        _player_data.isReady = false;
        _enemyManager.isReady = false;
        DuelUIManager.showStateText = true;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemyManager = EnemyManager.GetInstance();
        if (_player_data.isReady && _enemyManager.isReady)
        {
            _player_data.isReady = false;
            _enemyManager.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Draw;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
