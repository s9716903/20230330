using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameObject HandCards;
    public void ChangeState()
    {
        for (int i = 0; i < HandCards.transform.childCount; i++)
        {
            if (HandCards.transform.GetChild(i).GetComponent<CardManager>().isUseThisCard)
            {

            }
            HandCards.transform.GetChild(i).gameObject.SetActive(true);
            HandCards.transform.GetChild(i).GetComponent<CardManager>().isUseThisCard = false;
        }
        GameManager.duelStateMode++; //階段前進
        switch (GameManager.duelStateMode)
        {
            case GameState.DuelStateMode.DrawState:
                GameManager.playerStateMode = GameState.PlayerStateMode.NoDoThingState;
                break;
            case GameState.DuelStateMode.MoveState:
                GameManager.playerStateMode = GameState.PlayerStateMode.DoThingState;
                break;
            case GameState.DuelStateMode.MainState:
                GameManager.playerStateMode = GameState.PlayerStateMode.DoThingState;
                break;
            case GameState.DuelStateMode.EndState:
                GameManager.playerStateMode = GameState.PlayerStateMode.NoDoThingState;
                break;
        }
        Debug.Log(GameManager.duelStateMode); //Debug文字測試
        Debug.Log(GameManager.playerStateMode);
    }
}
