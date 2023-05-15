using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Location")]
    public GameObject Ground; //場地區
    public GameObject groundLocation; //玩家所站區域

    //玩家基礎數值
    public int MaxHp; //最大血量
    public int Hp; //目前血量
    public int Defense; //防禦值
    public int AttackResultHP; //計算傷害時用的HP

    //決鬥中可變動數值
    public  int MoveValue; //移動值
    public  int Stars; //星星值
    public  int PhysicDamage; //受物理攻擊數值
    public  int MagicDamage; //受法術攻擊數值
    public  int AllDamaged; //受到總傷害值
    public  int TargetLocation; //玩家所在位置

    public int NormalDrawAmount; //玩家結束時抽牌數
    public int HealthDrawAmount; //玩家回血抽牌數
    public int DamageDropAmount; //玩家扣血棄牌數
    public int MoveStatePoint; //移動階段點數

    public int MoveToLocation; //玩家將要移動到的位置


    public  bool canMove; //可以移動
    public  bool isReady; //玩家是否已進入準備狀態
    public  bool isFirstATK; //是否先攻
    // Start is called before the first frame update
    private void Start()
    {
        TargetLocation = 2;
        isReady = false;
        canMove = false;
        isFirstATK = true;

        MaxHp = GetComponent<JobManager>().thisJob.TheMaxHp;
        Defense = GetComponent<JobManager>().thisJob.TheDefense;
        Hp = GetComponent<JobManager>().thisJob.TheHP;
        MoveValue = 0;
        Stars = 0;
        MoveToLocation = TargetLocation;
        Debug.Log(MaxHp);
        Debug.Log(Hp);
        Debug.Log(Defense);
    }
    private void Update()
    {
        if (MoveToLocation < 0)
        {
            MoveToLocation = 0;
        }
        else if (MoveToLocation > 4)
        {
            MoveToLocation = 4;
        }
        //TargetLocation = MoveToLocation;
        MoveStatePoint = MoveValue + Stars;

        if (PhysicDamage < Defense)
        {
            AllDamaged = MagicDamage;
        }
        else
        {
            AllDamaged = MagicDamage + (PhysicDamage - Defense);
        }
        if (groundLocation.name == "A")
        {
            transform.position = new Vector3(Ground.GetComponent<Ground>().Alocation[TargetLocation].transform.position.x, 5, Ground.GetComponent<Ground>().Alocation[TargetLocation].transform.position.z);
        }
        else if (groundLocation.name == "B")
        {
            transform.position = new Vector3(Ground.GetComponent<Ground>().Blocation[TargetLocation].transform.position.x, 5, Ground.GetComponent<Ground>().Blocation[TargetLocation].transform.position.z);
        }
        Debug.Log("Target:" + TargetLocation);
        Debug.Log("Move:" + MoveToLocation);
    }
}
