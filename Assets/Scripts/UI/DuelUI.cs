using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUI : MonoBehaviour
{
    public GameObject ReadyButton; //準備完成鈕
    public GameObject StateText; //目前階段文字
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Playerturn == false)
        {
            ReadyButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            ReadyButton.GetComponent<Button>().enabled = true;
        }

        switch (GameManager.duelStateMode)
        {
            case GameState.DuelStateMode.DrawState:
                GameManager.Playerturn = false;
                break;
            case GameState.DuelStateMode.MoveState:
                GameManager.Playerturn = true;
                break;
            case GameState.DuelStateMode.MainState:
                GameManager.Playerturn = true;
                break;
            case GameState.DuelStateMode.EndState:
                GameManager.Playerturn = false;
                break;
        }
        StateText.GetComponent<TextMeshProUGUI>().text = GameManager.duelStateMode.ToString();
    }
    public void ReadyButtonPress() //準備鈕
    {
        Debug.Log(GameManager.duelStateMode); //Debug文字測試
        Debug.Log(GameManager.Playerturn); //Debug測試
    }
}
