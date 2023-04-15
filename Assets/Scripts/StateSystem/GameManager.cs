using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���A��
    public IState currentduelState; //�ثe�M�����A(�i�J/����/�h�X)
    public IState currentplayerState; //�ثe���a���A(�i�J/����/�h�X)
    public static GameState.DuelStateMode duelStateType; //�M�����A
    public static GameState.PlayerStateMode playerStateType; //���a���A
    public Dictionary<GameState.DuelStateMode, IState> duelstates = new Dictionary<GameState.DuelStateMode, IState>(); //���A����ɵo�ͯS�w��(�r��)
    public Dictionary<GameState.PlayerStateMode, IState> playerstates = new Dictionary<GameState.PlayerStateMode, IState>(); //���A����ɵo�ͯS�w��(�r��)

    //GameManager�ۨ�
    public static GameManager instance = null;

    //�޲z��T
    public static int Hp; //��q
    public static int PhysicAtk; //��¦���z�����ƭ�
    public static int MagicAtk; //��¦�k�N�����ƭ�
    public static int MoveValue; //��¦���ʭ�
    public static int Defense; //���m��
    public static bool canInterect; //�i����

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
        //���������ۦP����ɺR���P����
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
