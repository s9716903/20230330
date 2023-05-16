using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using UnityEngine.SceneManagement;

public class firstDialogue : MonoBehaviour
{
    FlowerSystem flowerSystem;
    private bool isGameEnd = false;
    private int stage = 0;
    public GameObject school;
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("firstdialogue", false);
        flowerSystem.SetupDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if(flowerSystem.isCompleted && !isGameEnd)
        {
            switch(stage)
            {
                case 0:
                    flowerSystem.ReadTextFromResource("Open");
                    break;
                case 1:
                    school.SetActive(true);
                    black.SetActive(false);
                    flowerSystem.ReadTextFromResource("Stage0");
                    break;
                case 2:
                    flowerSystem.SetupButtonGroup();

                    flowerSystem.SetupButton("好的，前輩", () =>
                    {
                        LordingUI.NextScene = 3;
                        SceneManager.LoadScene(1);
                        flowerSystem.RemoveButtonGroup();
                    });
                    flowerSystem.SetupButton("不，今天就不訓練了", () =>
                    {
                        LordingUI.NextScene = 5;
                        SceneManager.LoadScene(1);
                        flowerSystem.RemoveButtonGroup();
                    });
                    break;
                case 3:
                    isGameEnd = true;
                    break;
            }
            stage++;
        }
        if (!isGameEnd)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                flowerSystem.Next();
            }
        }
    }
}
