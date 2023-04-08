using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�M�����A��
    public static GameState.DuelStateMode duelStateMode;

    public static GameManager instance = null;
    
    public bool Playerturn = true;

    // Start is called before the first frame update
    private void Awake()
    {
        duelStateMode = GameState.DuelStateMode.DrawState; //��l���q�]�w
        Debug.Log("Awake GamaManager"); 

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
