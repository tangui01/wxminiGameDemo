using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static EventManager _instance = null;
    /// <summary>
    /// �¼�����
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
    /// ����¼�����
    /// </summary>
    /// <param name="name">�¼�����</param>
    /// <param name="action">׼�����������¼���ί�к���</param>
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
    /// �¼�����
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
            finally//���Ҳ���Բ�д
            {
                // �������ǲ����쳣���쳣����ִ�е����
            }
        }
    }

    /// <summary>
    /// �Ƴ��¼�����
    /// </summary>
    /// <param name="name">�¼�����</param>
    /// <param name="action">ί�к���</param>
    public void RemoveEventListener(sky_mirror.SM_EventType eventName, Action<string,string,string> action)
    {
        //GlobalFunc.Log("RemoveEventListener:" + eventName);
        if (eventDic.ContainsKey(eventName))
            eventDic[eventName] -= action;
    }

    /// <summary>
    /// ����¼�����
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}