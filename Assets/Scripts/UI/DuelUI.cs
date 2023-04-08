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
        Debug.Log(GameManager.duelStateMode); //Debug文字測試
    }
    public void ReadyButtonPress()
    {
        GameManager.duelStateMode++; //階段前進
        StateText.GetComponent<TextMeshProUGUI>().text = GameManager.duelStateMode.ToString();
    }
}
