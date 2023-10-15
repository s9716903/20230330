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
    public bool isPlayerATKZone; //在玩家攻擊範圍內

    private Button button;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerATKZone = false;
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
                image.color = new Color(0, 235, 255,255);
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

        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Attack)
        {
            var playerPhysicATKZone = PlayerUIManager.GetInstance().PlayerData.PhyATKZone;
            var playerMagicATKZone = PlayerUIManager.GetInstance().PlayerData.MagicATKZone;
            var playerLocation = PlayerUIManager.GetInstance().PlayerData.PlayerLocation;
            if (LocationManager.showPlayerATKZone)
            {
                if (LocationManager.ATKType == 1)
                {
                    if (playerLocation[1] == Coordinates[1] && (Mathf.Abs((Coordinates[0] - playerLocation[0])) <= playerPhysicATKZone) && (playerLocation[0] != Coordinates[0]))
                    {
                        isPlayerATKZone = true;
                    }
                    else if (playerLocation[0] == Coordinates[0] && (Mathf.Abs((Coordinates[1] - playerLocation[1])) <= playerPhysicATKZone) && (playerLocation[1] != Coordinates[1]))
                    {
                        isPlayerATKZone = true;
                    }
                    else
                    {
                        isPlayerATKZone = false;
                    }
                }
                if (LocationManager.ATKType == 2)
                {
                    if (playerLocation[1] == Coordinates[1] && ((6 - playerMagicATKZone) <= Coordinates[0]) && (playerLocation[0] < Coordinates[0]))
                    {
                        isPlayerATKZone = true;
                    }
                    else if (playerLocation[1] == Coordinates[1] && ((-1 + playerMagicATKZone) >= Coordinates[0]) && (playerLocation[0] > Coordinates[0]))
                    {
                        isPlayerATKZone = true;
                    }
                    else if (playerLocation[0] == Coordinates[0] && ((4 - playerMagicATKZone) <= Coordinates[1]) && (playerLocation[1] < Coordinates[1]))
                    {
                        isPlayerATKZone = true;
                    }
                    else if (playerLocation[0] == Coordinates[0] && ((-1 + playerMagicATKZone) >= Coordinates[1]) && (playerLocation[1] > Coordinates[1]))
                    {
                        isPlayerATKZone = true;
                    }
                    else
                    {
                        isPlayerATKZone = false;
                    }
                }
            }
            else
            {
                isPlayerATKZone = false;
            }

            if (isPlayerATKZone)
            {
                image.color = new Color(255, 0, 0, 255);
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
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
