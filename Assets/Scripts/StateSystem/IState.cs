using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //IState���Ѫ��B��Ҧ�
{
    void OnEnter(); //���A�i�J�ɰ���
    void OnUpdate(); //���A���򤤰���
    void OnExit(); //���A�����ɰ���
}
