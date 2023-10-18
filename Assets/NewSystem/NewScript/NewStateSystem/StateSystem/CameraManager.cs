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
        EnemyCam1.GetComponent<CinemachineVirtualCamera>().Follow = FollowEnemy.transform;
        EnemyCam1.transform.position = FollowEnemy.transform.position;
        MainCamera.SetActive(false);
        EnemyCamera.SetActive(true);
    }
    public void LookScene()
    {
        LocationManager.showEnemyATKZone = true;
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
            DuelUIManager.showInformationText = true;
            FollowEnemy = enemys.EnemyPiece[i];
            enemys.EnemyPiece[i].GetComponent<EnemyData>().EnemyAction();
            DuelUIManager.Information = enemys.EnemyPiece[i].GetComponent<EnemyData>().SkillNameText;
            FollowAtEnemy();
            yield return new WaitForSeconds(1f);
            LookScene();
            for (int j = 0; j < 3; j++)
            {
                LocationManager.showEnemyATKZone = true;
                yield return new WaitForSeconds(0.25f);
                LocationManager.showEnemyATKZone = false;
                yield return new WaitForSeconds(0.25f);
            }
        }
        yield return 0;
        _player_data.canMove = true;
        enemys.isReady = true;
        _player_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        DuelUIManager.showInformationText = true;
        DuelUIManager.Information = "Choose Move";
        yield return 0;
    }
}
