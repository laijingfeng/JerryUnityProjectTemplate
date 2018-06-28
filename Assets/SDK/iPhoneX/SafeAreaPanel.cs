using UnityEngine;

/// <summary>
/// 设计安全区域面板（适配iPhone X）
/// Jeff 2017-12-1
/// 文件名 SafeAreaPanel.cs
/// </summary>
public class SafeAreaPanel : MonoBehaviour
{
    private RectTransform target;

#if UNITY_EDITOR
    [SerializeField]
    private bool Simulate_X = false;
#endif


    void Awake()
    {
        target = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        var area = SafeAreaUtils.Get();

#if UNITY_EDITOR

        /*
        iPhone X 横持手机方向:
        iPhone X 分辨率
        2436 x 1125 px

        safe area
        2172 x 1062 px

        左右边距分别
        132px

        底边距 (有Home条)
        63px

        顶边距
        0px
        */

        float Xwidth = 2436f;
        float Xheight = 1125f;
        float Margin = 132f;

        if ((Screen.width == (int)Xwidth && Screen.height == (int)Xheight)
            || (Screen.width == 812 && Screen.height == 375))
        {
            Simulate_X = true;
        }

        if (Simulate_X)
        {
            var insets = area.width * Margin / Xwidth;
            var positionOffset = new Vector2(insets, 0);
            var sizeOffset = new Vector2(insets * 2, 0);
            area.position = area.position + positionOffset;
            area.size = area.size - sizeOffset;
        }
#endif

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        target.anchorMin = anchorMin;
        target.anchorMax = anchorMax;
    }
}