using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotDataAsset : ScriptableObject
{
    public bool bDefaultSetting = true;

    public string fileRootPath;

    public Camera camera ;

    public int width;

    public int height;
}
