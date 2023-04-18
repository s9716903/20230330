using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //!!!���a���A���b�����ɶ��I�ѨM�����A����A���i�X!!!
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
    public static bool canInterect = false; //���a�i�H�i��ʧ@

    // Start is called before the first frame update
    private void Awake()
    {       
        //�N�M�����A�ˤJ�r��
        duelstates.Add(GameState.DuelStateMode.Draw, new DrawState(this));
        duelstates.Add(GameState.DuelStateMode.Move, new MoveState(this));
        duelstates.Add(GameState.DuelStateMode.Attack, new AttackState(this));
        duelstates.Add(GameState.DuelStateMode.End, new EndState(this));

        //�N���a���A�ˤJ�r��
        playerstates.Add(GameState.PlayerStateMode.DoThing, new DoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.NoDoThing, new NoDoThingState(this));
        playerstates.Add(GameState.PlayerStateMode.Damage, new DamageState(this));
        playerstates.Add(GameState.PlayerStateMode.Ready, new ReadyState(this));

        //��������GameManager�ɺR���P�W����
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
        //��l�ƨM���������A
        duelStateType = GameState.DuelStateMode.Draw;
        playerStateType = GameState.PlayerStateMode.NoDoThing;
        TransitionDuelState(GameState.DuelStateMode.Draw);
        //TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
    }
    // Update is called once per frame
    void Update()
    {
        //currentduelState.OnUpdate();
        //currentplayerState.OnUpdate();
        Debug.Log(currentduelState);
        Debug.Log(currentplayerState);
        Debug.Log(duelStateType);
        Debug.Log(playerStateType);

        if (Input.GetKeyDown(KeyCode.W)) //���դ������q
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
