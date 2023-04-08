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
        Debug.Log(GameManager.duelStateMode); //Debug��r����
    }
    public void ReadyButtonPress()
    {
        GameManager.duelStateMode++; //���q�e�i
        StateText.GetComponent<TextMeshProUGUI>().text = GameManager.duelStateMode.ToString();
    }
}
