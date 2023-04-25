using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState //���A���j��
{
    public enum DuelStateMode //�M���y�{���q���A
    {
        Draw,
        Move,
        MoveResult,
        Attack,
        AttackResult,
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
    private DuelStateManager manager; //�t�d�ޥΩ��U�y�{��
    public DrawState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
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

public class MoveState : IState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public MoveState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 30;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true || (Player.isReady && Enemy.isReady))
        {
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
        }
    }
    public void OnExit()
    {
        
    }
}

public class MoveResultState : IState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public MoveResultState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
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

public class AttackState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public AttackState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 30;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
    }
    public void OnUpdate()
    {
        if (StateTimer.stopStateTime == true || (Player.isReady && Enemy.isReady))
        {
            DuelStateManager.playerStateType = GameState.PlayerStateMode.Ready;
            manager.TransitionPlayerState(DuelStateManager.playerStateType);
        }
    }
    public void OnExit()
    {

    }
}

public class AttackResultState : IState //���ʶ��q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public AttackResultState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
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
        if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Ready)
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
public class EndState : IState //�������q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public EndState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        manager.TransitionPlayerState(DuelStateManager.playerStateType);
        StateTimer.startTime = 5;
        StateTimer.isStartTime = true;
        StateTimer.stopStateTime = false;
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

public class DoThingState : IState //���a�i���ƶ��q(�ޥ�IState���B��Ҧ�)
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

public class NoDoThingState : IState //���a���i���ƶ��q(�ޥ�IState���B��Ҧ�)
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

public class DamageState : IState //�ˮ`�B�z���q(�ޥ�IState���B��Ҧ�)
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

public class ReadyState : IState //�ǳƶi�J���ⶥ�q(�ޥ�IState���B��Ҧ�)
{
    private DuelStateManager manager;
    public ReadyState(DuelStateManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        Player.isReady = false;
        Enemy.isReady = false;
        Player.canMove = false;
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
