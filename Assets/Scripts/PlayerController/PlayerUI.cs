using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public bool readyToDuel = false;

    public GameObject ThisPlayer;
    public GameObject PlayerHandZone;
    public GameObject PlayerDeck; 
    public GameObject ThisTrashCardZone;
    public GameObject PlayerSkill;

    // Start is called before the first frame update
    void Start()
    {
        ThisPlayer.SetActive(false);
        PlayerHandZone.SetActive(false);
        PlayerDeck.SetActive(false);
        ThisTrashCardZone.SetActive(false);
        PlayerSkill.SetActive(false);
        readyToDuel = false;
        StartCoroutine(StartDuel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartDuel()
    {
        yield return new WaitForSeconds(1);
        PlayerDeck.SetActive(true);
        ThisTrashCardZone.SetActive(true);
        PlayerSkill.SetActive(true);
        yield return new WaitForSeconds(1);
        PlayerHandZone.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ThisPlayer.SetActive(true);
        readyToDuel = true;
        yield return 0;
    }

    public void ReadyToChooseMove() //MoveState Deside Choose Move Where
    {
        if (ThisPlayer.GetComponent<Player>().isReady == false)
        {
            ThisPlayer.GetComponent<Player>().canMove = !ThisPlayer.GetComponent<Player>().canMove;
        }
    }
}
