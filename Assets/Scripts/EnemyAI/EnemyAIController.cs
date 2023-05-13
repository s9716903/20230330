using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : MonoBehaviour
{
    public GameObject EnemyHandCard;
    public GameObject Player;
    public GameObject Enemy;

    private bool AIReady;

    // Start is called before the first frame update
    void Start()
    {
        AIReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move && DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
        {
            if ((StateTimer.startTime == 30 || Player.GetComponent<Player>().isReady) && !AIReady)
            {
                AIReady = true;
                for (int i = 0; i < EnemyHandCard.transform.childCount;i++)
                {
                    if (EnemyHandCard.transform.GetChild(i).GetComponent<CardManager>().canUseThisCard)
                    {
                        EnemyHandCard.transform.GetChild(i).GetComponent<CardManager>().ChooseUseCard();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.Attack && DuelStateManager.playerStateType == GameState.PlayerStateMode.DoThing)
        {

        }
        else if (DuelStateManager.duelStateType == GameState.DuelStateMode.AttackResult && DuelStateManager.playerStateType == GameState.PlayerStateMode.Damage)
        {

        }*/
    }
}
