using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CoordinatesLocation : MonoBehaviour,IPointerDownHandler
{
    public int[] Coordinates = new int[2];

    public bool alreadyStop; //已經有東西停留
    public bool isPlayerLocation; //屬於玩家的移動區
    public bool canStop; //此格可停留 

    private Button button;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        canStop = false;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        image.enabled = false;
        button.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
        {
            if (!alreadyStop)
            {
                canStop = true;
            }
            else
            {
                canStop = false;
            }

            if (canStop && PlayerUIManager.GetInstance().PlayerData.canMove && !PlayerUIManager.GetInstance().PlayerData.isReady && isPlayerLocation && PlayerUIManager.GetInstance().PlayerData.MoveValue >= (Mathf.Abs(PlayerUIManager.GetInstance().PlayerData.PlayerLocation[0] - Coordinates[0]) + Mathf.Abs(PlayerUIManager.GetInstance().PlayerData.PlayerLocation[1] - Coordinates[1])))
            {
                button.enabled = true;
                image.enabled = true;
            }
            else
            {
                button.enabled = false;
                image.enabled = false;
            }
        }
        else
        {
            canStop = false;
        }

        if (gameObject.transform.childCount != 0)
        {
            alreadyStop = true;
        }
        else
        {
            alreadyStop = false;
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        MovePointClick();
    }
    public void MovePointClick()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move && canStop)
        {
            PlayerUIManager.GetInstance().PlayerData.MoveToLocation = Coordinates;
            PlayerUIManager.GetInstance().PlayerData.isReady = true;
        }
    }
}
