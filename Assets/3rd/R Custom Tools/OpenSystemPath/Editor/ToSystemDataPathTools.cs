using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class ToSystemDataPathTools : MonoBehaviour
{
    [MenuItem("Tools/Open Directory /ProjectPath")]
    private static void ToProjectPath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.dataPath.Replace("/", "\\") + "\\..\\");
    }


    [MenuItem("Tools/Open Directory /DataPath")]
    private static void ToDataPath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.dataPath.Replace("/", "\\"));
    }

    [MenuItem("Tools/Open Directory /PersistentDataPath")]
    private static void ToPersistentDataPath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.persistentDataPath.Replace("/", "\\"));
    }

    [MenuItem("Tools/Open Directory /StreamingAssetsPath")]
    private static void ToStreamingAssetsPath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.streamingAssetsPath.Replace("/", "\\"));
    }

    [MenuItem("Tools/Open Directory /TemporaryCachePath")]
    private static void ToTemporaryCachePath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.temporaryCachePath.Replace("/", "\\"));
    }

    [MenuItem("Tools/Open Directory /ConsoleLogPath")]
    private static void ToConsoleLogPath()
    {
        System.Diagnostics.Process.Start("Explorer.exe", "/open," + Application.consoleLogPath.Replace("/", "\\"));
    }
}
