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
        if (GameManager.playerStateMode != GameState.PlayerStateMode.DoThingState)
        {
            ReadyButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            ReadyButton.GetComponent<Button>().enabled = true;
        }
        StateText.GetComponent<TextMeshProUGUI>().text = GameManager.duelStateMode.ToString();
    }
    public void ReadyButtonPress() //�ǳƶs
    {
        GameManager.playerStateMode = GameState.PlayerStateMode.ReadyState;
        Debug.Log(GameManager.duelStateMode); //Debug��r����
        Debug.Log(GameManager.playerStateMode);
    }
}
