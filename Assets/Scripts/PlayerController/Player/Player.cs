using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Location")]
    public GameObject Ground; //���a��
    public GameObject groundLocation; //���a�ү��ϰ�

    //���a��¦�ƭ�
    public  int MaxHp; //�̤j��q
    public  int Hp; //�ثe��q
    public  int Defense; //���m��

    //�M�����i�ܰʼƭ�
    public  int MoveValue; //���ʭ�
    public  int Stars; //�P�P��
    public  int PhysicDamage; //�����z�����ƭ�
    public  int MagicDamage; //���k�N�����ƭ�
    public  int AllDamaged; //�����`�ˮ`��
    public  int TargetLocation; //���a�Ҧb��m

    public int NormalDrawAmount; //���a�����ɩ�P��
    public int HealthDrawAmount; //���a�^���P��

    public int MoveToLocation; //���a�N�n���ʨ쪺��m


    public  bool canMove; //�i�H����
    public  bool isReady; //���a�O�_�w�i�J�ǳƪ��A
    // Start is called before the first frame update
    private void Start()
    {
        TargetLocation = 2;
        isReady = false;
        canMove = false;
        MaxHp = GetComponent<JobManager>().thisJob.TheMaxHp;
        Defense = GetComponent<JobManager>().thisJob.TheDefense;
        MoveValue = 0;
        Stars = 0;
        MoveToLocation = TargetLocation;
        Debug.Log(MaxHp);
        Debug.Log(Defense);
    }
    private void Update()
    {
        /*if (TargetLocation >= groundLocation.GetComponent<Ground>().Locations)
        {
            TargetLocation = groundLocation.GetComponent<Ground>().Locations - 1;
        }
        if (TargetLocation < 0)
        {
            TargetLocation = 0;
        }*/
        //TargetLocation = MoveToLocation;
        if (groundLocation.name == "A")
        {
            transform.position = new Vector3(Ground.GetComponent<Ground>().Alocation[TargetLocation].transform.position.x, 5, Ground.GetComponent<Ground>().Alocation[TargetLocation].transform.position.z);
        }
        else if (groundLocation.name == "B")
        {
            transform.position = new Vector3(Ground.GetComponent<Ground>().Blocation[TargetLocation].transform.position.x, 5, Ground.GetComponent<Ground>().Blocation[TargetLocation].transform.position.z);
        }
        Debug.Log("Target:" + TargetLocation);
        Debug.Log("Move:" + MoveToLocation);
    }
}
