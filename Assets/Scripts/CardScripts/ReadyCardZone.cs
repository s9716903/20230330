using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCardZone : MonoBehaviour
{
    public List<GameObject> ReadCards; //�ǳƧ������Ҧ��d��
    public bool candraw; //�Χ��O�_�i��P
    public int Type; //����
    public int Value; //�ƭ� 
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
