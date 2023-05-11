using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Location : MonoBehaviour,IPointerClickHandler
{
    public bool canStop; //可以停留在此 

    private Image Icon;

    public GameObject ThisPlayer;
    private GameObject Player;
    private void Start()
    {
        canStop = false;
        Player = GameObject.Find("Player");
        Icon = gameObject.GetComponent<Image>();
        Icon.enabled = false;
    }
    private void Update()
    {
        var PlayerMoveZoneUp = ThisPlayer.GetComponent<Player>().TargetLocation + ThisPlayer.GetComponent<Player>().MoveValue;
        var PlayerMoveZoneDown = ThisPlayer.GetComponent<Player>().TargetLocation - ThisPlayer.GetComponent<Player>().MoveValue;
      

        if ((gameObject.transform.GetSiblingIndex() >= ThisPlayer.GetComponent<Player>().TargetLocation) && (PlayerMoveZoneUp >= gameObject.transform.GetSiblingIndex()))
        {
            canStop = true;
        }
        else if ((gameObject.transform.GetSiblingIndex() <= ThisPlayer.GetComponent<Player>().TargetLocation) && (PlayerMoveZoneDown <= gameObject.transform.GetSiblingIndex()))
        {
            canStop = true;
        }
        else
        {
            canStop = false;
        }

        if (ThisPlayer == Player && canStop == true && DuelStateManager.duelStateType == GameState.DuelStateMode.Move && ThisPlayer.GetComponent<Player>().canMove && ThisPlayer.GetComponent<Player>().isReady == false)
        {
            Icon.enabled = true;
        }
        else
        {
            Icon.enabled = false;
        }

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (ThisPlayer == Player && DuelStateManager.duelStateType == GameState.DuelStateMode.Move)
        {
            MovePointClick();
        }
    }

    public void MovePointClick()
    {
        if (DuelStateManager.duelStateType == GameState.DuelStateMode.Move && ThisPlayer.GetComponent<Player>().canMove && ThisPlayer.GetComponent<Player>().isReady == false && canStop)
        {
            ThisPlayer.GetComponent<Player>().MoveToLocation = gameObject.transform.GetSiblingIndex();
            ThisPlayer.GetComponent<Player>().isReady = true;
        }
    }
}
