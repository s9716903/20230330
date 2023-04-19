using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState //���A���j��
{
    public enum DuelStateMode //�M���y�{���q���A
    {
        Draw,
        Move,
        Attack,
        End,
    }
    public enum PlayerStateMode //���a���A
    {
        DoThing,
        Ready,
        Damage,
        NoDoThing,
    }
}

public class DrawState : IState //��P���q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager; //�t�d�ޥΩ��U�y�{��
    public DrawState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
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

public class MoveState : IState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public MoveState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
        StateTimer.startTime = 10;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            GameManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(GameManager.playerStateType);
        }
    }
    public void OnExit()
    {

    }
}
public class AttackState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public AttackState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
        StateTimer.startTime = 10;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            GameManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(GameManager.playerStateType);
        }
    }
    public void OnExit()
    { 
    
    }
}

public class EndState : IState //�������q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public EndState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(GameManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true)
        {
            GameManager.duelStateType = GameState.DuelStateMode.Draw;
            GameManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
            manager.TransitionDuelState(GameManager.duelStateType);
        }
    }
    public void OnExit()
    {
       
    }
}

public class DoThingState : IState //���a�i���ƶ��q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public DoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.canInterect = true;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class NoDoThingState : IState //���a���i���ƶ��q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public NoDoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.canInterect = false;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class DamageState : IState //�ˮ`�B�z���q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public DamageState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.canInterect = true;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class ReadyState : IState //�ǳƵ��ⶥ�q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public ReadyState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.canInterect = false;
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.startTime == 1)
        {
            if (GameManager.duelStateType == GameState.DuelStateMode.Move)
            {
                GameManager.duelStateType = GameState.DuelStateMode.Attack;
                GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
                manager.TransitionDuelState(GameManager.duelStateType);
            }
        }
        if (StateTimer.startTime == 2)
        {
            if (GameManager.duelStateType == GameState.DuelStateMode.Attack)
            {
                GameManager.duelStateType = GameState.DuelStateMode.End;
                GameManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
                manager.TransitionDuelState(GameManager.duelStateType);
            }
        }
    }
    public void OnExit()
    {

    }
}
