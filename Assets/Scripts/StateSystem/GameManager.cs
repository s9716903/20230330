using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //決鬥狀態機
    public static GameState.DuelStateMode duelStateMode;

    public static GameManager instance = null;
    
    //是否為玩家可做事時間
    public static bool Playerturn;

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
            
    }
}
