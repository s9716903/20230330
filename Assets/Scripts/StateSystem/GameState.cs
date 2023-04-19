using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState //狀態機大全
{
    public enum DuelStateMode //決鬥流程階段狀態
    {
        Draw,
        Move,
        Attack,
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
    private GameManager manager; //負責引用底下流程的
    private float time = 0;
    public DrawState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
        GameManager.canInterect = false;
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if(StateTimer.stopStateTime == true) 
        {
            GameManager.duelStateType = GameState.DuelStateMode.Move;
            GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
            manager.TransitionDuelState(GameManager.duelStateType);
        }
    }
    public void OnExit()
    {
        
    }
}

public class MoveState : IState //移動階段(引用IState的運行模式)
{
    private GameManager manager;
    public MoveState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
        GameManager.canInterect = true;
        StateTimer.startTime = 60;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            GameManager.duelStateType = GameState.DuelStateMode.Move;
            GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
            manager.TransitionDuelState(GameManager.duelStateType);
        }
    }
    public void OnExit()
    {

    }
}
public class AttackState : IState //主要階段(引用IState的運行模式)
{
    private GameManager manager;
    public AttackState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.duelStateType = GameState.DuelStateMode.Attack;
        GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
        GameManager.canInterect = true;
    }
    public void OnUpdate()
    { 
    
    }
    public void OnExit()
    { 
    
    }
}

public class EndState : IState //結束階段(引用IState的運行模式)
{
    private GameManager manager;
    public EndState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.duelStateType = GameState.DuelStateMode.End;
        GameManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
        GameManager.canInterect = false;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {
       
    }
}

public class DoThingState : IState //主要階段(引用IState的運行模式)
{
    private GameManager manager;
    public DoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class NoDoThingState : IState //主要階段(引用IState的運行模式)
{
    private GameManager manager;
    public NoDoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class DamageState : IState //主要階段(引用IState的運行模式)
{
    private GameManager manager;
    public DamageState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.playerStateType = GameState.PlayerStateMode.Damage;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class ReadyState : IState //主要階段(引用IState的運行模式)
{
    private GameManager manager;
    public ReadyState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.playerStateType = GameState.PlayerStateMode.Ready;
        GameManager.canInterect = false;
    }
    public void OnUpdate()
    {
        
    }
    public void OnExit()
    {

    }
}
