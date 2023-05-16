using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeNoJob : SkillManager
{
    public static bool Skill1UnLock = false;
    public static bool Skill2UnLock = false;
    public static bool PassiveSkillUnLock = false;
    // Start is called before the first frame update
    public override void Setting()
    {
        canUseSkill1 = Skill1UnLock;
        canUseSkill2 = Skill2UnLock;
        canUsePassiveSkill = PassiveSkillUnLock;
        TheMaxHp = 6;
        TheHP = 5;
        TheDefense = 0;
    }
    public override void Skill1()
    {

    }
    public override void Skill2()
    {

    }
    public override void PassiveSkill()
    {

    }
}
