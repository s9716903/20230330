using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager:MonoBehaviour
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //��¦�ƭ�
    public int JobIndex;
    public int MaxHP; 
    public int StartHP; 
    public int Defense;

    //�ܰʼƭ�
    public int HP; //��q(��P��)
    public int MoveValue; //���ʭ�
    public int Stars; //�P�P��
    public int HealthValue; //�^��(�ĪG��P)�ƭ�
    public int PhysicATK; //���z�����ƭ�
    public int MagicATK; //�k�N�����ƭ�
    public int[] PlayerLocation; //���a�Ҧb��m
    public int NormalDrawAmount; //���a�q�`��P��
    public int DamageDropAmount; //���a�����P��
    public int[] EnemyLocation; //�ĤH�Ҧb��m

    public int AllDamaged; //���ˮ`�`�ƭ�
    public int AllMoveStatePoint; //���ʶ��q�`��
    public int[] MoveToLocation = new int[2]; //���a�N�n���ʨ쪺��m
    public int LimitedUse; //����a�X�P��

    //���a���A
    public bool isPlayer1; //�O�_�����a���H
    public bool canMove; //��ܲ��ʦܭ���
    public bool isReady; //���a�O�_�w�i�J�ǳƪ��A
    public bool isFirstATK = false; //�O�_����
    public bool isLimitedUsing = false; //�O�_����X�P
    public bool Skill1NoUse;
    public bool Skill2NoUse;
    public bool PassiveSkillNoUse;
    public void SettingValue()
    {
        JobIndex = 0;
        playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;
        MaxHP = GameManager.gameManager_instance.Jobs[JobIndex].TheMaxHP;
        Defense = GameManager.gameManager_instance.Jobs[JobIndex].TheDefense;
        StartHP = GameManager.gameManager_instance.Jobs[JobIndex].TheStartHP;
        Skill1NoUse = false;
        Skill2NoUse = false;
        PassiveSkillNoUse = false;
    }
    private void Update()
    {
        if (isPlayer1)
        {
            EnemyLocation = EnemyUIManager.GetInstance().Location;
        }
        else
        {
            EnemyLocation = PlayerUIManager.GetInstance().Location;
        }
        PassiveSkill();
    }
    public void UseSkill()
    {
        if ((Stars >= GameManager.gameManager_instance.Jobs[JobIndex].Skill1NeedStars) && (Stars < GameManager.gameManager_instance.Jobs[JobIndex].Skill2NeedStars))
        {
            GameManager.gameManager_instance.Jobs[JobIndex].Skill1();
            Debug.Log("Player Using Skill1");
        }
        else if ((Stars >= GameManager.gameManager_instance.Jobs[JobIndex].Skill2NeedStars) && (GameManager.gameManager_instance.Jobs[JobIndex].UnLockedSkill2 == 1))
        {
            GameManager.gameManager_instance.Jobs[JobIndex].Skill2();
        }
    }
    public void PassiveSkill()
    {
        if (GameManager.gameManager_instance.Jobs[JobIndex].UnLockedPassiveSkill == 1 && !PassiveSkillNoUse)
        {
            GameManager.gameManager_instance.Jobs[JobIndex].PassiveSkill();
        }
        else
        {
            return;
        }
    }
}
