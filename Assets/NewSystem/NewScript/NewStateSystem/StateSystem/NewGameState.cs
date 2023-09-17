using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameState : MonoBehaviour
{
    public enum NewDuelStateMode
    {
        Draw,
        Move,
        MoveResult,
        Attack,
        AttackResult,
        Damage,
        DamageResult,
        End,
    }
    public enum NewPlayerStateMode //���a���A
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
        DuelBattleManager.showStateText = true;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        PlayerUIManager.GetInstance().NormalDrawCard();
        EnemyUIManager.GetInstance().NormalDrawCard();
        /*if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 0)
        {
            PracticeDialodue.practiceduel = 1;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady && !StateTimer.pauseStateTime)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Move;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewMoveState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        _player_data.isReady = false;
        _enemy_data.isReady = false;
        _player_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        _enemy_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        DuelBattleManager.showStateText = true;
        StateTimer.startTime = 60;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
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
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady)
        {
            PlayerUIManager.GetInstance().PlayerData.canMove = false;
            EnemyUIManager.GetInstance().EnemyData.canMove = false;
            StateTimer.startTime = 0;
            StateTimer.pauseStateTime = false;
        }
        if (StateTimer.stopStateTime == true)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.MoveResult;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewMoveResultState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        EnemyUIManager.GetInstance().EnemyData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        DuelUIManager.startMoveStateResult = true;
        //EnemyAIController.AIDoThing = false;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;
            DuelUIController.resultEnd = true;
        }
        if (DuelUIController.resultEnd)
        {
            DuelUIController.resultEnd = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Attack;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewAttackState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        EnemyUIManager.GetInstance().EnemyData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        ReadyButton.LimitedUsing = 0;
        ReadyButton.PracticeLimited = 0;
        DuelBattleManager.showStateText = true;
        StateTimer.startTime = 60;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
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
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady)
        {
            StateTimer.startTime = 0;
            StateTimer.pauseStateTime = false;
        }
        if (StateTimer.stopStateTime == true)
        {
            PlayerUIManager.GetInstance().PlayerData.isReady = false;
            EnemyUIManager.GetInstance().EnemyData.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.AttackResult;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewAttackResultState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        //DuelUIController.startAttackStateResult = true;
        //EnemyAIController.AIDoThing = false;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady && DuelBattleManager.duelStateMode != NewGameState.NewDuelStateMode.Damage && !StateTimer.pauseStateTime)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;
            DuelUIController.resultEnd = true;
        }
        if (DuelUIController.resultEnd)
        {
            DuelUIController.resultEnd = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Damage;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewDamageState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        EnemyUIManager.GetInstance().EnemyData.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        ReadyButton.LimitedUsing = 0;
        ReadyButton.PracticeLimited = 0;
        DuelBattleManager.showStateText = true;
        StateTimer.startTime = 30;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        /*if (PracticeLimtedSetting.LimitedOn && PracticeLimtedSetting.PracticeTurn == 0)
        {
            PracticeDialodue.practiceduel = 4;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady)
        {
            StateTimer.startTime = 0;
        }
        if (StateTimer.stopStateTime == true)
        {
            PlayerUIManager.GetInstance().PlayerData.isReady = false;
            EnemyUIManager.GetInstance().EnemyData.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.DamageResult;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
        /*if (PracticeLimtedSetting.LimitedOn && _player_data.isReady && _enemy_data.isReady && PracticeDialodue.practiceduel == 10)
        {
            PracticeDialodue.practiceduel = 5;
            PracticeDialodue.DialogueStart = true;
        }*/
    }
}
public class DamageResultState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        PlayerUIManager.GetInstance().PlayerData.isReady = false;
        EnemyUIManager.GetInstance().EnemyData.isReady = false;
        //DuelUIController.startAttackStateResult = true;
        //EnemyAIController.AIDoThing = false;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;
            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.End;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
public class NewEndState : DuelBattleIState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    public override void EnterState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        PracticeLimtedSetting.PracticeTurn += 1;
        _player_data.PhysicATK = 0;
        _player_data.MagicATK = 0;
        _player_data.MoveValue = 0;
        _enemy_data.PhysicATK = 0;
        _enemy_data.MagicATK = 0;
        _enemy_data.MoveValue = 0;
        _player_data.Stars = 0;
        _enemy_data.Stars = 0;
        _player_data.isReady = false;
        _enemy_data.isReady = false;
        DuelBattleManager.showStateText = true;
    }
    public override void UpdateState()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var _enemy_data = EnemyUIManager.GetInstance().EnemyData;
        if (_player_data.isReady && _enemy_data.isReady && !StateTimer.pauseStateTime)
        {
            _player_data.isReady = false;
            _enemy_data.isReady = false;

            DuelBattleManager.duelStateMode = NewGameState.NewDuelStateMode.Draw;
            DuelBattleManager.TranslateDuelState(DuelBattleManager.duelStateMode);
        }
    }
}
