using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobData
{ 
    public int TheMaxHP; //最大HP
    public int TheStartHP; //基礎HP
    public int TheDefense; //防禦值
    public int Skill1NeedStars; //技能1需要星星數
    public int Skill2NeedStars; //技能2需要星星數

    public int UnLockedSkill2 = 0; //是否解鎖技能2
    public int UnLockedPassiveSkill = 0; //是否解鎖被動技能
    public bool isPlayer = false;
    public abstract void Setting();
    public abstract void Skill1(); //主技能1
    public abstract void Skill2(); //主技能2
    public abstract void PassiveSkill(); //被動技能
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
