using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    public GameObject[] Alocation;
    public GameObject[] Blocation;
    public int Locations; //場地格數 
    // Start is called before the first frame update
    void Awake()
    {
        Alocation = new GameObject[Locations];
        Blocation = new GameObject[Locations];
        for (int i = 0; i < Locations; i++)
        {
           Alocation[i] = transform.GetChild(0).transform.GetChild(i).gameObject;
           Blocation[i] = transform.GetChild(1).transform.GetChild(i).gameObject;
        }
    }
}
