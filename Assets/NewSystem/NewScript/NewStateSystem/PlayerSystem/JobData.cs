using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobData
{
    //基礎數值
    public int HP; //血量
    public int MoveValue; //移動值
    public int ATKValue; //物理攻擊數值
    public int ATKZone;

    public int[] Upgrades;
    public int[] BuffValue; 
    public abstract void Setting();
    public abstract void Upgrade();
    public abstract void Skill();
}
public class Warrior : JobData
{
    public override void Setting()
    {
        HP = 20;
        MoveValue = 1;
        ATKValue = 2;
        ATKZone = 4;
        Upgrades = new int[3] { 0, 0, 0}; //(Defense,ATKCard,Skill)
        BuffValue = new int[3] { 1, 0, 1}; //(Defense,ATKValue,SkillUse)
    }
    public override void Upgrade()
    {
        if (Upgrades[0] == 1)
        {
            BuffValue[0] = 1;
        }
        if (Upgrades[1] == 1)
        {
            ATKValue = 4;
            ATKZone = 2;
        }
    }
    public override void Skill()
    {
        if (Upgrades[2] == 1)
        {
            BuffValue[1] = 1;
        }
    }

}
public class Magician : JobData
{
    public List<int[]> MagicATKZone;
    public override void Setting()
    {
        HP = 20;
        MoveValue = 1;
        ATKValue = 2;
        ATKZone = 4;
        BuffValue = new int[3] { 1, 0, 1 }; //(Defense,ATKValue,SkillUse)
    }
    public override void Upgrade()
    {

    }
    public override void Skill()
    {
        MagicATKZone.Clear();
        for (int i = 0; i < 2; i++)
        {
            var Random1 = Random.Range(0, 3);
            var Random2 = Random.Range(0, 8);
            var RandomInt = new int[2] { Random1, Random2 };
            MagicATKZone.Add(RandomInt);
        }    
    }
}