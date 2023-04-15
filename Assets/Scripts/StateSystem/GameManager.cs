using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //狀態機
    public IState currentduelState; //目前決鬥狀態(進入/持續/退出)
    public IState currentplayerState; //目前玩家狀態(進入/持續/退出)
    public static GameState.DuelStateMode duelStateType; //決鬥狀態
    public static GameState.PlayerStateMode playerStateType; //玩家狀態
    public Dictionary<GameState.DuelStateMode, IState> duelstates = new Dictionary<GameState.DuelStateMode, IState>(); //狀態執行時發生特定事(字典)
    public Dictionary<GameState.PlayerStateMode, IState> playerstates = new Dictionary<GameState.PlayerStateMode, IState>(); //狀態執行時發生特定事(字典)

    //GameManager自身
    public static GameManager instance = null;

    //管理資訊
    public static int Hp; //血量
    public static int PhysicAtk; //基礎物理攻擊數值
    public static int MagicAtk; //基礎法術攻擊數值
    public static int MoveValue; //基礎移動值
    public static int Defense; //防禦值
    public static bool canInterect; //可互動

    // Start is called before the first frame update
    private void Awake()
    {       
        duelstates.Add(GameState.DuelStateMode.Draw, new DrawState(this));
        duelstates.Add(GameState.DuelStateMode.Move, new MoveState(this));
        duelstates.Add(GameState.DuelStateMode.Attack, new AttackState(this));
        duelstates.Add(GameState.DuelStateMode.End, new EndState(this));
        playerstates.Add(GameState.PlayerStateMode.DoThing, new DoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.NoDoThing, new NoDoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.Damage, new DamageState(this));
        playerstates.Add(GameState.PlayerStateMode.Ready, new ReadyState(this));

        //TransitionState(GameState.DuelStateMode.DrawState);
        //場景中有相同物體時摧毀同物體
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        duelStateType = GameState.DuelStateMode.Draw;
        playerStateType = GameState.PlayerStateMode.NoDoThing;
        TransitionDuelState(GameState.DuelStateMode.Draw);
        TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
        currentduelState.OnEnter();
        currentplayerState.OnEnter();
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
    }
    public void TransitionDuelState(GameState.DuelStateMode type)
    {
        if (currentduelState != null)
        {
            currentduelState.OnExit();
        }
        currentduelState = duelstates[type];
        currentduelState.OnEnter();
    }
    public void TransitionPlayerState(GameState.PlayerStateMode type)
    {
        if (currentplayerState != null)
        {
            currentplayerState.OnExit();
        }
        currentplayerState = playerstates[type];
        currentplayerState.OnEnter();
    }
}
