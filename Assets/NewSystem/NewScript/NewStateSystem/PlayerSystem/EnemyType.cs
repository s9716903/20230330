using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyType
{
    //基礎數值
    public string Name;
    public string SkillName;
    public int HP; //血量
    public int Defense;
    public int MoveValue; //移動值
    public int ATKValue; //攻擊數值
    public List<int[]> ATKZone;
    public int[] ActionType;
    public abstract void Setting();
    public abstract void Action(int type);
    public abstract void Skill1();
    public abstract void Skill2();
}
public class Tomato : EnemyType
{
    public override void Setting()
    {
        Name = "Tomato";
        HP = Random.Range(2,9);
        Defense = Random.Range(0, 2);
        MoveValue = 0;
        ATKValue = Random.Range(2,4);
        ActionType = new int[] { 0, 1 };
    }
    public override void Action(int type)
    {
        if (type == 0)
        {
            Skill1();
        }
        if (type == 1)
        {
            Skill2();
        }
    }
    public override void Skill1()
    {
        SkillName = "Hypertension";
        var Zone = new List<int[]>();
        for (int i = 0; i < 3; i++)
        {
            Zone.Add(new int[2] {i,3});
        }
        ATKZone = Zone;
    }
    public override void Skill2()
    {
        SkillName = "Malicious Hurt";
        var Zone = new List<int[]>();
        var TargetZone = new List<int[]>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Zone.Add(new int[2] { i, j });
            }
        }
        for (int k = 0; k < 2; k++)
        {
            var Location = Random.Range(0, Zone.Count);
            TargetZone.Add(Zone[Location]);
            Zone.RemoveAt(Location);
        }
        ATKZone = TargetZone;
    }
}
