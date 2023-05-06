using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject SkillUp;
    public GameObject SkillDown;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Defense;

    // Start is called before the first frame update
    void Start()
    {
        SkillUp.SetActive(false);
        SkillDown.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIUP");
        SkillUp.SetActive(true);
        SkillDown.SetActive(false);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIDOWN");
        SkillUp.SetActive(false);
        SkillDown.SetActive(true);
    }
}