using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewReadyButton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var Player = PlayerUIManager.GetInstance().PlayerData;
        if (Player.playerStateMode == NewGameState.NewPlayerStateMode.PlayerActivate)
        {
            if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move && Player.canMove)
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
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
}
