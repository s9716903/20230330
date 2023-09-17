using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T> 
{
    private static T m_instance = null;
    public static T GetInstance()
    {
        return m_instance;
    }
    protected virtual void Awake()
    {
        m_instance = (T)this;
    }
}
