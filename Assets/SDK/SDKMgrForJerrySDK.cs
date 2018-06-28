using Jerry;

public partial class SDKMgr
{
    private JerrySDK m_JerrySDK = null;

    private void JerrySDKMgr_Create()
    {
        m_JerrySDK = new JerrySDK(this.gameObject.name);
    }

    /// <summary>
    /// 获取设备ID
    /// </summary>
    /// <returns></returns>
    public string JerrySDKMgr_GetDeviceId()
    {
        if (m_JerrySDK != null)
        {
            return m_JerrySDK.GetDeviceId();
        }
        return string.Empty;
    }

    /// <summary>
    /// -1参数异常；-2禁用了下载；0正常；1下载完成，进入安装
    /// </summary>
    /// <param name="par">-1参数异常；-2禁用了下载；0正常；1下载完成，进入安装</param>
    /// <returns></returns>
    public int JerrySDKMgr_DownloadApk(JerrySDK.DownloadPar par)
    {
        if (m_JerrySDK != null)
        {
            return m_JerrySDK.DownloadApk(par);
        }
        return -2;
    }

    public JerrySDK.DownLoadPro JerrySDKMgr_GetDownloadPro()
    {
        if (m_JerrySDK != null)
        {
            return m_JerrySDK.GetDownLoadPro();
        }
        return null;
    }

    private void SDK2Unity_DownloadFinishCallback(string data)
    {
    }

    /// <summary>
    /// 仅仅是测试
    /// </summary>
    public void JerrySDKMgr_DoTest()
    {
        if (m_JerrySDK != null)
        {
            m_JerrySDK.DoTest();
        }
    }

    public void JerrySDKMgr_CleanCache()
    {
        if (m_JerrySDK != null)
        {
            m_JerrySDK.CleanCache();
        }
    }

    public void JerrySDKMgr_CopyTextToClipboard(string str)
    {
        if (m_JerrySDK != null)
        {
            m_JerrySDK.CopyTextToClipboard(str);
        }
    }

    public string JerrySDKMgr_GetTextFromClipboard()
    {
        if (m_JerrySDK != null)
        {
            return m_JerrySDK.GetTextFromClipboard();
        }
        return string.Empty;
    }
}