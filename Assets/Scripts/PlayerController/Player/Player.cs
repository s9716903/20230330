using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Location")]
    public GameObject groundLocation; //玩家所站區域

    //玩家基礎數值
    public static int MaxHp; //最大血量
    public static int Hp; //目前血量
    public static int Defense; //防禦值

    //決鬥中可變動數值
    public static int MoveValue; //移動值
    public static int Stars; //星星值
    public static int PhysicDamage; //受物理攻擊數值
    public static int MagicDamage; //受法術攻擊數值
    public static int AllDamaged; //受到總傷害值
    public int TargetLocation; //玩家所在位置

    public static int DrawAmoumt; //玩家結束時抽牌數

    public static int MoveToLocation; //玩家將要移動到的位置


    public static bool canMove; //可以移動
    public static bool isReady; //玩家是否已進入準備狀態
    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        canMove = false;
        MaxHp = GetComponent<JobManager>().thisJob.TheMaxHp;
        Defense = GetComponent<JobManager>().thisJob.TheDefense;
        MoveValue = 0;
        Stars = 0;
        MoveToLocation = TargetLocation;
        Debug.Log(MaxHp);
        Debug.Log(Defense);
    }
    private void Update()
    {
        TargetLocation = MoveToLocation;
        if (TargetLocation >= groundLocation.GetComponent<Ground>().Locations)
        {
            TargetLocation = groundLocation.GetComponent<Ground>().Locations - 1;
        }
        if (TargetLocation < 0)
        {
            TargetLocation = 0;
        }
        transform.position = new Vector3(groundLocation.GetComponent<Ground>().playerlocation[TargetLocation].transform.position.x, 5, groundLocation.GetComponent<Ground>().playerlocation[TargetLocation].transform.position.z);
        Debug.Log(TargetLocation);
        Debug.Log(MoveToLocation);
    }
}
