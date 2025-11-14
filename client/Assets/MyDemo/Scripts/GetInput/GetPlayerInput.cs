using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MyDemo 
{ 
/// <summary>
/// 获取玩家的输入
/// </summary>
public class GetPlayerInput : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        GetEditorInput();
#elif UNITY_ANDROID
        GetAndroidInput();
#endif
    }

    /// <summary>
    /// 获取编辑器下的键盘模拟输入(用鼠标点击模拟屏幕点击)
    /// </summary>
    private void GetEditorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Execute(GameEventKey.ScreenClick,Input.mousePosition);
        }
    }

    /// <summary>
    /// 获取安卓平台的输入
    /// </summary>
    private void GetAndroidInput()
    {
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.Execute(GameEventKey.ScreenClick, Input.mousePosition);
            }
        }
}
}