using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelUI : MonoBehaviour
{
    public GameObject ReadyButton; //�ǳƧ����s
    public GameObject StateText; //�ثe���q��r
    public GameObject ThisPlayer; //���a���H
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!DuelStateManager.canInterect || ThisPlayer.GetComponent<Player>().isReady == true)
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
        if (ThisPlayer.GetComponent<Player>().isReady == false)
        {
            ThisPlayer.GetComponent<Player>().canMove = !ThisPlayer.GetComponent<Player>().canMove;
        }
    }
}
