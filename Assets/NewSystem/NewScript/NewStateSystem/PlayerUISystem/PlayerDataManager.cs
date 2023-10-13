using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //基礎數值
    public int JobIndex; //Job代號
    public int HP; //血量
    public int StartCard; //初始手牌數
    public int MoveValue; //移動值
    public int PhysicATK; //物理攻擊數值
    public int MagicATK; //法術攻擊數值

    //變動數值
    public int DrawAmount;
    public int Defense; //防禦值
    public int[] PlayerLocation; //玩家所在位置
    public int AllDamaged; //受傷害總數值
    public int[] MoveToLocation = new int[2]; //玩家將要移動到的位置
    public int LimitedInt; //限制數

    public int[] SkillIndex;


    //玩家狀態
    public bool canMove; //選擇移動至哪裡
    public bool isReady; //玩家是否已準備完成
    public bool isFirstATK = false; //是否先攻
    public bool isLimitedUsing = false; //是否限制出牌
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        JobIndex = Random.Range(0, GameManager.gameManager_instance.Jobs.Count);
        HP = GameManager.gameManager_instance.Jobs[JobIndex].TheMaxHP;
        MoveValue = GameManager.gameManager_instance.Jobs[JobIndex].MoveValue;
        StartCard = GameManager.gameManager_instance.Jobs[JobIndex].TheStartCard;
        PhysicATK = GameManager.gameManager_instance.Jobs[JobIndex].PhysicATKValue;
        MagicATK = GameManager.gameManager_instance.Jobs[JobIndex].MagicATKValue;
    }
    private void Update()
    {
        
    }
}
