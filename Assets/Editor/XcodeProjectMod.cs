using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.iOS.Xcode.Custom;
#endif
using System.IO;

public class XcodeProjectMod : MonoBehaviour
{
    //该属性是在build完成后，被调用的callback
    [PostProcessBuildAttribute(0)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        //BuildTarget需为iOS
        if (buildTarget != BuildTarget.iOS)
        {
            return;
        }

        #region 配置

        //初始化
        string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(projectPath);
        string targetGuid = pbxProject.TargetGuidByName("Unity-iPhone");

        //添加flag
        pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");//for bugly
        pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-lz");//for IFlyMSC
        //关闭Bitcode
        pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");//for IFlyMSC,normal

        //Windows下自动导出的部分斜杠不对，重新设置一次
        pbxProject.SetBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        pbxProject.AddBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks/Plugins/Bugly/iOS");//for bugly
        pbxProject.AddBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks/Plugins/IFlyMSC/iOS");//for IFlyMSC

        //Windows下自动导出的部分斜杠不对，重新设置一次
        pbxProject.SetBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(inherited)");
        pbxProject.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)");
        pbxProject.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");
        pbxProject.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries/Plugins/Bugly/iOS");//for bugly
        pbxProject.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries/Plugins/IFlyMSC/iOS");//for IFlyMSC

        //添加framwrok
        pbxProject.AddFrameworkToProject(targetGuid, "Security.framework", false);//for bugly,idfa
        pbxProject.AddFrameworkToProject(targetGuid, "SystemConfiguration.framework", false);//for bugly,IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "JavaScriptCore.framework", true);//for bugly
        pbxProject.AddFrameworkToProject(targetGuid, "AdSupport.framework", false);//for idfa

        pbxProject.AddFrameworkToProject(targetGuid, "AVFoundation.framework", false);//for IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "Foundation.framework", false);//for IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "CoreTelephony.framework", false);//for IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "AudioToolbox.framework", false);//for IFlyMSC
        //pbxProject.AddFrameworkToProject(targetGuid, "UIKit.framework", false);//for IFlyMSC
        //pbxProject.AddFrameworkToProject(targetGuid, "CoreLocation.framework", false);//for IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "Contacts.framework", false);//for IFlyMSC
        pbxProject.AddFrameworkToProject(targetGuid, "AddressBook.framework", false);//for IFlyMSC
        //pbxProject.AddFrameworkToProject(targetGuid, "QuartzCore.framework", false);//for IFlyMSC
        //pbxProject.AddFrameworkToProject(targetGuid, "CoreGraphics.framework", false);//for IFlyMSC

        //添加lib
        AddLibToProject(pbxProject, targetGuid, "libc++.tbd");//for bugly
        AddLibToProject(pbxProject, targetGuid, "libz.tbd");//for bugly,IFlyMSC
        //AddLibToProject(pbxProject, targetGuid, "libicucore.tbd");//for IFlyMSC

        //应用修改
        File.WriteAllText(projectPath, pbxProject.WriteToString());

        #endregion 配置

        #region 修改Info.plist

        // 修改Info.plist文件
        var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        plist.root.SetString("NSMicrophoneUsageDescription", "Voice Talk Need Microphone");//for IFlyMSC
        //plist.root.SetString("NSLocationUsageDescription", "Voice Talk Need Location");//for IFlyMSC
        //plist.root.SetString("NSLocationAlwaysUsageDescription", "Voice Talk Need Location");//for IFlyMSC
        //plist.root.SetString("NSContactsUsageDescription", "Voice Talk Need Contacts");//for IFlyMSC
        // 应用修改
        plist.WriteToFile(plistPath);

        #endregion 修改Info.plist
    }

    /// <summary>
    /// 添加lib方法
    /// </summary>
    /// <param name="inst"></param>
    /// <param name="targetGuid"></param>
    /// <param name="lib"></param>
    static void AddLibToProject(PBXProject inst, string targetGuid, string lib)
    {
        string fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
        inst.AddFileToBuild(targetGuid, fileGuid);
    }
}