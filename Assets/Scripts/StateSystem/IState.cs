using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
   void OnState();
   void OnUpdate();
   void OnExit();
}
