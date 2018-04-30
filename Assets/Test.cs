using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Log("hahaha");
        foreach (Type t in by_property)
        {
            UnityEngine.Debug.LogWarning(" " + t);
        }
    }
    

    void Update()
    {
    }

    [Conditional("DEBUG_VER")]
    static public void Log(object msg)
    {
        UnityEngine.Debug.LogError(msg);
    }

    public static List<Type> by_property
    {
        get
        {
            return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                    where !type.ToString().Contains("XLua.")
                    select type).ToList();
        }
    }
}