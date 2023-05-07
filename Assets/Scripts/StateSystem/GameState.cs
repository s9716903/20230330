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
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();

        player.isReady = false;
        enemy.isReady = false;
        
        DuelStateManager.showStateText = true;
        
        player_handcards.NormalDrawCard();
        enemy_handcards.NormalDrawCard();
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
    }
    public void OnUpdate()
    {
        var player = GameObject.Find("Player").GetComponent<Player>();
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        if (player.isReady && enemy.isReady)
        {
            player.isReady = false;
            enemy.isReady = false;
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
        var player = ThePlayer.GetComponent<Player>();
        var enemy =  TheEnemy.GetComponent<Player>();
        if (player.isReady && enemy.isReady)
        {
            StateTimer.startTime = 0;
        }
        if (StateTimer.stopStateTime == true)
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
        DuelUIController.startMoveStateResult = true;
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
    }
    public void OnUpdate()
    {
        if (DuelUIController.resultEnd)
        {
            DuelUIController.resultEnd = false;
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
        var player = ThePlayer.GetComponent<Player>();
        var enemy = TheEnemy.GetComponent<Player>();
        if (player.isReady && enemy.isReady)
        {
            StateTimer.startTime = 0;
        }
        if (StateTimer.stopStateTime == true)
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
        DuelUIController.startAttackStateResult = true;
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
    }
    public void OnUpdate()
    {
        if (DuelUIController.resultEnd)
        {
            DuelUIController.resultEnd = false;
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Damage;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
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
        player_handcards.NormalDrawCard();
        enemy_handcards.NormalDrawCard();
        player.PhysicDamage = 0;
        player.MagicDamage = 0;
        player.MoveValue = 0;
        enemy.PhysicDamage = 0;
        enemy.MagicDamage = 0;
        enemy.MoveValue = 0;
    }
    public void OnUpdate()
    {
        var player = GameObject.Find("Player").GetComponent<Player>();
        var enemy = GameObject.Find("Enemy").GetComponent<Player>();
        if (player.isReady && enemy.isReady)
        {
            player.isReady = false;
            enemy.isReady = false;
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
    private GameObject ThePlayer;
    private GameObject TheEnemy;
    public DamageState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        DuelStateManager.canInterect = true;
        var player_handcards = GameObject.Find("PlayerHandCards").GetComponent<HandCards>();
        var enemy_handcards = GameObject.Find("EnemyHandCards").GetComponent<HandCards>();
        player_handcards.ShowHandCard();
        enemy_handcards.ShowHandCard();
        StateTimer.startTime = 10;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
        ThePlayer = GameObject.Find("Player");
        TheEnemy = GameObject.Find("Enemy");
    }
    public void OnUpdate()
    {
        var player = ThePlayer.GetComponent<Player>();
        var enemy = TheEnemy.GetComponent<Player>();
        if (player.isReady && enemy.isReady)
        {
            StateTimer.startTime = 0;
        }
        if (StateTimer.stopStateTime == true)
        {
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
        }
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
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult)
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
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult)
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
