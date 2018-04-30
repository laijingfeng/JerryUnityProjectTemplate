using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Haha();
    }

    void Update()
    {
    }

    public void Haha()
    {
        UnityEngine.Debug.LogWarning("haha unity");
    }

    [Conditional("DEBUG_VER")]
    static public void Log(object msg)
    {
        UnityEngine.Debug.LogError(msg);
    }
}