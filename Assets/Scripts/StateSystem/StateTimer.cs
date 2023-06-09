using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateTimer : MonoBehaviour
{
    private TextMeshProUGUI timer_text; //計時器文字
    public static int startTime; //起始時間
    public static bool isStartTime; //每隔一秒執行一次
    public static bool stopStateTime; //是否停止計時器
    public static bool pauseStateTime;
    // Start is called before the first frame update
    void Start()
    {
        pauseStateTime = false;
        timer_text = GetComponent<TextMeshProUGUI>();
    }
    public IEnumerator StartToTime()
    {
        yield return new WaitForSeconds(1);
        startTime--;
        if (startTime <= 0)
        {
            stopStateTime = true;
        }
        else
        {
            isStartTime = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer_text.text = startTime.ToString();
        if (isStartTime == true && !pauseStateTime)
        {
            StartCoroutine("StartToTime");
            isStartTime = false;
        }
        if (stopStateTime)
        {
            StopCoroutine("StartToTime");
        }
    }
}
