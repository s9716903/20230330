using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCardZone : MonoBehaviour
{
    public List<GameObject> ReadCards; //準備完成的所有卡片
    public bool candraw; //用完是否可抽牌
    public int Type; //種類
    public int Value; //數值 
    // Start is called before the first frame update
    void Start()
    {
        ReadCards.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
