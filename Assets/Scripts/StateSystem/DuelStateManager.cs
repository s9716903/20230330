using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelStateManager : MonoBehaviour
{
    //!!!PlayerState are sometimes controlled by DuelState!!!
    //StateEvent
    public IState currentduelState; //目前決鬥狀態(進入/持續/退出)
    public IState currentplayerState; //目前玩家狀態(進入/持續/退出)
    public Dictionary<GameState.DuelStateMode, IState> duelstates = new Dictionary<GameState.DuelStateMode, IState>(); //狀態執行時發生特定事(字典)
    public Dictionary<GameState.PlayerStateMode, IState> playerstates = new Dictionary<GameState.PlayerStateMode, IState>(); //狀態執行時發生特定事(字典)

    //Very Important
    public static GameState.DuelStateMode duelStateType; //決鬥狀態
    public static GameState.PlayerStateMode playerStateType; //玩家狀態

    public static bool showStateText;
    public static bool canInterect; //玩家是否可以與特定東西互動

    private AudioSource audioSource;
    public static bool listenDuelMusic;

    // Start is called before the first frame update
    private void Awake()
    {
        duelstates.Clear();
        //將決鬥狀態裝入字典
        duelstates.Add(GameState.DuelStateMode.Draw, new DrawState(this));
        duelstates.Add(GameState.DuelStateMode.Move, new MoveState(this));
        duelstates.Add(GameState.DuelStateMode.MoveResult, new MoveResultState(this));
        duelstates.Add(GameState.DuelStateMode.Attack, new AttackState(this));
        duelstates.Add(GameState.DuelStateMode.AttackResult, new AttackResultState(this));
        duelstates.Add(GameState.DuelStateMode.End, new EndState(this));

        //將玩家狀態裝入字典
        playerstates.Add(GameState.PlayerStateMode.DoThing, new DoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.NoDoThing, new NoDoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.Damage, new DamageState(this));
        playerstates.Add(GameState.PlayerStateMode.Ready, new ReadyState(this));
    }
    private void Start()
    {
        listenDuelMusic = true;
        audioSource = GetComponent<AudioSource>();
        //初始化決鬥場景狀態
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
        if (!listenDuelMusic)
        {
            audioSource.Stop();
        }
    }
    public virtual void TransitionDuelState() //切換決鬥階段時所執行
    {
        
    }
    public virtual void TransitionDuelState(GameState.DuelStateMode type) //切換決鬥階段時所執行
    {
        if (currentduelState != null)
        {
            currentduelState.OnExit();
        }
        currentduelState = duelstates[type];
        currentduelState.OnEnter();
    }
    public virtual void TransitionPlayerState() //切換玩家狀態時所執行
    {
       
    }
    public virtual void TransitionPlayerState(GameState.PlayerStateMode type) //切換玩家狀態時所執行
    {
        if (currentplayerState != null)
        {
            currentplayerState.OnExit();
        }
        currentplayerState = playerstates[type];
        currentplayerState.OnEnter();
    }
}
