using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Location")]
    public GameObject groundLocation; //���a�ү��ϰ�

    //���a��¦�ƭ�
    public static int MaxHp; //�̤j��q
    public static int Hp; //�ثe��q
    public static int Defense; //���m��

    //�M�����i�ܰʼƭ�
    public static int MoveValue; //���ʭ�
    public static int Stars; //�P�P��
    public static int PhysicDamage; //�����z�����ƭ�
    public static int MagicDamage; //���k�N�����ƭ�
    public static int AllDamaged; //�����`�ˮ`��
    public int TargetLocation; //���a�Ҧb��m

    public static int MoveToLocation; //���a�N�n���ʨ쪺��m


    public static bool canMove = false; //�i�H����
    public static bool isReady = false; //���a�O�_�w�i�J�ǳƪ��A
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        isReady = false;
        MaxHp = GetComponent<JobManager>().thisJob.TheMaxHp;
        Defense = GetComponent<JobManager>().thisJob.TheDefense;
        MoveValue = 0;
        Stars = 0;
        MoveToLocation = TargetLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetLocation >= groundLocation.GetComponent<Ground>().Locations)
        {
            TargetLocation = groundLocation.GetComponent<Ground>().Locations - 1;
        }
        if (TargetLocation < 0)
        {
            TargetLocation = 0;
        }
        transform.position = new Vector3(groundLocation.GetComponent<Ground>().enemylocation[TargetLocation].transform.position.x, 5, groundLocation.GetComponent<Ground>().enemylocation[TargetLocation].transform.position.z);
    }
}
