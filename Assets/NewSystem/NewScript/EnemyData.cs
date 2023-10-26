using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //��¦�ƭ�
    public int EnemyID;
    public int HP;
    public int Defense;
    public int ATKValue;
    public List<int[]> ATKZone;

    //public int MoveValue;

    public int[] ThisLocation; //�өҦb��m
    public int[] PlayerLocation; //���a�Ҧb��m
    public int AllDamaged; //���ˮ`�`�ƭ�
    public string SkillNameText;

    //public int[] MoveToLocation = new int[2]; //�N�n���ʨ쪺��m

    //���A
    //public bool canMove; //��ܲ��ʦܭ���
    public void Start()
    {
        SettingValue();
    }
    void Update()
    {

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
        SkillNameText = EnemySetting[EnemyID].SkillName;
        ATKZone = EnemySetting[EnemyID].ATKZone;
        EnemyManager.GetInstance().EnemyATKZone = ATKZone;
    }
}
