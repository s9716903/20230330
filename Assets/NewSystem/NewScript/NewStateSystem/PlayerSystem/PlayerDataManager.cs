using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //��¦�ƭ�
    public int JobIndex; //Job�N��
    public int HP; //��q
    public int StartCard; //��l��P��
    public int MoveValue; //���ʭ�
    public int PhysicATK; //���z�����ƭ�
    public int MagicATK; //�k�N�����ƭ�
    public int PhyATKZone;
    public int MagicATKZone;
    public List<int> BasicValue;

    //�ܰʼƭ�
    public int DrawAmount;
    public int Defense; //���m��
    public int[] PlayerLocation; //���a�Ҧb��m
    public int AllDamaged; //���ˮ`�`�ƭ�
    public int[] MoveToLocation = new int[2]; //���a�N�n���ʨ쪺��m
    public int[] BuffValue = new int[3];

    public int[] SkillIndex;


    //���a���A
    public bool canMove; //��ܲ��ʦܭ���
    public bool isReady; //���a�O�_�w�ǳƧ���
    public bool isFirstATK = false; //�O�_����
    public bool isLimitedUsing = false; //�O�_����X�P
    public void SettingValue()
    {
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        JobIndex = Random.Range(0, GameManager.gameManager_instance.Jobs.Count);
        HP = GameManager.gameManager_instance.Jobs[JobIndex].TheMaxHP;
        MoveValue = GameManager.gameManager_instance.Jobs[JobIndex].MoveValue;
        StartCard = GameManager.gameManager_instance.Jobs[JobIndex].TheStartCard;
        PhysicATK = GameManager.gameManager_instance.Jobs[JobIndex].PhysicATKValue;
        MagicATK = GameManager.gameManager_instance.Jobs[JobIndex].MagicATKValue;
        PhyATKZone = GameManager.gameManager_instance.Jobs[JobIndex].PhysicATKZone;
        MagicATKZone = GameManager.gameManager_instance.Jobs[JobIndex].MagicATKZone;
        BasicValue = new List<int> { Defense, PhysicATK, MagicATK };
        BuffValue = new int[] { 1, -1, 0 };
    }
    private void Update()
    {
        
    }
}