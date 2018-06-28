#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public class JerrySDKiOSImpl : JerrySDKImpl
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern string __getIDFA();
#endif

    /// <summary>
    /// 获取设备ID
    /// </summary>
    /// <returns></returns>
    public override string GetDeviceId()
    {
#if UNITY_IOS && !UNITY_EDITOR
        return __getIDFA();
#else
        return base.GetDeviceId();
#endif
    }
}