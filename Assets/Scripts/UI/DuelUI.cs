using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUI : MonoBehaviour
{
    public GameObject ReadyButton; //準備完成鈕
    public GameObject StateText; //目前階段文字
    private GameManager manager; //負責引用底下流程的
    // Start is called before the first frame update
    void Start()
    {
        
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
        //GameManager.playerStateMode = GameState.PlayerStateMode.ReadyState;
        //Debug.Log(GameManager.duelStateMode); //Debug文字測試
        //Debug.Log(GameManager.playerStateMode);
    }
}
