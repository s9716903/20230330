using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LordingUI : MonoBehaviour
{
    public static int NextScene; //�U�ӳ����s��
    private bool Cannextscene; //�O�_�i�i�J�U�ӳ���
    private AsyncOperation async; //���BŪ������

    public GameObject LordingBar;
    private float LordingTimer; //�p�ɾ�
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
