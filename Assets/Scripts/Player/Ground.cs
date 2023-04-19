using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    public GameObject[] playerlocation;
    public GameObject[] enemylocation;
    public int Locations; //場地格數 
    // Start is called before the first frame update
    void Awake()
    {
        playerlocation = new GameObject[Locations];
        enemylocation = new GameObject[Locations];
        for (int i = 0; i < Locations; i++)
        {
            playerlocation[i] = transform.GetChild(0).transform.GetChild(i).gameObject;
            enemylocation[i] = transform.GetChild(1).transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
