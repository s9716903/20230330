using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //基礎數值
    public int JobIndex; //Job代號
    public int HP; 
    public int MoveValue; 
    public int ATKValue; 
    public int ATKZone;

    //變動數值
    public int DrawAmount;
    public int SkillUse;
    public int Defense; //防禦值
    public List<int> AllDamaged; //所受傷害值
    public int[] BuffValue;
    public int[] PlayerLocation = new int[2]; //玩家所在位置
    public int[] MoveToLocation = new int[2]; //玩家將要移動到的位置
    public List<int> PlayerAction; //玩家行為

    //玩家狀態
    public bool canMove; //選擇移動至哪裡
    public bool isReady; //玩家是否已準備完成
    public bool isFirstATK = false; //是否先攻
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        if (PlayerAction != null)
        {
            PlayerAction.Clear();
        }
        JobIndex = Random.Range(0, GameManager.gameManager_instance.Jobs.Count);
        GameManager.gameManager_instance.Jobs[JobIndex].Setting();
        HP = GameManager.gameManager_instance.Jobs[JobIndex].HP;
        MoveValue = GameManager.gameManager_instance.Jobs[JobIndex].MoveValue;
        ATKValue = GameManager.gameManager_instance.Jobs[JobIndex].ATKValue;
        ATKZone = GameManager.gameManager_instance.Jobs[JobIndex].ATKZone;
        Defense = 0;
        //BasicValue = new int[] { Defense, ATKValue,ATKZone};
        BuffValue = GameManager.gameManager_instance.Jobs[JobIndex].BuffValue;
    }
    public void Skill()
    {
        GameManager.gameManager_instance.Jobs[JobIndex].Skill();
    }
}
