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
    public GameObject TrashCardsText; //棄牌區卡片數文字顯示
    public List<GameObject> TrashCardsObject; //棄牌區卡片們
    // Start is called before the first frame update
    void Start()
    {
        TrashCardsText.SetActive(false);
    }
    private void Update()
    {
        TrashCardsText.GetComponent<TextMeshProUGUI>().text = TrashCardsObject.Count.ToString();
    }
    public void OnPointerEnter(PointerEventData pointerEventData) //滑鼠游標移入
    {
            TrashCardsText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData) //滑鼠游標移出
    {
            TrashCardsText.SetActive(false);
    }
    public void ClickTrashZone()
    { 
    
>>>>>>> 7680eedc252985faa318d04fb0c4961d29f1ff7b
    }
}
