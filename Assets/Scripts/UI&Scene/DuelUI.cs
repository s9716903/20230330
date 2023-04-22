using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUI : MonoBehaviour
{
    public GameObject ReadyButton; //�ǳƧ����s
    public GameObject StateText; //�ثe���q��r
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!DuelStateManager.canInterect)
        {
            ReadyButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            ReadyButton.GetComponent<Button>().enabled = true;
        }
        StateText.GetComponent<TextMeshProUGUI>().text = DuelStateManager.duelStateType.ToString();
    }
    public void ReadyButtonPress() //�ǳƶs
    {
        /*switch (GameManager.duelStateType)
        {
            case GameState.DuelStateMode.Move:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Ready);
                break;
            case GameState.DuelStateMode.Attack:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Ready);
                break;
        }*/
    }
}
