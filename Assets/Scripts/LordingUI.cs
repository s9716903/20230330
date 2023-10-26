using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LordingUI : MonoBehaviour
{
    public static int NextScene; //下個場景編號
    private bool Cannextscene; //是否可進入下個場景
    private AsyncOperation async; //異步讀取場景

    public GameObject LordingBar;
    private float LordingTimer; //計時器
    // Start is called before the first frame update
    void Start()
    {

        LordingBar.SetActive(false);
        Cannextscene = false;
        LordingTimer = 0;
        StartCoroutine(LordScene());
    }

    // Update is called once per frame
    void Update()
    {
        LordingTimer += Time.deltaTime;
        if (LordingTimer >= 2 && LordingTimer < 5)
        {
            LordingBar.SetActive(true);
        }
        if (LordingTimer >= 5)
        {
            LordingBar.SetActive(false);
        }
        if (LordingTimer >= 6)
        {
            Cannextscene = true;
        }
    }
    public IEnumerator LordScene()
    {
        async = SceneManager.LoadSceneAsync(NextScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (Cannextscene == true)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
