/****************************************************
	文件：ScreenShotWindow.cs
	作者：Zhangying
	邮箱：zhy18125@gmail.com
	日期：2022/5/7 17:16:40
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using System.Reflection;
using System.Data;

public class ScreenShotWindow : EditorWindow
{
    private static string CURRENTROOTPATH = GetScriptInDirectory(MethodBase.GetCurrentMethod().DeclaringType.Name);

    private static ScreenShotDataAsset mDataAsset;

    private static bool bDefaultSetting = true;
    private static string filePath;
    private static Camera camera;
    private static string width;
    private static string height;


    [MenuItem("Tools/ScreenShot/Screen Shot &C", false)]
    static void OnScreenShot()
    {
        LoadDataAsset();
        if (mDataAsset.bDefaultSetting)
        {
            DefaultScreenShot();
        }
        else
        {
            CustomScreenShot();
        }
    }

    [MenuItem("Tools/ScreenShot/Setting", false)]
    static void Open()
    {
        EditorWindow.GetWindow<ScreenShotWindow>(false, "Setting", true).Show();
    }

    [MenuItem("Tools/ScreenShot/Open Folder", false)]
    static void OnOpenFolder()
    {
        LoadDataAsset();
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + mDataAsset.fileRootPath.Replace("/", "\\"));
    }


    private static void DefaultScreenShot()
    {
        var path = Path.Combine(mDataAsset.fileRootPath, $"{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
        UnityEngine.ScreenCapture.CaptureScreenshot(path);
        Debug.Log("截图保存路径：" + path);    
    }

    private static void CustomScreenShot()
    {
        if (mDataAsset.camera == null)
        {
            mDataAsset.camera = Camera.main;
        }

        // 建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture(mDataAsset.width, mDataAsset.height, 1);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        mDataAsset.camera.targetTexture = rt;
        mDataAsset.camera.Render();

        // **这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(mDataAsset.width, mDataAsset.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0,0, mDataAsset.width, mDataAsset.height), 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        mDataAsset.camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);
        // 最后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();

        // 拼接文件名
        string filename = Path.Combine(mDataAsset.fileRootPath, $"{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");

        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log("截图保存路径：" + filename);
    }

    private void OnEnable()
    {
        LoadDataAsset();
    }

    private void OnDisable()
    {
        mDataAsset = null;
    }

    void OnGUI()
    {
        GUILayout.Space(10);

        bDefaultSetting = EditorGUILayout.Toggle("朴素截图方式", bDefaultSetting);

        DrawSplitLine();

        if (bDefaultSetting)
        {

        }
        else
        {
            camera = (Camera)EditorGUILayout.ObjectField("选择相机", camera, typeof(Camera), true);
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("宽度", GUILayout.Width(75f));
            width = EditorGUILayout.TextField(width);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("长度", GUILayout.Width(75f));
            height = EditorGUILayout.TextField(height);
            GUILayout.EndHorizontal();
        }

        DrawSplitLine();

        // 设置保存路径
        GUILayout.BeginHorizontal();
        GUILayout.Label("保存路径:", GUILayout.Width(75f));
        filePath = GUILayout.TextField(filePath);
        if (GUILayout.Button("选择", GUILayout.Width(45f)))
        {
            filePath = EditorUtility.OpenFolderPanel("", "", "");
            if (filePath.Length == 0)
            {
                filePath = Application.dataPath;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        EditorGUILayout.HelpBox("当前模式为" + (mDataAsset.bDefaultSetting ? "朴素截图": "自定义截图"), MessageType.Info);

        GUILayout.Space(20f);
        if (GUILayout.Button("保存设置"))
        {
            SaveDataAsset();
        }
    }

    private static void LoadDataAsset()
    {
        var path = CURRENTROOTPATH + "/DataAsset.asset";
        if (!File.Exists(path))
        {
            mDataAsset = new ScreenShotDataAsset()
            {
                bDefaultSetting = true,
                fileRootPath = Application.dataPath,
                camera = Camera.main,
                width = Screen.width,
                height = Screen.height
            };

            AssetDatabase.CreateAsset(mDataAsset, path);
            AssetDatabase.Refresh();
        }
        else
        {
            mDataAsset = AssetDatabase.LoadAssetAtPath<ScreenShotDataAsset>(path);
        }

        bDefaultSetting = mDataAsset.bDefaultSetting;
        filePath = mDataAsset.fileRootPath;
        camera = mDataAsset.camera;
        width = mDataAsset.width.ToString();
        height = mDataAsset.height.ToString();
    }

    private static void SaveDataAsset()
    {
        // 赋值
        mDataAsset.bDefaultSetting = bDefaultSetting;
        mDataAsset.fileRootPath = filePath;
        mDataAsset.camera = camera;

        mDataAsset.width = int.TryParse(width, out var value1) ? int.Parse(width) : mDataAsset.width;
        mDataAsset.height = int.TryParse(width, out var value2) ? int.Parse(height) : mDataAsset.height;

        // SetDirty确保其可以被保存（命令行模式下不加SetDirty无法保存）
        EditorUtility.SetDirty(mDataAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void DrawSplitLine(float heightValue = 1.0f)
    {
        GUILayout.Space(10f);
        GUILayout.Box("",
                GUILayout.Height(heightValue),
                GUILayout.Height(heightValue),
                GUILayout.Height(heightValue),
                GUILayout.ExpandWidth(true)
                );
        GUILayout.Space(10f);

    }

    /// <summary>
    /// 根据脚本名称获取所在目录
    /// </summary>
    /// <param name="scriptName"></param>
    /// <returns></returns>
    public static string GetScriptInDirectory(string scriptName)
    {
        string[] path = UnityEditor.AssetDatabase.FindAssets(scriptName);
        foreach (var item in path)
        {
            string tmpPtah = AssetDatabase.GUIDToAssetPath(item);
            if (tmpPtah.EndsWith(".cs"))
            {
                return tmpPtah.Replace((@"/" + scriptName + ".cs"), "");
            }
        }
        return string.Empty;


    }
}
