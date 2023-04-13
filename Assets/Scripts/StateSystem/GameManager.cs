using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //狀態機
    public static GameState.DuelStateMode duelStateMode; //階段狀態機
    public static GameState.PlayerStateMode playerStateMode; //玩家狀態機

    //GameManager自身
    public static GameManager instance = null;

    //玩家資訊
    public static int Hp; //血量
    public static int PhysicAtk; //基礎物理攻擊數值
    public static int MagicAtk; //基礎法術攻擊數值
    public static int MoveValue; //基礎移動值
    public static int Defense; //防禦值

    // Start is called before the first frame update
    private void Awake()
    {
        playerStateMode = GameState.PlayerStateMode.NoDoThingState; //初始玩家狀態機設定
        duelStateMode = GameState.DuelStateMode.DrawState; //初始階段狀態機設定
        Debug.Log("Awake GamaManager");
        Debug.Log(duelStateMode); //Debug文字測試
        Debug.Log(playerStateMode);

        //場景中有相同物體時摧毀同物體
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
