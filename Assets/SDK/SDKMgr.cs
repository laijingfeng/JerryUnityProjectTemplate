using Jerry;

public partial class SDKMgr : SingletonMono<SDKMgr>
{
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    
    private void Init()
    {
        JerrySDKMgr_Create();
    }
}