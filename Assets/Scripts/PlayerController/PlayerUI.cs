using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject ThisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadyToChooseMove() //�ǳƶs
    {
        if (ThisPlayer.GetComponent<Player>().isReady == false)
        {
            ThisPlayer.GetComponent<Player>().canMove = !ThisPlayer.GetComponent<Player>().canMove;
        }
    }
}
