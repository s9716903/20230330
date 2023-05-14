using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter0Setting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StateTimer.pauseStateTime = !StateTimer.pauseStateTime;
        var ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        var TheEnemy = GameObject.Find("Enemy").GetComponent<Player>();

        ThePlayer.TargetLocation = 0;
        TheEnemy.TargetLocation = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
