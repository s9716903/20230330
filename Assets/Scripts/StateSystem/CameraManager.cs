using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

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
        StartCoroutine(EnterDuelScene());
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
            if (enemys.EnemyPiece[i].GetComponent<EnemyData>().ChooseActionType == 0)
            {
                DuelUIManager.Information = "Enemy Attack";
            }
            else if (enemys.EnemyPiece[i].GetComponent<EnemyData>().ChooseActionType == 1)
            {
                DuelUIManager.Information = "Enemy Move";
            }
            FollowAtEnemy();
            yield return new WaitForSeconds(1f);
            LookScene();
            if (enemys.EnemyPiece[i].GetComponent<EnemyData>().ChooseActionType == 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    LocationManager.showEnemyATKZone = true;
                    yield return new WaitForSeconds(0.25f);
                    LocationManager.showEnemyATKZone = false;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            /*else if (enemys.EnemyPiece[i].GetComponent<EnemyData>().ChooseActionType == 1)
            {
                enemys.MovePiece();
            }*/
        }
        yield return 0;
        _player_data.canMove = true;
        enemys.isReady = true;
        _player_data.playerStateMode = NewGameState.NewPlayerStateMode.PlayerActivate;
        DuelUIManager.showInformationText = true;
        DuelUIManager.Information = "Choose Move";
        yield return 0;
    }
    public IEnumerator EnemyAttackResult()
    {
        var _player_data = PlayerUIManager.GetInstance().PlayerData;
        var enemys = EnemyManager.GetInstance();
        for (int i = 0; i < enemys.EnemyPiece.Count; i++)
        {
            var atkhit = enemys.EnemyPiece[i].GetComponent<EnemyData>().ActionZone.Any(arr => arr.GetType() == _player_data.PlayerLocation.GetType() && arr.SequenceEqual(_player_data.PlayerLocation));
            if (enemys.EnemyPiece[i].GetComponent<EnemyData>().ChooseActionType == 0)
            {
                DuelUIManager.Information = enemys.EnemyPiece[i].GetComponent<EnemyData>().ActionNameText;
                FollowEnemy = enemys.EnemyPiece[i];
                FollowAtEnemy();
                yield return new WaitForSeconds(1f);
                LookScene();
                DuelUIManager.showInformationText = true;
                if (atkhit)
                {
                    _player_data.HP -= enemys.EnemyPiece[i].GetComponent<EnemyData>().ATKValue;
                    yield return new WaitForSeconds(1f);
                    if (_player_data.HP <= 0)
                    {
                        DuelUIManager.Information = "Player Die";
                        yield return new WaitForSeconds(1f);
                        DuelUIManager.BattleEnd = true;
                        yield break;
                    }
                }
                else
                {
                    DuelUIManager.Information = "ATK No Hit";
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        yield return 0;
    }
    public IEnumerator EnterDuelScene()
    {
        var maincamera = MainCamera.GetComponent<Camera>();
        for (; maincamera.orthographicSize < 540;maincamera.orthographicSize++)
        {
            MainCamera.GetComponent<Camera>().orthographicSize += 1;
            yield return new WaitForSeconds(0.002f);
        }
        yield return null;
    }
}
