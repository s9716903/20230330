using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashCard : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject TrashCardsText; //��P�ϥd���Ƥ�r���
    public List<GameObject> TrashCardsObject; //��P�ϥd����


    // Start is called before the first frame update
    void Start()
    {
        TrashCardsText.SetActive(false);
    }
    private void Update()
    {
        TrashCardsText.GetComponent<TextMeshProUGUI>().text = TrashCardsObject.Count.ToString();
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //�ƹ���в��J
    {
        TrashCardsText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //�ƹ���в��X
    {
        TrashCardsText.SetActive(false);
    }
    /*public void ClickTrashZone()
    {
        TrashZoneUI.readTrashZoneList = true;
    }*/
}
