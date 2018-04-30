using UnityEngine;
using XLua;

public class XLuaMgr : MonoBehaviour
{
    static private XLuaMgr m_Inst;
    static public XLuaMgr Inst
    {
        get
        {
            return m_Inst;
        }
    }

    private LuaEnv luaEnv = null;

    void Awake()
    {
        m_Inst = this;

        luaEnv = new LuaEnv();
        if (luaEnv != null)
        {
            luaEnv.AddLoader(CustomLoader);
            LoadMain();
        }
    }

    void LoadMain()
    {
        try
        {
            if (luaEnv != null)
            {
                luaEnv.DoString("require('test')");//包含某个文件
                //luaEnv.DoString("require('Main')");//包含某个文件
            }
        }
        catch (System.Exception ex)
        {
            string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);
            Debug.LogError(msg, null);
        }
    }

    public static byte[] CustomLoader(ref string filepath)
    {
        Debug.Log("Load xLua script : " + filepath);
        TextAsset textAsset = (TextAsset)Resources.Load(filepath.Replace(".", "/") + ".lua");
        if (textAsset != null)
        {
            return textAsset.bytes;
        }
        return null;
    }

    void Update()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();

            if (Time.frameCount % 100 == 0)
            {
                luaEnv.FullGc();
            }
        }
    }

    void OnDestroy()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            luaEnv = null;
        }
    }
}