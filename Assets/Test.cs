using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Log("hahaha");
    }

    void Update()
    {
    }

    [Conditional("DEBUG_VER")]
    static public void Log(object msg)
    {
        UnityEngine.Debug.LogError(msg);
    }
}