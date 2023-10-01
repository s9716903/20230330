using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobData
{ 
    public int TheMaxHP; //�̤jHP
    public int TheStartHP; //��¦HP
    public int TheDefense; //���m��
    public int Skill1NeedStars; //�ޯ�1�ݭn�P�P��
    public int Skill2NeedStars; //�ޯ�2�ݭn�P�P��

    public int UnLockedSkill2 = 0; //�O�_����ޯ�2
    public int UnLockedPassiveSkill = 0; //�O�_����Q�ʧޯ�
    public bool isPlayer = false;
    public abstract void Setting();
    public abstract void Skill1(); //�D�ޯ�1
    public abstract void Skill2(); //�D�ޯ�2
    public abstract void PassiveSkill(); //�Q�ʧޯ�
}
public class NoSkill : JobData
{
    public override void Setting()
    {
        TheMaxHP = 6;
        TheStartHP = 5;
        TheDefense = 1;
        Skill1NeedStars = 0;
        Skill2NeedStars = 0;
        UnLockedSkill2 = 0;
        UnLockedPassiveSkill = 0;
    }
    public override void Skill1()
    {
        return;
    }
    public override void Skill2()
    {
        return;
    }
    public override void PassiveSkill()
    {
        return;
    }
}
public class SkillTest : JobData
{
    public override void Setting()
    {
        TheMaxHP = 9;
        TheStartHP = 7;
        TheDefense = 0;
        Skill1NeedStars = 3;
        Skill2NeedStars = 0;
        UnLockedSkill2 = 0;
        UnLockedPassiveSkill = 0;
    }
    public override void Skill1()
    {
        return;
    }
    public override void Skill2()
    {
        return;
    }
    public override void PassiveSkill()
    {
        return;
    }
}
