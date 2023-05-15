using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    public GameObject ThisPlayer; //玩家本人
    public static int LimitedUsing; //一般對戰限制出牌數
    public static int PracticeLimited; //新手關對戰限制出牌數
    // Start is called before the first frame update
    void Start()
    {
        LimitedUsing = 0;
        PracticeLimited = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!DuelStateManager.canInterect || ThisPlayer.GetComponent<Player>().canMove == true)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            if (PracticeLimtedSetting.LimitedOn)
            {
                if (PracticeLimited != PracticeLimtedSetting.LimitedUseHowManyCard)
                {
                    gameObject.GetComponent<Button>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<Button>().enabled = true;
                }
            }
            else
            {
                if (LimitedUsing == ThisPlayer.GetComponent<Player>().Hp)
                {
                    gameObject.GetComponent<Button>().enabled = false;
                }
                else
                { 
                    gameObject.GetComponent<Button>().enabled = true;
                }               
            }
        }
    }
}
