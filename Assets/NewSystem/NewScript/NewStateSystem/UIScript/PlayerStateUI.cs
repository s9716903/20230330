using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateUI : MonoBehaviour
{
    public TextMeshProUGUI MaxHP;
    public TextMeshProUGUI Defense;
    public TextMeshProUGUI Star;
    public TextMeshProUGUI SkillInformation;

    public static int PlayerMaxHpInformation;
    public static int PlayerDefenseInformation;
    public static int PlayerStarInformation;
    public static int ReadSkill;

    // Start is called before the first frame update
    void Start()
    {
        ReadSkill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MaxHP.text = ":" + PlayerMaxHpInformation.ToString();
        Defense.text = ":" + PlayerDefenseInformation.ToString();
        Star.text = ":" + PlayerStarInformation.ToString();
        if (ReadSkill == 0)
        {
            SkillInformation.text = "None";
        }
        else if (ReadSkill == 1)
        {
            SkillInformation.text = "None2";
        }
        else if (ReadSkill == 2)
        {
            SkillInformation.text = "None3";
        }
    }
}
