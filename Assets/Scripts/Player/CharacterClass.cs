using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : SkillManager
{
    public int Hp; //HP
    public int Defense; //���m��
    public int Skill1Stars; //�ޯ�1�ݭn�P�P��
    public int Skill2Stars; //�ޯ�2�ݭn�P�P��
    // Start is called before the first frame update
    public override void Setting()
    {
        NeedStars1 = Skill1Stars;
        NeedStars2 = Skill2Stars;
    }
    public override void Skill1()
    {
        
    }
    public override void Skill2()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
