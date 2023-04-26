using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState //狀態機大全
{
    public enum DuelStateMode //決鬥流程階段狀態
    {
        Draw,
        Move,
        MoveResult,
        Attack,
        AttackResult,
        End,
    }
    public enum PlayerStateMode //玩家狀態
    {
        DoThing,
        Ready,
        Damage,
        NoDoThing,
    }
}

public class DrawState : IState //抽牌階段(引用IState的運行模式)
{
    private DuelStateManager manager; //負責引用底下流程的

    public DrawState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().NormalDrawCard();

        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if(StateTimer.stopStateTime == true) 
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.Move;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.DoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
    }
    public void OnExit()
    {
        
    }
}

public class MoveState : IState //移動階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    private GameObject ThePlayer;
    private GameObject TheEnemy;
    public MoveState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 10;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        ThePlayer = GameObject.Find("Player");
        TheEnemy = GameObject.Find("Enemy");
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true || (ThePlayer.GetComponent<Player>().isReady && TheEnemy.GetComponent<Player>().isReady))
        {
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
        }
    }
    public void OnExit()
    {
        
    }
}

public class MoveResultState : IState //移動階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public MoveResultState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().HealthDrawCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.Attack;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.DoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
    }
    public void OnExit()
    {

    }
}

public class AttackState : IState //主要階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    private GameObject ThePlayer;
    private GameObject TheEnemy;
    public AttackState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().ShowHandCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 10;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        ThePlayer = GameObject.Find("Player");
        TheEnemy = GameObject.Find("Enemy");
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true || (ThePlayer.GetComponent<Player>().isReady && TheEnemy.GetComponent<Player>().isReady))
        {
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
        }
    }
    public void OnExit()
    {
        
    }
}

public class AttackResultState : IState //移動階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public AttackResultState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().HealthDrawCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.End;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
    }
    public void OnExit()
    {

    }
}
public class EndState : IState //結束階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public EndState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().ShowHandCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        GameObject.Find("PlayerHandCards").GetComponent<HandCards>().NormalDrawCard();
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.Draw;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
    }
    public void OnExit()
    {
       
    }
}

public class DoThingState : IState //玩家可做事階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public DoThingState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        DuelStateManager.canInterect = true;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class NoDoThingState : IState //玩家不可做事階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public NoDoThingState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        DuelStateManager.canInterect = false;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class DamageState : IState //傷害處理階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    public DamageState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        DuelStateManager.canInterect = true;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class ReadyState : IState //準備進入結算階段(引用IState的運行模式)
{
    private DuelStateManager manager;
    private GameObject ThePlayer;
    private GameObject TheEnemy;
    public ReadyState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        ThePlayer = GameObject.Find("Player");
        TheEnemy = GameObject.Find("Enemy");
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            GameObject.Find("PlayerHandCards").GetComponent<HandCards>().PlayerIsReady();
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            GameObject.Find("PlayerHandCards").GetComponent<HandCards>().PlayerAttackIsReady();
        }
        ThePlayer.GetComponent<Player>().isReady = false;
        TheEnemy.GetComponent<Player>().isReady = false;
        ThePlayer.GetComponent<Player>().canMove = false;
    }
    public void OnUpdate()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.MoveResult;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            DuelStateManager.duelStateType = GameState.DuelStateMode.AttackResult;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
            manager.TransitionDuelState(DuelStateManager.duelStateType);
        }
    }
    public void OnExit()
    {

    }
}
