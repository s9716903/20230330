using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillManager : MonoBehaviour
{
    public int TheMaxHp; //最大HP
    public int TheHP; //基礎HP
    public int TheDefense; //防禦值
    public bool canUseSkill1; //可以使用技能1(是否解鎖)
    public bool canUseSkill2; //可以使用技能2(是否解鎖)
    public bool canUsePassiveSkill; //是否能使用被動技能(是否解鎖)

    public int NeedStars1; //技能1需要星星數
    public int NeedStars2; //技能2需要星星數
    public int playerStars; //玩家打出星星數

    public bool UseSkill1; //使用技能1
    public bool UseSkill2; //使用技能2
    public bool UsePassiveSkill; //適用被動技能

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }
    public abstract void Setting(); //初始設定
    public abstract void Skill1(); //主技能1
    public abstract void Skill2(); //主技能2
    public abstract void PassiveSkill(); //被動技能
    // Update is called once per frame
    public void UseSkill() //判定技能使用
    {
        if (playerStars >= NeedStars1 && playerStars < NeedStars2 && canUseSkill1)
        {
            UseSkill1 = true;
            UseSkill2 = false;
        }
        else if (playerStars >= NeedStars2 && canUseSkill2)
        {
            UseSkill1 = false;
            UseSkill2 = true;
        }
        else
        {
            UseSkill1 = false;
            UseSkill2 = false;
        }
    }
}
