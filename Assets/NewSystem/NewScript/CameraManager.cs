using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject EnemyCamera;
    public GameObject EnemyCam1;
    public GameObject FollowEnemy;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.SetActive(true);
        EnemyCamera.SetActive(false);
    }

    // Update is called once per frame
    public void FollowAtEnemy()
    {
        DuelUIManager.showInformationText = true;
        DuelUIManager.Information = "Attack";
        EnemyCam1.GetComponent<CinemachineVirtualCamera>().Follow = FollowEnemy.transform;
        MainCamera.SetActive(false);
        EnemyCamera.SetActive(true);
    }
    public void LookScene()
    {
        DuelUIManager.showInformationText = false;
        MainCamera.SetActive(true);
        EnemyCamera.SetActive(false);
    }
    public IEnumerator MoveStateLook()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var enemys = EnemyManager.GetInstance();
        for (int i = 0; i < enemys.EnemyPiece.Count; i++)
        {
            FollowEnemy = enemys.EnemyPiece[i];
            FollowAtEnemy();
            yield return new WaitForSeconds(1.5f);
            LookScene();
            yield return new WaitForSeconds(1.5f);
        }
        yield return 0;
        _player_data.canMove = true;
        enemys.isReady = true;
        _player_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        DuelUIManager.showInformationText = true;
        DuelUIManager.Information = "Moving";
        yield return 0;
    }
}
