
public class JerrySDKImpl
{
    /// <summary>
    /// 获取设备ID
    /// </summary>
    /// <returns></returns>
    public virtual string GetDeviceId()
    {
        return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="par">-1参数异常；-2禁用了下载；0正常；1下载完成，进入安装</param>
    /// <returns></returns>
    public virtual int DownloadApk(string par)
    {
        return -2;
    }

    /// <summary>
    /// 获取下载进度
    /// </summary>
    /// <returns></returns>
    public virtual string GetDownloadPro()
    {
        return string.Empty;
    }

    /// <summary>
    /// 测试
    /// </summary>
    public virtual void DoTest()
    {
    }

    public virtual void CleanCache()
    {
    }

    public virtual void CopyTextToClipboard(string str)
    {
    }

    public virtual string GetTextFromClipboard()
    {
        return string.Empty;
    }
}