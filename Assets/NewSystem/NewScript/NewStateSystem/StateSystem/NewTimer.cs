using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewTimer : MonoBehaviour
{
    private TextMeshProUGUI TimerText; //計時器文字
    public static int StartTime; //起始時間
    public static bool TimeDelay; //每隔一秒執行一次
    public static bool StopTime; //是否停止計時器
    public static bool PauseTime;
    // Start is called before the first frame update
    void Start()
    {
        PauseTime = false;
        TimerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        TimerText.text = StartTime.ToString();
        if (TimeDelay == true && !PauseTime)
        {
            StartCoroutine("StartToTime");
            TimeDelay = false;
        }
        if (StopTime)
        {
            StopCoroutine("StartToTime");
        }
    }
    public IEnumerator StartToTime()
    {
        yield return new WaitForSeconds(1);
        StartTime--;
        if (StartTime <= 0)
        {
            StopTime = true;
        }
        else
        {
            TimeDelay = true;
        }
    }
}
