using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    private GameManager gmmanager;
    public GameObject HandCards;
    public TestButton(GameManager manager)
    {
        gmmanager = manager;
    }
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

        switch (GameManager.duelStateType)
        {
            case GameState.DuelStateMode.Draw:
                gmmanager.TransitionDuelState(GameState.DuelStateMode.Move);
                break;
            case GameState.DuelStateMode.Move:
                gmmanager.TransitionDuelState(GameState.DuelStateMode.Attack);
                break;
            case GameState.DuelStateMode.Attack:
                gmmanager.TransitionDuelState(GameState.DuelStateMode.End);
                break;
            case GameState.DuelStateMode.End:
                gmmanager.TransitionDuelState(GameState.DuelStateMode.Draw);
                break;
        }
        switch (GameManager.playerStateType)
        {
            case GameState.PlayerStateMode.DoThing:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.DoThing);
                break;
            case GameState.PlayerStateMode.NoDoThing:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.NoDoThing);
                break;
            case GameState.PlayerStateMode.Ready:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Ready);
                break;
            case GameState.PlayerStateMode.Damage:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Damage);
                break;
        }
        Debug.Log(gmmanager.currentduelState);
        Debug.Log(gmmanager.currentplayerState);
        Debug.Log(GameManager.duelStateType);
        Debug.Log(GameManager.playerStateType);
    }
}
