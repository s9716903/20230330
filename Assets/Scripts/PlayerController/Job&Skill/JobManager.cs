using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public int ChooseJob; //選擇使用的職業
    public SkillManager thisJob; //正在使用中的職業的各項數值、技能(程式碼)
    public Dictionary<int, SkillManager> JobKey = new Dictionary<int, SkillManager>(); //職業字典
    private void Awake()
    {
        JobKey.Clear();
        JobKey.Add(0, new NoJob());
        JobKey.Add(1, new Warrior());
        thisJob = JobKey[ChooseJob];
        thisJob.Setting();
    }
    void Start()
    {
        Debug.Log(ChooseJob);
    }
    private void Update()
    {
        thisJob.UseSkill();
    }
}
