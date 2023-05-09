using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private bool openSkillUI;

    public GameObject SkillUp;
    public GameObject SkillUpTop;
    public GameObject SkillUpBottom;
    public GameObject SkillDown;

    public TextMeshProUGUI MaxHp;
    public TextMeshProUGUI Defense;

   
    // Start is called before the first frame update
    void Start()
    {
        openSkillUI = false;
        SkillUp.SetActive(false);
        SkillDown.SetActive(true);
        SkillUpTop.SetActive(true);
        SkillUpBottom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (openSkillUI)
            {
                SkillUp.SetActive(true);
                SkillDown.SetActive(false);
            }
            else
            {
                SkillUpTop.SetActive(true);
                SkillUpBottom.SetActive(false);
                SkillUp.SetActive(false);
                SkillDown.SetActive(true);
            }
        }
        if (!DuelStateManager.canInterect)
        {
            SkillUp.SetActive(false);
            SkillDown.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (SkillUp.activeInHierarchy && pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (SkillUpTop.activeInHierarchy)
            {
                SkillUpTop.SetActive(false);
                SkillUpBottom.SetActive(true);
            }
            else if (SkillUpBottom.activeInHierarchy)
            {
                SkillUpTop.SetActive(true);
                SkillUpBottom.SetActive(false);
            }

        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIUP");
        openSkillUI = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIDOWN");
        openSkillUI = false;
    }
}