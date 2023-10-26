using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DuelEndUI : MonoBehaviour
{
    public TextMeshProUGUI DuelEndText;
    public TextMeshProUGUI NextLevelButtonText;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerUIManager.GetInstance().PlayerData.HP <= 0)
        {
            DuelEndText.text = "You Lose";
            audioSource.clip = audioClips[1];
            NextLevelButtonText.text = "Try Again";
            audioSource.Play();
        }
        else
        {
            DuelEndText.text = "You Win";
            audioSource.clip = audioClips[0];
            NextLevelButtonText.text = "Next Level";
            audioSource.Play();
        }
    }
    public void NextLevelButton()
    {
        if (PlayerUIManager.GetInstance().PlayerData.HP <= 0)
        {
            LordingUI.NextScene = 5;
            SceneManager.LoadScene(2);
        }
        else
        {
            LordingUI.NextScene = 5;
            SceneManager.LoadScene(2);
        }
    }
    public void BackMenuButton()
    {
        LordingUI.NextScene = 1;
        SceneManager.LoadScene(2);
    }
}
