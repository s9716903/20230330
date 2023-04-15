using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //IState提供的運行模式
{
    void OnEnter(); //狀態進入時執行
    void OnUpdate(); //狀態持續中執行
    void OnExit(); //狀態脫離時執行
}
