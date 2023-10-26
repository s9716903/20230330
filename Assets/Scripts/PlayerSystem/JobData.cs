using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JobData
{
    //基礎數值
    public string SkillName;
    public int HP; 
    public int MoveValue; 
    public int ATKValue; 
    public int ATKZone;
    public int[] BuffValue;
    //特殊數值
    public int SkillValue;
    public List<int[]> SkillZones;
    public abstract void Setting();
    public abstract void Upgrade();
    public abstract void Skill(int Location1);
}
public class Warrior : JobData
{
    public override void Setting()
    {
        SkillName = "Skill";
        HP = 20;
        MoveValue = 3;
        ATKValue = 8;
        ATKZone = 4;
        BuffValue = new int[3] { 0, 0, 0 }; //(Defense,ATKValue,SkillValue)
        Upgrade();
    }
    public override void Upgrade()
    {
        if (GameManager.gameManager_instance.PlayerUpgrade[0] == 1)
        {
            BuffValue[0] = 1;
        }
        if (GameManager.gameManager_instance.PlayerUpgrade[1] == 1)
        {
            BuffValue[1] = 2;
            ATKZone = 2;
        }
        if (GameManager.gameManager_instance.PlayerUpgrade[2] == 1)
        {
            BuffValue[2] = 1;
        }
    }
    public override void Skill(int Location1)
    {
        SkillValue = 2 + BuffValue[2];
        var SkillZone = new List<int[]>();
        for (int i = 0; i < 8; i++)
        {
            SkillZone.Add(new int[2] {Location1,i});
        }
        for (int j = 0; j < Location1 + 1; j++)
        {
            SkillZone.RemoveAt(0);
        }
        SkillZones = SkillZone;
    }

}
public class Magician : JobData
{
    public override void Setting()
    {
        SkillName = "Skill2";
        HP = 5;
        MoveValue = 2;
        ATKValue = 2;
        ATKZone = 4;
        BuffValue = new int[3] { 0, 0, 0 }; //(Defense,ATKValue,SkillUse)
        Upgrade();
    }
    public override void Upgrade()
    {
        if (GameManager.gameManager_instance.PlayerUpgrade[0] == 1)
        {
            BuffValue[0] = 0;
        }
        if (GameManager.gameManager_instance.PlayerUpgrade[1] == 1)
        {
            BuffValue[1] = -1;
        }
        if (GameManager.gameManager_instance.PlayerUpgrade[2] == 1)
        {
            BuffValue[2] = 1;
        }
    }
    public override void Skill(int Location1)
    {
        for (int i = 0; i < 2; i++)
        {
            var Random1 = Random.Range(0, 3);
            var Random2 = Random.Range(0, 8);
            var RandomInt = new int[2] { Random1, Random2 };
        }    
    }
}