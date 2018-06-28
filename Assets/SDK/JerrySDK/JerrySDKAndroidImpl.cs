using System;
using UnityEngine;

public class JerrySDKAndroidImpl : JerrySDKImpl
{
#if UNITY_ANDROID

    private AndroidJavaObject m_SDK = null;

    public JerrySDKAndroidImpl(string mgr)
    {
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    m_SDK = new AndroidJavaObject("com.jerry.lai.lib.UnityPluginInterface", mgr, jo);
                }
            }
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    public override string GetDeviceId()
    {
        if (m_SDK != null)
        {
            return m_SDK.Call<string>("getDeviceId");
        }
        return base.GetDeviceId();
    }

    public override int DownloadApk(string par)
    {
        if (m_SDK != null)
        {
            return m_SDK.Call<int>("downloadApk", par);
        }
        return base.DownloadApk(par);
    }

    public override string GetDownloadPro()
    {
        if (m_SDK != null)
        {
            return m_SDK.Call<string>("getDownloadPro");
        }
        return base.GetDownloadPro();
    }

    public override void DoTest()
    {
        if (m_SDK != null)
        {
            m_SDK.Call("doTest");
        }
    }

    public override void CleanCache()
    {
        if (m_SDK != null)
        {
            m_SDK.Call("cleanCache");
        }
    }

    public void Log(string str)
    {
        //UnityEngine.Debug.LogWarning(str);
    }
#endif
}