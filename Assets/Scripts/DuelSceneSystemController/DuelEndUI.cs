using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuelEndUI : MonoBehaviour
{
    public TextMeshProUGUI DuelEndText;
    private void OnEnable()
    {
        if (DuelUIController.player1lose)
        {
            DuelEndText.text = "You Lose";
        }
        else if (DuelUIController.player2lose)
        {
            DuelEndText.text = "You Win";
        }
    }
}
