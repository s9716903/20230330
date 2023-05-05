using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    public GameObject ThisPlayer; //ª±®a¥»¤H
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!DuelStateManager.canInterect || ThisPlayer.GetComponent<Player>().canMove == true)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
    }
}
