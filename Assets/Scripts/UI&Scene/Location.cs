using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static bool canStop = false; //�i�H���d�b�� 
    public void MovePointClick()
    {
        if (DuelStateManager.canInterect/*&& Player.canMove*/)
        {
            Player.MoveToLocation = this.gameObject.transform.GetSiblingIndex();
        }
    }
}
