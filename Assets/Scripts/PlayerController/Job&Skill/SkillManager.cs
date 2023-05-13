using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillManager : MonoBehaviour
{
    public int TheMaxHp; //�̤jHP
    public int TheHP; //��¦HP
    public int TheDefense; //���m��
    public bool canUseSkill1; //�i�H�ϥΧޯ�1(�O�_����)
    public bool canUseSkill2; //�i�H�ϥΧޯ�2(�O�_����)
    public bool canUsePassiveSkill; //�O�_��ϥγQ�ʧޯ�(�O�_����)

    public int NeedStars1; //�ޯ�1�ݭn�P�P��
    public int NeedStars2; //�ޯ�2�ݭn�P�P��
    public int playerStars; //���a���X�P�P��

    public bool UseSkill1; //�ϥΧޯ�1
    public bool UseSkill2; //�ϥΧޯ�2
    public bool UsePassiveSkill; //�A�γQ�ʧޯ�

    // Start is called before the first frame update
    void Start()
    {
        //Setting();
    }
    public abstract void Setting(); //��l�]�w
    public abstract void Skill1(); //�D�ޯ�1
    public abstract void Skill2(); //�D�ޯ�2
    public abstract void PassiveSkill(); //�Q�ʧޯ�
    // Update is called once per frame
    public void UseSkill() //�P�w�ޯ�ϥ�
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
