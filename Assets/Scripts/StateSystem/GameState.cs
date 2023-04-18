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
    private float time = 0;
    public DrawState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.duelStateType = GameState.DuelStateMode.Draw;
        GameManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
        GameManager.canInterect = false;
       
        //GameManager.duelStateType = GameState.DuelStateMode.Move;
        //GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
        //manager.TransitionDuelState(GameManager.duelStateType);
        //manager.TransitionPlayerState(GameManager.playerStateType);
    }
    public void OnUpdate()
    {
        
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
        GameManager.duelStateType = GameState.DuelStateMode.Move;
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
public class AttackState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
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

public class EndState : IState //�������q(�ޥ�IState���B��Ҧ�)
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

public class DoThingState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public DoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.playerStateType = GameState.PlayerStateMode.DoThing;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class NoDoThingState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
{
    private GameManager manager;
    public NoDoThingState(GameManager manager)
    {
        this.manager = manager;
    }
    public void OnEnter()
    {
        GameManager.playerStateType = GameState.PlayerStateMode.NoDoThing;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

public class DamageState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
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

public class ReadyState : IState //�D�n���q(�ޥ�IState���B��Ҧ�)
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
