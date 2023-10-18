using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //基礎數值
    public int EnemyID;
    public int HP;
    public int Defense;
    public int ATKValue;
    public List<int[]> ATKZone;

    //public int MoveValue;

    public int[] ThisLocation; //該所在位置
    public int[] PlayerLocation; //玩家所在位置
    public int AllDamaged; //受傷害總數值
    public string SkillNameText;

    //public int[] MoveToLocation = new int[2]; //將要移動到的位置

    //狀態
    //public bool canMove; //選擇移動至哪裡
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
