using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashZoneUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool readTrashZoneList;
    public static bool isopenTrashZoneUI;
    // Start is called before the first frame update
    void Start()
    {
        readTrashZoneList = false;
        isopenTrashZoneUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (readTrashZoneList && gameObject.GetComponent<RectTransform>().anchoredPosition.x != -150)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-150, 0, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isopenTrashZoneUI)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-150, 0, 0);
            }
            else
            {
                readTrashZoneList = false;
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(150, 0, 0);
            }
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isopenTrashZoneUI = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isopenTrashZoneUI = false;
    }
}
