using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// 朝向种类
/// </summary>
public enum FaceDirType
{
    Left,
    Right
}

/// <summary>
/// 设置人物朝向
/// </summary>
public class SetDir : MonoBehaviour
{
    [SerializeField]private FaceDirType currentFaceDir;
    public void Initialize()
    {
        SetFaceDir(currentFaceDir);
    }

    public void SetFaceDir(FaceDirType targetDir)
    {
        if (targetDir == currentFaceDir) return;
      
        //如果人物要朝向左，但是现在朝向右
        if (targetDir == FaceDirType.Left&&Mathf.Approximately(transform.eulerAngles.y,0))
        {
            transform.rotation=Quaternion.Euler(0, 180, 0);
        }
        else if(targetDir == FaceDirType.Right&&Mathf.Approximately(transform.eulerAngles.y,180))
        {
            transform.rotation=Quaternion.identity;
        }
        currentFaceDir = targetDir;
    }

    public FaceDirType GetCurrentFaceDir()
    {
        return currentFaceDir;
    }
}
