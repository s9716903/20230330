using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //��¦�ƭ�
    public int JobIndex; //Job�N��
    public string SkillName;
    public int HP; 
    public int MoveValue; 
    public int ATKValue; 
    public int ATKZone;
    public int[] BuffValue;

    //�S��ƭ�
    public int SkillValue;
    public List<int[]> SkillZones;

    //�H�M���ܤƼƭ�
    public int DrawAmount;
    public int Defense; //���m��
    public int[] PlayerLocation = new int[2]; //���a�Ҧb��m
    public int[] MoveToLocation = new int[2]; //���a�N�n���ʨ쪺��m
    public List<int[]> ATKHitZone;
    public int[] PlayerAction; //���a�欰(���m�P/�����P/�ޯ�P)

    //���a���A
    public bool canMove; //��ܲ��ʦܭ���
    public bool isReady; //���a�O�_�w�ǳƧ���
    public bool isFirstATK = false; //�O�_����
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        PlayerAction = new int[3] { 0, 0, 0 };
        JobIndex = GameManager.gameManager_instance.PlayerJob;
        GameManager.gameManager_instance.Jobs[JobIndex].Setting();
        HP = GameManager.gameManager_instance.Jobs[JobIndex].HP;
        MoveValue = GameManager.gameManager_instance.Jobs[JobIndex].MoveValue;
        ATKValue = GameManager.gameManager_instance.Jobs[JobIndex].ATKValue;
        ATKZone = GameManager.gameManager_instance.Jobs[JobIndex].ATKZone;
        Defense = 0;
        BuffValue = GameManager.gameManager_instance.Jobs[JobIndex].BuffValue;
        SkillName = GameManager.gameManager_instance.Jobs[JobIndex].SkillName;
    }
    public void Skill()
    {
        GameManager.gameManager_instance.Jobs[JobIndex].Skill(PlayerLocation[0]);
        SkillValue = GameManager.gameManager_instance.Jobs[JobIndex].SkillValue;
        SkillZones = GameManager.gameManager_instance.Jobs[JobIndex].SkillZones;
    }
}
