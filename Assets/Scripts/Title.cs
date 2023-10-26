using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioSource audioSource;
    public TextMeshProUGUI PressAnyKeyText;
    private bool pressAnyKey;
    private AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pressAnyKey = true;
        InvokeRepeating(nameof(ShowText),0.5f,0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && pressAnyKey)
        {
            audioSource.Play();
            EnterMenu();
        }
    }
    private void ShowText()
    {
        if (PressAnyKeyText.text == "")
        {
            PressAnyKeyText.text = "Press Any Button";
        }
        else
        {
            PressAnyKeyText.text = "";
        }
    }
    private void EnterMenu()
    {
        StartCoroutine(LordScene());
    }
    public IEnumerator LordScene()
    {
        pressAnyKey = false;
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5f);
        while (!async.isDone)
        {
            async.allowSceneActivation = true;
            yield return null;
        }
    }
}
