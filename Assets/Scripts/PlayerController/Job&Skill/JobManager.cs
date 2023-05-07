using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public int ChooseJob; //��ܨϥΪ�¾�~
    public SkillManager thisJob; //���b�ϥΤ���¾�~���U���ƭȡB�ޯ�(�{���X)
    public Dictionary<int, SkillManager> JobKey = new Dictionary<int, SkillManager>(); //¾�~�r��
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
