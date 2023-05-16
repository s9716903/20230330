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
    // Start is called before the first frame update
    void Start()
    {
        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("firstdialogue", false);
        flowerSystem.SetupDialog();
        flowerSystem.SetScreenReference(1920, 1080);
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
                    flowerSystem.ReadTextFromResource("Stage1");
                    break;
                case 2:
                    flowerSystem.SetupButtonGroup();

                    flowerSystem.SetupButton("好的，前輩", () =>
                    {
                        SceneManager.LoadScene(2);
                        flowerSystem.RemoveButtonGroup();
                    });
                    flowerSystem.SetupButton("不，今天就不訓練了", () =>
                    {
                        SceneManager.LoadScene(3);
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
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                flowerSystem.Next();
            }
        }
    }
}
