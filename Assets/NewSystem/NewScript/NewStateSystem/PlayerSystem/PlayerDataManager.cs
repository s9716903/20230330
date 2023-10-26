using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //��¦�ƭ�
    public int JobIndex; //Job�N��
    public int HP; 
    public int MoveValue; 
    public int ATKValue; 
    public int ATKZone;

    //�ܰʼƭ�
    public int DrawAmount;
    public int SkillUse;
    public int Defense; //���m��
    public List<int> AllDamaged; //�Ҩ��ˮ`��
    public int[] BuffValue;
    public int[] PlayerLocation = new int[2]; //���a�Ҧb��m
    public int[] MoveToLocation = new int[2]; //���a�N�n���ʨ쪺��m
    public List<int> PlayerAction; //���a�欰

    //���a���A
    public bool canMove; //��ܲ��ʦܭ���
    public bool isReady; //���a�O�_�w�ǳƧ���
    public bool isFirstATK = false; //�O�_����
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        if (PlayerAction != null)
        {
            PlayerAction.Clear();
        }
        JobIndex = Random.Range(0, GameManager.gameManager_instance.Jobs.Count);
        GameManager.gameManager_instance.Jobs[JobIndex].Setting();
        HP = GameManager.gameManager_instance.Jobs[JobIndex].HP;
        MoveValue = GameManager.gameManager_instance.Jobs[JobIndex].MoveValue;
        ATKValue = GameManager.gameManager_instance.Jobs[JobIndex].ATKValue;
        ATKZone = GameManager.gameManager_instance.Jobs[JobIndex].ATKZone;
        Defense = 0;
        //BasicValue = new int[] { Defense, ATKValue,ATKZone};
        BuffValue = GameManager.gameManager_instance.Jobs[JobIndex].BuffValue;
    }
    public void Skill()
    {
        GameManager.gameManager_instance.Jobs[JobIndex].Skill();
    }
}
