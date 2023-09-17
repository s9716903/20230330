using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //基礎數值
    public int MaxHP; 
    public int StartHP; 
    public int Defense;

    //變動數值
    public int HP; //血量(手牌數)
    public int MoveValue; //移動值
    public int Stars; //星星值
    public int AllDamaged; //總傷害數值
    public int HealthValue; //回血(效果抽牌)數值
    public int PhysicATK; //物理攻擊數值
    public int MagicATK; //法術攻擊數值
    public int PlayerLocation; //玩家所在位置
    public int NormalDrawAmount; //玩家通常抽牌數
    public int DamageDropAmount; //玩家扣血棄牌數

    public int MoveToLocation; //玩家將要移動到的位置

    //玩家狀態
    public bool isPlayer1; //是否為玩家本人
    public bool canMove; //選擇移動至哪裡
    public bool isReady; //玩家是否已進入準備狀態
    public bool isFirstATK = false; //是否先攻
    public bool isLimitedUsing = false; //是否限制出牌
    public int LimitedUse; //限制玩家出牌數
}
