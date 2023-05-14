using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationUIExplain : MonoBehaviour
{
    public GameObject ExplainUpText;
    public GameObject ExplainUpATK;
    public GameObject UpPlayerTargetLocation;
    public Image UpATKIcon;
    public GameObject[] UpATKZone;
    public GameObject ExplainDownText;
    public GameObject ExplainDownATK;
    public GameObject DownPlayerTargetLocation;
    public Image DownATKIcon;
    public GameObject[] DownATKZone;

    [Header("Up and Down Public")]
    public Sprite[] ATKIcon;
    // Update is called once per frame
    public void ClearInformation()
    {
        for (int a = 0; a < UpATKZone.Length; a++)
        {
            UpATKZone[a].gameObject.GetComponent<Image>().color = new Color32(120, 120, 120, 100);
        }
        for (int b = 0; b < DownATKZone.Length; b++)
        {
            DownATKZone[b].gameObject.GetComponent<Image>().color = new Color32(120, 120, 120, 100);
        }
        if (InformationUI.InformationUpID == 1)
        {
            if (InformationUI.InformationUpAttackType == 0)
            {
                UpPlayerTargetLocation.SetActive(true);
                UpATKIcon.sprite = ATKIcon[0];
                for (int i = 0; i < InformationUI.AttackZoneUP.Length; i++)
                {
                    UpATKZone[2 + InformationUI.AttackZoneUP[i]].gameObject.GetComponent<Image>().color = new Color32(180, 18, 18, 200);
                }
                ExplainUpText.SetActive(false);
                ExplainUpATK.SetActive(true);
            }
            else if (InformationUI.InformationUpAttackType == 1)
            {
                UpPlayerTargetLocation.SetActive(false);
                UpATKIcon.sprite = ATKIcon[1];
                for (int i = 0; i < InformationUI.AttackZoneUP.Length; i++)
                {
                    UpATKZone[InformationUI.AttackZoneUP[i]].gameObject.GetComponent<Image>().color = new Color32(18, 100, 180, 200);
                }
                ExplainUpText.SetActive(false);
                ExplainUpATK.SetActive(true);
            }
        }
        else
        {
            ExplainUpText.SetActive(true);
            ExplainUpATK.SetActive(false);
        }

        if (InformationUI.InformationDownID == 1)
        {
            if (InformationUI.InformationDownAttackType == 0)
            {
                DownPlayerTargetLocation.SetActive(true);
                DownATKIcon.sprite = ATKIcon[0];
                for (int i = 0; i < InformationUI.AttackZoneDown.Length; i++)
                {
                    DownATKZone[2 + InformationUI.AttackZoneDown[i]].gameObject.GetComponent<Image>().color = new Color32(180, 18, 18, 200);
                }
                ExplainDownText.SetActive(false);
                ExplainDownATK.SetActive(true);
            }
            else if (InformationUI.InformationDownAttackType == 1)
            {
                DownPlayerTargetLocation.SetActive(false);
                DownATKIcon.sprite = ATKIcon[1];
                for (int i = 0; i < InformationUI.AttackZoneDown.Length; i++)
                {
                    DownATKZone[InformationUI.AttackZoneDown[i]].gameObject.GetComponent<Image>().color = new Color32(18, 100, 180, 200);
                }
                ExplainDownText.SetActive(false);
                ExplainDownATK.SetActive(true);
            }
        }
        else
        {
            ExplainDownText.SetActive(true);
            ExplainDownATK.SetActive(false);
        }
    }
}
