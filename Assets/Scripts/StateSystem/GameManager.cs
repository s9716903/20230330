using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���A��
    public static GameState.DuelStateMode duelStateMode; //���q���A��
    public static GameState.PlayerStateMode playerStateMode; //���a���A��

    //GameManager�ۨ�
    public static GameManager instance = null;

    //���a��T
    public static int Hp; //��q
    public static int PhysicAtk; //��¦���z�����ƭ�
    public static int MagicAtk; //��¦�k�N�����ƭ�
    public static int MoveValue; //��¦���ʭ�
    public static int Defense; //���m��

    // Start is called before the first frame update
    private void Awake()
    {
        playerStateMode = GameState.PlayerStateMode.NoDoThingState; //��l���a���A���]�w
        duelStateMode = GameState.DuelStateMode.DrawState; //��l���q���A���]�w
        Debug.Log("Awake GamaManager");
        Debug.Log(duelStateMode); //Debug��r����
        Debug.Log(playerStateMode);

        //���������ۦP����ɺR���P����
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
