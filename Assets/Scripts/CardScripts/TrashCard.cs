using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCard : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        
=======
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
    public void ClickTrashZone()
    { 
    
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b
    }
}
