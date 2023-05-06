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
        var player = GameObject.Find("Player").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        DuelStateManager.showStateText = true;
        if (player.Hp != player.MaxHp)
        { 
            player_handcards.NormalDrawCard();
        }
        if (enemy.Hp != enemy.MaxHp)
        {
            enemy_handcards.NormalDrawCard();
        }
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
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
        DuelStateManager.showStateText = true;
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
        var ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        var TheEnemy = GameObject.Find("Enemy").GetComponent<Player>();
        ThePlayer.TargetLocation = ThePlayer.MoveToLocation;
        TheEnemy.TargetLocation = TheEnemy.MoveToLocation;
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        player_handcards.HealthDrawCard();
        enemy_handcards.HealthDrawCard();
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
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        DuelStateManager.showStateText = true;
        player_handcards.ShowHandCard();
        enemy_handcards.ShowHandCard();
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
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        player_handcards.HealthDrawCard();
        enemy_handcards.HealthDrawCard();
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
        var player = GameObject.Find("Player").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        DuelStateManager.showStateText = true;
        player_handcards.ShowHandCard();
        enemy_handcards.ShowHandCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        if (player.Hp != player.MaxHp)
        {
            player_handcards.NormalDrawCard();
            enemy_handcards.NormalDrawCard();
        }
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
    public ReadyState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        var player = GameObject.Find("Player").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        //TheEnemy = GameObject.Find("Enemy");
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            player_handcards.PlayerIdleReady();
            enemy_handcards.PlayerIdleReady();
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            player_handcards.PlayerIdleReady();
            enemy_handcards.PlayerIdleReady();
        }
        player.isReady = false;
        enemy.isReady = false;
        player.canMove = false;
        enemy.canMove = false;
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
