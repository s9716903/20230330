using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using UnityEngine.SceneManagement;

public class PracticeDialodue : MonoBehaviour
{
    FlowerSystem flowerSystem;
    private bool isGameEnd = false;
    public static int practiceduel;
    public static bool DialogueStart;

    private GameObject Flower;
    // Start is called before the first frame update
    void Start()
    {
        DialogueStart = false;
        practiceduel = 0;
        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("DuelPractice", false);
        Flower = GameObject.Find("FlowerSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (flowerSystem.isCompleted && !isGameEnd)
        {
            switch (practiceduel)
            {
                case 0:
                    StateTimer.pauseStateTime = false;
                    break;
                case 1:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        DialogueStart = false;
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice1");
                        practiceduel = 0;
                    }
                    break;
                case 2:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        flowerSystem.ReadTextFromResource("Practice2");
                        practiceduel = 10;
                    }
                    break;
                case 3:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice3");
                        practiceduel = 10;
                    }
                    break;
                case 4:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice4");
                        practiceduel = 10;
                    }
                    break;
                case 5:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        DialogueStart = false;
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice5");
                        practiceduel = 0;
                    }
                    break;
                case 6:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        flowerSystem.ReadTextFromResource("Practice6");
                        practiceduel = 10;
                    }
                    break;
                case 7:
                    StateTimer.pauseStateTime = true;
                    if (DialogueStart == true)
                    {
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice7");
                        practiceduel = 10;
                    }
                    break;
                case 8:
                    if (DialogueStart == true)
                    {
                        DialogueStart = false;
                        flowerSystem.SetupDialog();
                        flowerSystem.ReadTextFromResource("Practice8");
                        practiceduel = 9;
                    }
                    break;
                case 9:
                    practiceduel = 0;
                    LordingUI.NextScene = 5;
                    SceneManager.LoadScene(1);
                    isGameEnd = true;
                    break;
                case 10:
                    if (DialogueStart == true)
                    {
                        DialogueStart = false;
                        flowerSystem.RemoveDialog();
                    }
                    break;
            }
        }
        if (!isGameEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flowerSystem.Next();
            }
        }
        if (isGameEnd)
        {
            flowerSystem.RemoveDialog();
            Destroy(Flower);
        }
    }
}
