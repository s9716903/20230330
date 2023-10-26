using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyType
{
    //基礎數值
    public string Name;
    public string ActionName;
    public int HP; //血量
    public int Defense;
    public int ATKValue; //攻擊數值
    public List<int[]> ActionZone;
    public int[] ActionType;
    public int ChooseActionType; //(0:ATK/1:move/2:obstacle)

    //特殊數值
    public int EnemySkillValue;
    public abstract void Setting();
    public abstract void Action(int type);
}
public class Tomato : EnemyType
{
    public override void Setting()
    {
        Name = "Tomato";
        HP = Random.Range(2,9);
        Defense = Random.Range(0, 2);
        ATKValue = Random.Range(2,4);
        ActionType = new int[] {0, 1 };
        EnemySkillValue = 0;
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
        /*if (type == 2)
        {
            Move();
        }*/
    }
    public void Skill1()
    {
        ActionName = "Hypertension";
        ChooseActionType = 0;
        var Zone = new List<int[]>();
        for (int i = 0; i < 3; i++)
        {
            Zone.Add(new int[2] {i,3});
        }
        ActionZone = Zone;
    }
    public void Skill2()
    {
        ActionName = "Malicious Hurt";
        ChooseActionType = 0;
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
        ActionZone = TargetZone;
    }
    /*public void Move()
    {
        ActionName = "Move";
        ChooseActionType = 1;
        var Zone = new List<int[]>();
        var MoveLocation = new int[2] {2,2};
        Zone.Add(MoveLocation);
        ActionZone = Zone;
    }*/
}
