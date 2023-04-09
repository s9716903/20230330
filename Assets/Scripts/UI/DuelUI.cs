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
    public void ReadyButtonPress() //�ǳƶs
    {
        Debug.Log(GameManager.duelStateMode); //Debug��r����
        Debug.Log(GameManager.Playerturn); //Debug����
    }
}
