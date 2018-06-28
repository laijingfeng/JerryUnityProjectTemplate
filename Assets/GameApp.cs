using Jerry;
using UnityEngine.UI;

public class GameApp : SingletonMono<GameApp>
{
    public Text tex;

    protected override void Awake()
    {
        base.Awake();
        AddLog("Awake");
    }

    private void Start()
    {
        AddLog("DeviceId：" + SDKMgr.Inst.JerrySDKMgr_GetDeviceId());
    }

    /// <summary>
    /// 增加日志
    /// </summary>
    /// <param name="msg"></param>
    public void AddLog(string msg)
    {
        if (tex == null
            || string.IsNullOrEmpty(msg))
        {
            return;
        }
        tex.text += "\n" + msg;
    }
}
