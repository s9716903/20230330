using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static bool canStop = false; //可以停留在此 
    public void MovePointClick()
    {
        if (DuelStateManager.canInterect && Player.canMove && Player.isReady == false)
        {
            Player.MoveToLocation = this.gameObject.transform.GetSiblingIndex();
            Player.isReady = true;
        }
    }
}
