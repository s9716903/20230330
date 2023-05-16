using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public void TryAgain()
    {
        LordingUI.NextScene = 4;
        SceneManager.LoadScene(1);
    }
    public void BackMenu()
    {
        LordingUI.NextScene = 0;
        SceneManager.LoadScene(1);
    }
}
