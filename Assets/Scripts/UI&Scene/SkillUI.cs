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
    public void OnPointerEnter(PointerEventData pointerEventData) //�ƹ���в��J
    {
        Debug.Log("SkillUIUP");
        rectTransform.transform.position += new Vector3(0, 0, 40);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //�ƹ���в��X
    {
        Debug.Log("SkillUIDOWN");
        rectTransform.transform.position -= new Vector3(0, 0, 40);
    }
}