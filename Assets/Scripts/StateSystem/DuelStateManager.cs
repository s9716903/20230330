using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelStateManager : MonoBehaviour
{
    //!!!PlayerState are sometimes controlled by DuelState!!!
    //StateEvent
    public IState currentduelState; //�ثe�M�����A(�i�J/����/�h�X)
    public IState currentplayerState; //�ثe���a���A(�i�J/����/�h�X)
    public Dictionary<GameState.DuelStateMode, IState> duelstates = new Dictionary<GameState.DuelStateMode, IState>(); //���A����ɵo�ͯS�w��(�r��)
    public Dictionary<GameState.PlayerStateMode, IState> playerstates = new Dictionary<GameState.PlayerStateMode, IState>(); //���A����ɵo�ͯS�w��(�r��)

    //Very Important
    public static GameState.DuelStateMode duelStateType; //�M�����A
    public static GameState.PlayerStateMode playerStateType; //���a���A

    public static bool showStateText;
    public static bool canInterect; //���a�O�_�i�H�P�S�w�F�褬��


    // Start is called before the first frame update
    private void Awake()
    {
        duelstates.Clear();
        //�N�M�����A�ˤJ�r��
        duelstates.Add(GameState.DuelStateMode.Draw, new DrawState(this));
        duelstates.Add(GameState.DuelStateMode.Move, new MoveState(this));
        duelstates.Add(GameState.DuelStateMode.MoveResult, new MoveResultState(this));
        duelstates.Add(GameState.DuelStateMode.Attack, new AttackState(this));
        duelstates.Add(GameState.DuelStateMode.AttackResult, new AttackResultState(this));
        duelstates.Add(GameState.DuelStateMode.End, new EndState(this));

        //�N���a���A�ˤJ�r��
        playerstates.Add(GameState.PlayerStateMode.DoThing, new DoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.NoDoThing, new NoDoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.Damage, new DamageState(this));
        playerstates.Add(GameState.PlayerStateMode.Ready, new ReadyState(this));
    }
    private void Start()
    {
        //��l�ƨM���������A
        showStateText = false;
        canInterect = false;
        duelStateType = GameState.DuelStateMode.Draw;
        playerStateType = GameState.PlayerStateMode.NoDoThing;
        TransitionDuelState(duelStateType);
    }
    // Update is called once per frame
    void Update()
    {
        currentduelState.OnUpdate();
        currentplayerState.OnUpdate();
    }
    public virtual void TransitionDuelState() //�����M�����q�ɩҰ���
    {
        
    }
    public virtual void TransitionDuelState(GameState.DuelStateMode type) //�����M�����q�ɩҰ���
    {
        if (currentduelState != null)
        {
            currentduelState.OnExit();
        }
        currentduelState = duelstates[type];
        currentduelState.OnEnter();
    }
    public virtual void TransitionPlayerState() //�������a���A�ɩҰ���
    {
       
    }
    public virtual void TransitionPlayerState(GameState.PlayerStateMode type) //�������a���A�ɩҰ���
    {
        if (currentplayerState != null)
        {
            currentplayerState.OnExit();
        }
        currentplayerState = playerstates[type];
        currentplayerState.OnEnter();
    }
}
