using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject PlayerCamera;
    public GameObject EnemyCamera;

    public static bool ChangeCamera;
    public static int CameraInt;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera.SetActive(true);
        PlayerCamera.SetActive(false);
        EnemyCamera.SetActive(false);
        CameraInt = 0;
        ChangeCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCamera)
        {
            ChangeCamera = false;
            if (CameraInt == 0)
            {
                MainCamera.SetActive(true);
                PlayerCamera.SetActive(false);
                EnemyCamera.SetActive(false);
            }
            else if (CameraInt == 1)
            {
                MainCamera.SetActive(true);
                PlayerCamera.SetActive(true);
                EnemyCamera.SetActive(false);
            }
            else if (CameraInt == 2)
            {
                MainCamera.SetActive(true);
                PlayerCamera.SetActive(false);
                EnemyCamera.SetActive(true);
            }
        }    
    }
}
