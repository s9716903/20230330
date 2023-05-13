using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateTimer : MonoBehaviour
{
    private TextMeshProUGUI timer_text; //�p�ɾ���r
    public static int startTime; //�_�l�ɶ�
    public static bool isStartTime; //�C�j�@�����@��
    public static bool stopStateTime; //�O�_����p�ɾ�
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
