using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelStateManager : MonoBehaviour
{
    //!!!玩家狀態機在部分時間點由決鬥狀態控制狀態機進出!!!
    //狀態機
    public IState currentduelState; //目前決鬥狀態(進入/持續/退出)
    public IState currentplayerState; //目前玩家狀態(進入/持續/退出)
    public static GameState.DuelStateMode duelStateType; //決鬥狀態
    public static GameState.PlayerStateMode playerStateType; //玩家狀態
    public Dictionary<GameState.DuelStateMode, IState> duelstates = new Dictionary<GameState.DuelStateMode, IState>(); //狀態執行時發生特定事(字典)
    public Dictionary<GameState.PlayerStateMode, IState> playerstates = new Dictionary<GameState.PlayerStateMode, IState>(); //狀態執行時發生特定事(字典)

    //管理資訊
    public static bool canInterect; //玩家可以進行操作

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

        //場景中有GameManager時摧毀同名物件
        /*if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
    }
    private void Start()
    {
        //初始化決鬥場景狀態
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
        Debug.Log(currentduelState);
        Debug.Log(currentplayerState);
        Debug.Log(duelStateType);
        Debug.Log(playerStateType);
        Debug.Log(canInterect);

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
