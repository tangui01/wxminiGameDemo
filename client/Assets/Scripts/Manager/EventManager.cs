using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static EventManager _instance = null;
    /// <summary>
    /// 事件容器
    /// </summary>
    private Dictionary<sky_mirror.SM_EventType, Action<string, string, string>> eventDic = new Dictionary<sky_mirror.SM_EventType, Action<string, string, string>>();
    public void Init()
    {

    }

    public static EventManager Instance()
    {
        if (_instance == null)
        {
            _instance = new EventManager();
        }

        return _instance;
    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public void AddEventListener(sky_mirror.SM_EventType eventName, Action<string, string, string> action)
    {
        //GlobalFunc.Log("AddEventListener:" + eventName);
        if (eventDic.ContainsKey(eventName))
        {
            eventDic[eventName] += action;
        }
        else
        {
            eventDic.Add(eventName, action);
        }
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(sky_mirror.SM_EventType eventName, string info ="", string info2 = "", string info3 = "")
    {
        if (eventDic.ContainsKey(eventName))
        {
            try
            {
                var act = eventDic[eventName];
                act?.Invoke(info, info2, info3);//.Invoke(info);
            }
            //
            catch (Exception e)
            {
                Debug.Log("eventName:" + eventName + " " + info + " " + info2);
                Debug.Log("EventTrigger -----" + e.Message);
            }
            finally//这个也可以不写
            {
                // 这里面是不管异常不异常都会执行的语句
            }
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">委托函数</param>
    public void RemoveEventListener(sky_mirror.SM_EventType eventName, Action<string,string,string> action)
    {
        //GlobalFunc.Log("RemoveEventListener:" + eventName);
        if (eventDic.ContainsKey(eventName))
            eventDic[eventName] -= action;
    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}