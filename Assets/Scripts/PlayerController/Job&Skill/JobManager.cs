using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public string ChooseJob; //��ܨϥΪ�¾�~
    public SkillManager thisJob; //���b�ϥΤ���¾�~���U���ƭȡB�ޯ�(�{���X)
    public Dictionary<string, SkillManager> JobKey = new Dictionary<string, SkillManager>(); //¾�~�r��
    private void Awake()
    {
        JobKey.Clear();
        JobKey.Add("Warrior", new Warrior());
        ChooseJob = "Warrior";
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
