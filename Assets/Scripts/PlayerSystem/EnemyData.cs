using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyData : MonoBehaviour
{
    //基礎數值
    public int EnemyID;
    public int HP;
    public int Defense;
    public int ATKValue;
    public List<int[]> ActionZone;

    //public int[] ThisLocation; //該所在位置
    public int[] EnemyLocation; //敵人所在位置
    public int AllDamaged; //受傷害總數值
    public string ActionNameText;
    public int ChooseActionType;

    //特殊數值
    public int EnemySkillValue;
    //public int[] MoveToLocation = new int[2]; //將要移動到的位置
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
