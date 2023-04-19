using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static bool canStop = false; //可以停留在此 
    public void MovePointClick()
    {
        if (GameManager.canInterect/*&& Player.canMove*/)
        {
            Player.MoveToLocation = this.gameObject.transform.GetSiblingIndex();
        }
    }
}
