using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int MaxHp; //�̤j��q
    public static int Hp; //��q
    public static int PhysicAtk; //��¦���z�����ƭ�
    public static int MagicAtk; //��¦�k�N�����ƭ�
    public static int MoveValue; //��¦���ʭ�
    public static int Defense; //���m��
    public static int Damaged; //����ˮ`��
    public static int AllDamaged; //���쪺�`�ˮ`��
    public  int TargetLocation; //���a�Ҧb��m
    public static int MoveToLocation; //���a�N�n���ʨ쪺��m
    public GameObject groundLocation; //���a�ү��ϰ�
    public static bool canMove = false; //�i�H����
    // Start is called before the first frame update
    void Start()
    {
        MoveToLocation = TargetLocation;
    }
    private void Update()
    {
        TargetLocation = MoveToLocation;
        if (TargetLocation >= groundLocation.GetComponent<Ground>().Locations)
        {
            TargetLocation = groundLocation.GetComponent<Ground>().Locations - 1;
        }
        if (TargetLocation < 0)
        {
            TargetLocation = 0;
        }
        transform.position = new Vector3(groundLocation.GetComponent<Ground>().playerlocation[TargetLocation].transform.position.x, 5, groundLocation.GetComponent<Ground>().playerlocation[TargetLocation].transform.position.z);
        Debug.Log(TargetLocation);
        Debug.Log(MoveToLocation);
    }
}
