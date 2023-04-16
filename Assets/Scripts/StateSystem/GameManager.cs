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
    public static bool canInterect = false; //玩家可以進行動作

    // Start is called before the first frame update
    private void Awake()
    {       
        //將決鬥狀態裝入字典
        duelstates.Add(GameState.DuelStateMode.Draw, new DrawState(this));
        duelstates.Add(GameState.DuelStateMode.Move, new MoveState(this));
        duelstates.Add(GameState.DuelStateMode.Attack, new AttackState(this));
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
        duelStateType = GameState.DuelStateMode.Draw;
        playerStateType = GameState.PlayerStateMode.NoDoThing;
        TransitionDuelState(GameState.DuelStateMode.Draw);
        TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
        currentduelState.OnEnter();
        currentplayerState.OnEnter();
        Debug.Log(currentduelState);
        Debug.Log(currentplayerState);
        Debug.Log(duelStateType);
        Debug.Log(playerStateType);
    }
    // Update is called once per frame
    void Update()
    {
        currentduelState.OnUpdate();
        currentplayerState.OnUpdate();

        if (Input.GetKeyDown(KeyCode.W)) //測試切換階段
        {
            switch (duelStateType)
            {
                case GameState.DuelStateMode.Draw:
                    TransitionDuelState(GameState.DuelStateMode.Move);
                    TransitionPlayerState(GameState.PlayerStateMode.DoThing);
                    break;
                case GameState.DuelStateMode.Move:
                    TransitionDuelState(GameState.DuelStateMode.Attack);
                    TransitionPlayerState(GameState.PlayerStateMode.DoThing);
                    break;
                case GameState.DuelStateMode.Attack:
                    TransitionDuelState(GameState.DuelStateMode.End);
                    TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
                    break;
                case GameState.DuelStateMode.End:
                    TransitionDuelState(GameState.DuelStateMode.Draw);
                    TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
                    break;
            }
        }
    }
    public void TransitionDuelState(GameState.DuelStateMode type) //切換決鬥階段時所執行
    {
        if (currentduelState != null)
        {
            currentduelState.OnExit();
        }
        currentduelState = duelstates[type];
        currentduelState.OnEnter();
    }
    public void TransitionPlayerState(GameState.PlayerStateMode type) //切換玩家狀態時所執行
    {
        if (currentplayerState != null)
        {
            currentplayerState.OnExit();
        }
        currentplayerState = playerstates[type];
        currentplayerState.OnEnter();
    }
}
