using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyButton : MonoBehaviour
{
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
           gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        StateText.GetComponent<TextMeshProUGUI>().text = DuelStateManager.duelStateType.ToString();
    }
}
