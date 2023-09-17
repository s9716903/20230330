using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewLocation : MonoBehaviour,IPointerClickHandler
{
    public bool canStop; //可以停留在此 
    public PlayerDataManager playerData;
    private Image Icon;
    private Button button;
    public int LocationGraph;
    // Start is called before the first frame update
    void Start()
    {
        canStop = false;
        Icon = GetComponent<Image>();
        button = GetComponent<Button>();
        Icon.enabled = false;
        button.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var PlayerMoveZoneUp = playerData.PlayerLocation + playerData.MoveValue;
        var PlayerMoveZoneDown = playerData.PlayerLocation - playerData.MoveValue;
        if (!PracticeLimtedSetting.LimitedOn)
        {
            if ((LocationGraph >= playerData.PlayerLocation) && (PlayerMoveZoneUp >= LocationGraph))
            {
                canStop = true;
            }
            else if ((LocationGraph <= playerData.PlayerLocation) && (PlayerMoveZoneDown <= LocationGraph))
            {
                canStop = true;
            }
            else
            {
                canStop = false;
            }
        }
        if (canStop == true && DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move && playerData.canMove && playerData.isReady == false)
        {

            Icon.enabled = true;
            button.enabled = true;
        }
        else
        {
            Icon.enabled = false;
            button.enabled = false;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        MovePointClick();
    }
    public void MovePointClick()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move && playerData.canMove && !playerData.isReady && canStop)
        {
            playerData.MoveToLocation = LocationGraph;
            playerData.isReady = true;
        }
    }
}
