using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// iPhone X适配工具类
/// Jeff 2017-12-1
/// 文件名 SafeAreaUtils.cs
/// </summary>
public class SafeAreaUtils
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void GetSafeArea(out float x, out float y, out float w, out float h);
#endif
    
    /// <summary>
    /// 获取iPhone X 等苹果未来的异性屏幕的安全区域Safe are
    /// </summary>
    /// <param name="showInsetsBottom"></param>
    /// <returns></returns>
    public static Rect Get()
    {
        float x, y, w, h;
#if UNITY_IOS && !UNITY_EDITOR
        GetSafeArea(out x, out y, out w, out h);
#else
        x = 0;
        y = 0;
        w = Screen.width;
        h = Screen.height;
#endif
        return new Rect(x, y, w, h);
    }
}