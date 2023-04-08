using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //決鬥狀態機
    public static GameState.DuelStateMode duelStateMode;

    public static GameManager instance = null;
    
    public bool Playerturn = true;

    // Start is called before the first frame update
    private void Awake()
    {
        duelStateMode = GameState.DuelStateMode.DrawState; //初始階段設定
        Debug.Log("Awake GamaManager"); 

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
            /*switch (duelStateMode)
            {
                case GameState.DuelStateMode.DrawState:
                    break;
                case GameState.DuelStateMode.MoveState:
                    break;
                case GameState.DuelStateMode.MainState:
                    break;
                case GameState.DuelStateMode.EndState:
                    break;
            }*/
    }
}
