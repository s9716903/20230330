using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //基礎數值
    public int JobIndex;
    public int MaxHP; 
    public int StartHP; 
    public int Defense;

    //變動數值
    public int HP; //血量(手牌數)
    public int MoveValue; //移動值
    public int Stars; //星星值
    public int HealthValue; //回血(效果抽牌)數值
    public int PhysicATK; //物理攻擊數值
    public int MagicATK; //法術攻擊數值
    public int[] PlayerLocation; //玩家所在位置
    public int NormalDrawAmount; //玩家通常抽牌數
    public int DamageDropAmount; //玩家扣血棄牌數
    public int[] EnemyLocation; //敵人所在位置

    public int AllDamaged; //受傷害總數值
    public int AllMoveStatePoint; //移動階段總值
    public int[] MoveToLocation = new int[2]; //玩家將要移動到的位置
    public int LimitedUse; //限制玩家出牌數

    //玩家狀態
    public bool isPlayer1; //是否為玩家本人
    public bool canMove; //選擇移動至哪裡
    public bool isReady; //玩家是否已進入準備狀態
    public bool isFirstATK = false; //是否先攻
    public bool isLimitedUsing = false; //是否限制出牌
    public bool Skill1NoUse;
    public bool Skill2NoUse;
    public bool PassiveSkillNoUse;
    public void SettingValue()
    {
        JobIndex = 0;
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        MaxHP = GameManager.gameManager_instance.Jobs[JobIndex].TheMaxHP;
        Defense = GameManager.gameManager_instance.Jobs[JobIndex].TheDefense;
        StartHP = GameManager.gameManager_instance.Jobs[JobIndex].TheStartHP;
        Skill1NoUse = false;
        Skill2NoUse = false;
        PassiveSkillNoUse = false;
    }
    private void Update()
    {
        if (isPlayer1)
        {
            EnemyLocation = EnemyUIManager.GetInstance().Location;
        }
        else
        {
            EnemyLocation = PlayerUIManager.GetInstance().Location;
        }
        PassiveSkill();
    }
    public void UseSkill()
    {
        if ((Stars >= GameManager.gameManager_instance.Jobs[JobIndex].Skill1NeedStars) && (Stars < GameManager.gameManager_instance.Jobs[JobIndex].Skill2NeedStars))
        {
            GameManager.gameManager_instance.Jobs[JobIndex].Skill1();
            Debug.Log("Player Using Skill1");
        }
        else if ((Stars >= GameManager.gameManager_instance.Jobs[JobIndex].Skill2NeedStars) && (GameManager.gameManager_instance.Jobs[JobIndex].UnLockedSkill2 == 1))
        {
            GameManager.gameManager_instance.Jobs[JobIndex].Skill2();
        }
    }
    public void PassiveSkill()
    {
        if (GameManager.gameManager_instance.Jobs[JobIndex].UnLockedPassiveSkill == 1 && !PassiveSkillNoUse)
        {
            GameManager.gameManager_instance.Jobs[JobIndex].PassiveSkill();
        }
        else
        {
            return;
        }
    }
}
