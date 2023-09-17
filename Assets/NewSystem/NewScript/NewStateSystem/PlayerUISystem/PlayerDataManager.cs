using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager
{
    public NewGameState.NewPlayerStateMode playerStateMode = NewGameState.NewPlayerStateMode.PlayerDeactivate;

    //��¦�ƭ�
    public int MaxHP; 
    public int StartHP; 
    public int Defense;

    //�ܰʼƭ�
    public int HP; //��q(��P��)
    public int MoveValue; //���ʭ�
    public int Stars; //�P�P��
    public int AllDamaged; //�`�ˮ`�ƭ�
    public int HealthValue; //�^��(�ĪG��P)�ƭ�
    public int PhysicATK; //���z�����ƭ�
    public int MagicATK; //�k�N�����ƭ�
    public int PlayerLocation; //���a�Ҧb��m
    public int NormalDrawAmount; //���a�q�`��P��
    public int DamageDropAmount; //���a�����P��

    public int MoveToLocation; //���a�N�n���ʨ쪺��m

    //���a���A
    public bool isPlayer1; //�O�_�����a���H
    public bool canMove; //��ܲ��ʦܭ���
    public bool isReady; //���a�O�_�w�i�J�ǳƪ��A
    public bool isFirstATK = false; //�O�_����
    public bool isLimitedUsing = false; //�O�_����X�P
    public int LimitedUse; //����a�X�P��
}
