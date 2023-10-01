using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerSkillManager : MonoBehaviour
{
    public static Dictionary<int, JobData> Jobs = new Dictionary<int, JobData>();
    private void Awake()
    {
        Jobs.Clear();
        Jobs.Add(0, new NoSkill());
        Jobs.Add(1, new SkillTest());
        for (int i = 0; i < Jobs.Count; i++)
        {
            Jobs[i].Setting();
        }
    }
}
