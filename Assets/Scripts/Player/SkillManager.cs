using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillManager : MonoBehaviour
{
    public int NeedStars1; //�ޯ�1�ݭn�P�P��
    public int NeedStars2; //�ޯ�2�ݭn�P�P��
    public bool UseSkill1; //�ϥΧޯ�1
    public bool UseSkill2; //�ϥΧޯ�2

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }
    public abstract void Setting();
    public abstract void Skill1();
    public abstract void Skill2();
    // Update is called once per frame
    void Update()
    {
        UseSkill();
    }
    public void UseSkill()
    {
        if (gameObject.GetComponent<Player>() == null)
        {
            if (NeedStars1 >= Player.Stars && Player.Stars < NeedStars2)
            {
                Skill1();
            }
            else if (Player.Stars > NeedStars2)
            {
                Skill2();
            }
        }
        if (gameObject.GetComponent<Enemy>() == null)
        {
            if (NeedStars1 >= Enemy.Stars && Enemy.Stars < NeedStars2)
            {
                Skill1();
            }
            else if (Enemy.Stars > NeedStars2)
            {
                Skill2();
            }
        }
    }
}
