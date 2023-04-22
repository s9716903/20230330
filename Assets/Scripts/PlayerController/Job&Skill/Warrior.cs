using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : SkillManager
{
    public static bool Skill1UnLock = false;
    public static bool Skill2UnLock = false;
    public static bool PassiveSkillUnLock = false;
    public static int Hp = 6;
    public static int Defense = 4;
    // Start is called before the first frame update
    public override void Setting()
    {
        canUseSkill1 = Skill2UnLock;
        canUseSkill2 = Skill2UnLock;
        canUsePassiveSkill = PassiveSkillUnLock;
        TheMaxHp = Hp;
        TheDefense = Defense;
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
    // Update is called once per frame
}
