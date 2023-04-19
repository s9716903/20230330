using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Location")]
    public GameObject groundLocation; //玩家所站區域


    public static int MaxHp; //最大血量
    public static int Hp; //目前血量
    public static int PhysicAtk; //受物理攻擊數值
    public static int MagicAtk; //受法術攻擊數值
    public static int MoveValue; //移動值
    public static int Defense; //防禦值
    public static int AllDamaged; //受到總傷害值
    public  int TargetLocation; //玩家所在位置
    public static int MoveToLocation; //玩家將要移動到的位置


    public static bool canMove = false; //可以移動
    // Start is called before the first frame update
    void Start()
    {
        MoveToLocation = TargetLocation;
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
