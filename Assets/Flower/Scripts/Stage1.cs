using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    FlowerSystem flowerSystem;
    private bool isGameEnd = false;
    private int stage1 = 0;
    private GameObject Flower;
    // Start is called before the first frame update
    void Start()
    {
        flowerSystem = FlowerManager.Instance.CreateFlowerSystem("Stage1", false);
        flowerSystem.SetupDialog();
        Flower = GameObject.Find("FlowerSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (flowerSystem.isCompleted && !isGameEnd)
        {
            switch (stage1)
            {
                case 0:
                    flowerSystem.ReadTextFromResource("Stage1");
                    break;
                case 1:
                    LordingUI.NextScene = 0;
                    SceneManager.LoadScene(1);
                    isGameEnd = true;
                    break;
            }
            stage1++;
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
