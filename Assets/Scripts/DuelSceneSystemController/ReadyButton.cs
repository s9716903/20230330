using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    public static int LimitedUsing; //�@���ԭ���X�P��
    public static int PracticeLimited; //�оǮɭ���X�P��
    // Start is called before the first frame update
    void Start()
    {
        LimitedUsing = 0;
        PracticeLimited = 0;
    }
    // Update is called once per frame
    void Update()
    {
        /*if (!DuelStateManager.canInterect || ThisPlayer.GetComponent<Player>().canMove == true)
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
        }*/
    }
}
