using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DuelEndUI : MonoBehaviour
{
    public TextMeshProUGUI DuelEndText;

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        /*if (DuelUIController.player1lose)
        {
            DuelEndText.text = "You Lose";
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if (DuelUIController.player2lose)
        {
            DuelEndText.text = "You Win";
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }*/
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
