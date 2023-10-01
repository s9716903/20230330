using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinatesLocation : MonoBehaviour
{
    public int[] Coordinates = new int[2];
    
    public bool canStop; //可以停留在此 
    public bool chooseStop; //最終停留在此

    public PlayerDataManager playerData;
    private Button button;
    private int PlayerDistance;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        canStop = false;
        chooseStop = false;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        image.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move)
        {
            if (playerData.canMove && !playerData.isReady && !PracticeLimtedSetting.LimitedOn && playerData.MoveValue >= (Mathf.Abs(playerData.PlayerLocation[0] - Coordinates[0]) + Mathf.Abs(playerData.PlayerLocation[1] - Coordinates[1])))
            {
                canStop = true;
            }
            else
            {
                canStop = false;
            }
        }
        else
        {
            chooseStop = false;
            canStop = false;
        }

        if (canStop)
        {
            if (playerData.isPlayer1)
            {
                button.enabled = true;
                image.enabled = true;
            }
        }
        else
        {
            if (chooseStop)
            {
                button.enabled = false;
                image.enabled = true;
            }
            else
            {
                button.enabled = false;
                image.enabled = false;
                chooseStop = false;
            }
        }
    }
    public void MovePointClick()
    {
        if (DuelBattleManager.duelStateMode == NewGameState.NewDuelStateMode.Move && canStop)
        {
            playerData.MoveToLocation = Coordinates;
            playerData.isReady = true;
            if (playerData.isPlayer1)
            {
                chooseStop = true;
            }
        }
    }
}
