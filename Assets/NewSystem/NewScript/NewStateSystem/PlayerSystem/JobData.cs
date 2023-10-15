using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobData
{ 
    public int TheMaxHP; //�̤jHP
    public int TheStartCard; //��l��P��
    public int MoveValue; //���ʭ�
    public int PhysicATKValue; //���z���
    public int MagicATKValue; //�k�N���
    public int PhysicATKZone;
    public int MagicATKZone;

    //public int UnLockedSkill2 = 0; //�O�_����ޯ�2
    //public int UnLockedPassiveSkill = 0; //�O�_����Q�ʧޯ�
    //public bool isPlayer = false;
    public abstract void Setting();
}
public class NoSkill : JobData
{
    public override void Setting()
    {
        TheMaxHP = 6;
        TheStartCard = 5;
        MoveValue = 1;
        PhysicATKValue = 1;
        MagicATKValue = 1;
        PhysicATKZone = 1;
        MagicATKZone = 1;

        //UnLockedSkill2 = 0;
        //UnLockedPassiveSkill = 0;
    }
}
public class SkillTest : JobData
{
    public override void Setting()
    {
        TheMaxHP = 9;
        TheStartCard = 8;
        MoveValue = 2;
        PhysicATKValue = 2;
        MagicATKValue = 1;
        PhysicATKZone = 2;
        MagicATKZone = 1;

        //UnLockedSkill2 = 0;
        //UnLockedPassiveSkill = 0;
    }
}
public class SkillTest2 : JobData
{
    public override void Setting()
    {
        TheMaxHP = 12;
        TheStartCard = 2;
        MoveValue = 3;
        PhysicATKValue = 3;
        MagicATKValue = 2;
        PhysicATKZone = 3;
        MagicATKZone = 2;
        //UnLockedSkill2 = 0;
        //UnLockedPassiveSkill = 0;
    }
}
