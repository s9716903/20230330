using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private RectTransform rectTransform;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Defense;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIUP");
        rectTransform.position += new Vector3(0, 0, 42);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //菲公村夹簿
    {
        Debug.Log("SkillUIDOWN");
        rectTransform.position -= new Vector3(0, 0, 42);
    }
}