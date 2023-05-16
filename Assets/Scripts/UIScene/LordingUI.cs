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
    private float LordingTimer; //計時器
    // Start is called before the first frame update
    void Start()
    {
        Cannextscene = false;
        LordingTimer = 0;
        StartCoroutine(LordScene());
    }

    // Update is called once per frame
    void Update()
    {
        LordingTimer += Time.deltaTime;
        if (LordingTimer >= 3)
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
