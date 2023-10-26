using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyData : MonoBehaviour
{
    //��¦�ƭ�
    public int EnemyID;
    public int HP;
    public int Defense;
    public int ATKValue;
    public List<int[]> ActionZone;

    //public int[] ThisLocation; //�өҦb��m
    public int[] EnemyLocation; //�ĤH�Ҧb��m
    public int AllDamaged; //���ˮ`�`�ƭ�
    public string ActionNameText;
    public int ChooseActionType;

    //�S��ƭ�
    public int EnemySkillValue;
    //public int[] MoveToLocation = new int[2]; //�N�n���ʨ쪺��m
    public void Start()
    {
        SettingValue();
    }
    void Update()
    {
        if (EnemyManager.GetInstance().isEnemyDie && HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SettingValue()
    {
        var EnemySetting = GameManager.gameManager_instance.Enemys;
        EnemyID = Random.Range(0,EnemySetting.Count);
        EnemySetting[EnemyID].Setting();
        gameObject.name = EnemySetting[EnemyID].Name;
        HP = EnemySetting[EnemyID].HP;
        Defense = EnemySetting[EnemyID].Defense;
        ATKValue = EnemySetting[EnemyID].ATKValue;
    }
    public void EnemyAction()
    {
        var EnemySetting = GameManager.gameManager_instance.Enemys;
        var actiontype = Random.Range(0, EnemySetting[EnemyID].ActionType.Length);
        EnemySetting[EnemyID].Action(actiontype);
        ActionNameText = EnemySetting[EnemyID].ActionName;
        ChooseActionType = EnemySetting[EnemyID].ChooseActionType;
        ActionZone = EnemySetting[EnemyID].ActionZone;
        EnemyManager.GetInstance().EnemyATKZone = ActionZone;
    }
}
