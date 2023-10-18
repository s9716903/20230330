using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CoordinatesLocation : MonoBehaviour,IPointerDownHandler
{
    public int[] Coordinates = new int[2];

    public bool alreadyStop; //�w�g���F�谱�d
    public bool isPlayerLocation; //�ݩ󪱮a�����ʰ�
    
    private bool canStop; //����i���d 
    private bool isPlayerATKZone; //�b�����d��

    private Button button;
    private Image image;
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
            if (LocationManager.showEnemyATKZone)
            {
                var atkzone = EnemyManager.GetInstance().EnemyATKZone.Any(arr => arr.GetType() == Coordinates.GetType() && arr.SequenceEqual(Coordinates));
                if (atkzone)
                {
                    image.color = new Color(255, 0, 0, 255);
                    image.enabled = true;
                }
                else
                {
                    image.enabled = false;
                }
            }
            else
            {
                if (canStop && PlayerUIManager.GetInstance().PlayerData.canMove && !PlayerUIManager.GetInstance().PlayerData.isReady && isPlayerLocation && PlayerUIManager.GetInstance().PlayerData.MoveValue >= (Mathf.Abs(PlayerUIManager.GetInstance().PlayerData.PlayerLocation[0] - Coordinates[0]) + Mathf.Abs(PlayerUIManager.GetInstance().PlayerData.PlayerLocation[1] - Coordinates[1])))
                {
                    image.color = new Color(0, 235, 255, 255);
                    button.enabled = true;
                    image.enabled = true;
                }
                else
                {
                    button.enabled = false;
                    image.enabled = false;
                }
            }

            if (!alreadyStop)
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
            var playerPhysicATKZone = PlayerUIManager.GetInstance().PlayerData.ATKZone;
            var playerLocation = PlayerUIManager.GetInstance().PlayerData.PlayerLocation;
            if (LocationManager.showPlayerATKZone)
            {
                if (playerLocation[0] == Coordinates[0] && (Coordinates[1] - playerLocation[1]) <= playerPhysicATKZone && (Coordinates[1] - playerLocation[1]) >= 0 && (playerLocation[1] != Coordinates[1]))
                {
                    isPlayerATKZone = true;
                }
                else
                {
                    isPlayerATKZone = false;
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
