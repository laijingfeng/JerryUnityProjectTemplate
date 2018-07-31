using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text txt;

    void Start()
    {
        Haha();
        txt.text = getCoin().ToString();
    }

    void Update()
    {
    }

    private int getCoin()
    {
        return 200;
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