using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetLocation >= groundLocation.GetComponent<Ground>().Locations)
        {
            TargetLocation = groundLocation.GetComponent<Ground>().Locations - 1;
        }
        if (TargetLocation < 0)
        {
            TargetLocation = 0;
        }
        transform.position = new Vector3(groundLocation.GetComponent<Ground>().enemylocation[TargetLocation].transform.position.x, 5, groundLocation.GetComponent<Ground>().enemylocation[TargetLocation].transform.position.z);
    }
}
