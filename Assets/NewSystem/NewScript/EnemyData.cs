using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
    private Dictionary<int, System.Action> Actions = new Dictionary<int, System.Action>();

    //��¦�ƭ�
    public string EnemyName;
    public int HP;
    public int Defense;
    public int PhysicATKValue;
    public int MagicATKValue;
    public int ATKType;
    //public int MoveValue;
    public List<int> BasicValue;
    public int[] BuffValue;

    public int[] ThisLocation; //�өҦb��m
    public int[] PlayerLocation; //���a�Ҧb��m
    public int AllDamaged; //���ˮ`�`�ƭ�

    public int[] Action;
    public int ChooseAction;
    //public int[] MoveToLocation = new int[2]; //�N�n���ʨ쪺��m

    //���A
    //public bool canMove; //��ܲ��ʦܭ���

    private void Awake()
    {
        Actions.Add(0,PhysicATKAction);
        Actions.Add(1, MagicATKAction);
        Actions.Add(2, TrapAction);
        Actions.Add(3, ObstacleAction);
        Actions.Add(4, SkillAction);
    }
    public void Start()
    {
        SettingValue();
        Action = new int[] { 0, 1, 2, 3, 4 };
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TheAction();
        }
    }
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        gameObject.name = EnemyName;
        BasicValue = new List<int> { Defense, PhysicATKValue, MagicATKValue };
        //HP = GameManager.gameManager_instance.Jobs[JobIndex].TheMaxHP;
        //Defense = GameManager.gameManager_instance.Jobs[JobIndex].TheDefense;
    }
    public void TheAction()
    {
        ChooseAction = Random.Range(Action[0],Action.Length);
        Actions[ChooseAction]();
        Debug.Log(ChooseAction);
    }
    public void PhysicATKAction()
    { 
    
    }
    public void MagicATKAction()
    {

    }
    public void TrapAction()
    {

    }
    public void ObstacleAction()
    {

    }
    public void SkillAction()
    {

    }
}
