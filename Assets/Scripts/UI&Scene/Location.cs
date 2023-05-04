using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool canStop = false; //可以停留在此 
    public GameObject ThisPlayer;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void MovePointClick()
    {
        if (ThisPlayer == Player && DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            if (ThisPlayer.GetComponent<Player>().canMove && ThisPlayer.GetComponent<Player>().isReady == false)
            {
                ThisPlayer.GetComponent<Player>().MoveToLocation = gameObject.transform.GetSiblingIndex();
                ThisPlayer.GetComponent<Player>().isReady = true;
            }
        }
    }
}
