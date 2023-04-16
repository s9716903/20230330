using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUI : MonoBehaviour
{
    public GameObject ReadyButton; //準備完成鈕
    public GameObject StateText; //目前階段文字
    public GameManager gmmanager; //GameManager
    // Start is called before the first frame update
    void Start()
    {
        gmmanager = GameObject.Find("GameManager").GetComponent<GameManager>(); //找尋場景中的GameManager
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.canInterect)
        {
            ReadyButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            ReadyButton.GetComponent<Button>().enabled = true;
        }
        StateText.GetComponent<TextMeshProUGUI>().text = GameManager.duelStateType.ToString();
    }
    public void ReadyButtonPress() //準備鈕
    {
        switch (GameManager.duelStateType)
        {
            case GameState.DuelStateMode.Move:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Ready);
                break;
            case GameState.DuelStateMode.Attack:
                gmmanager.TransitionPlayerState(GameState.PlayerStateMode.Ready);
                break;
        }
    }
}
