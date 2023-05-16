using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeLimtedSetting : MonoBehaviour
{
    private HandCards handCards;
    public GameObject[] PlayerGroundLocation;

    public static int LimitedUseHowManyCard;
    public static bool LimitedOn = false;
    public static int EnemyLocation;
    public static int PracticeTurn;
    // Start is called before the first frame update
    void Start()
    {
        PracticeTurn = 0;
        handCards = GetComponent<HandCards>();
        LimitedUseHowManyCard = 0;
        LimitedOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LimitedOn == true)
        {
            handCards.PracticeLimited = true;
        }
        else
        {
            handCards.PracticeLimited = false;
        }
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            if (PracticeTurn == 0)
            {
                LimitedUseHowManyCard = 2;
                EnemyLocation = 1;
                for (int i = 0; i < PlayerGroundLocation.Length; i++)
                {
                    if (i == 3)
                    {
                        PlayerGroundLocation[i].GetComponent<Location>().canStop = true;
                    }
                    else
                    {
                        PlayerGroundLocation[i].GetComponent<Location>().canStop = false;
                    }
                }
            }
            else if (PracticeTurn == 1)
            {
                LimitedUseHowManyCard = 2;
                EnemyLocation = 2;
                for (int i = 0; i < PlayerGroundLocation.Length; i++)
                {
                    if (i == 2)
                    {
                        PlayerGroundLocation[i].GetComponent<Location>().canStop = true;
                    }
                    else
                    {
                        PlayerGroundLocation[i].GetComponent<Location>().canStop = false;
                    }
                }
            }
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack)
        {
            if (PracticeTurn == 0)
            {
                LimitedUseHowManyCard = 3;
            }
            else
            {
                LimitedOn = false;
            }
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult && PracticeTurn == 1)
        {
            LimitedOn = true;
        }
        else if (DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
        {
            LimitedUseHowManyCard = 1;
        }
    }
}
