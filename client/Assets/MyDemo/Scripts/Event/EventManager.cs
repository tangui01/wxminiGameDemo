using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MyDemo
{
    /// <summary>
    /// 事件系统
    /// </summary>
    public class EventManager
    {
        private static Dictionary<string, IEvent> eventsDic = new Dictionary<string, IEvent>();
        private static readonly object lockObject = new object();

        /// <summary>
        /// 注册无参事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public static void Register(string eventName, UnityAction action)
        {
            if (string.IsNullOrEmpty(eventName)) // 缺少检查
            {
                Debug.LogError("事件名不能为空");
                return;
            }

            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent existingEvent))
                {
                    if (existingEvent != null)
                    {
                        if (existingEvent is Msg msg)
                        {
                            msg.Add(action);
                        }
                        else
                        {
                            Debug.LogError($"事件 {eventName} 已注册为不同类型，当前类型: {existingEvent.GetType()}, 期望类型: {typeof(Msg)}");
                        }
                    }
                }
                else
                {
                    //没有的话就创建
                    eventsDic.Add(eventName, new Msg(action));
                }
            }
        }

        /// <summary>
        /// 取消无参事件
        /// </summary>
        /// <param name="action"></param>
        public static void Unregister(string eventName, UnityAction action)
        {
            if (string.IsNullOrEmpty(eventName)) // 缺少检查
            {
                Debug.LogError("事件名不能为空");
                return;
            }

            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent existingEvent))
                {
                    if (existingEvent != null)
                    {
                        if (existingEvent is Msg msg)
                        {
                            msg.Remove(action);
                            //如果msg为空的话，从事件中心移除
                            if (msg.IsEmpty())
                            {
                                eventsDic.Remove(eventName);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"该{eventName}：事件已经被移除或者未注册");
                }
            }
        }

        /// <summary>
        /// 注册有参事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public static void Register<TPar>(string eventName, UnityAction<TPar> action)
        {
            if (string.IsNullOrEmpty(eventName)) // 缺少检查
            {
                Debug.LogError("事件名不能为空");
                return;
            }

            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent existingEvent))
                {
                    if (existingEvent != null)
                    {
                        if (existingEvent is Msg<TPar> msg)
                        {
                            msg.Add(action);
                        }
                        else
                        {
                            Debug.LogError(
                                $"事件 {eventName} 已注册为不同类型，当前类型: {existingEvent.GetType()}, 期望类型: {typeof(Msg<TPar>)}");
                        }
                    }
                }
                else
                {
                    //没有的话就创建
                    eventsDic.Add(eventName, new Msg<TPar>(action));
                }
            }
        }

        /// <summary>
        /// 取消有参事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="action"></param>
        public static void Unregister<TPar>(string eventName, UnityAction<TPar> action)
        {
            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent _event))
                {
                    if (_event != null)
                    {
                        if (_event is Msg<TPar> msg)
                        {
                            msg.Remove(action);
                            //如果msg为空的话，从事件中心移除
                            if (msg.IsEmpty())
                            {
                                ClearEvent(eventName);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"该{eventName}：事件已经被移除或者未注册");
                }
            }
        }

        /// <summary>
        /// 执行无参函数
        /// </summary>
        /// <param name="eventName"></param>
        public static void Execute(string eventName)
        {
            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent _event))
                {
                    if (_event != null)
                    {
                        if (_event is Msg msg)
                        {
                            msg.Execute();
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"该{eventName}：事件已经被移除或者未注册");
                }
            }
        }

        /// <summary>
        /// 执行有参函数
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        /// <typeparam name="TPar"></typeparam>
        public static void Execute<TPar>(string eventName, TPar data)
        {
            lock (lockObject)
            {
                //查找字典中是否有这个Key
                if (eventsDic.TryGetValue(eventName, out IEvent _event))
                {
                    if (_event != null)
                    {
                        if (_event is Msg<TPar> msg)
                        {
                            msg.Execute(data);
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"该{eventName}：事件已经被移除或者未注册");
                }
            }
        }

        public static void Clear()
        {
            eventsDic.Clear();
        }

        public static void ClearEvent(string eventName)
        {
            eventsDic.Remove(eventName);
        }
    }

    #region 事件参数

    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent
    {
    }

    /// <summary>
    /// 无参事件
    /// </summary>
    public class Msg : IEvent
    {
        private UnityAction eventAction;

        public Msg(UnityAction action)
        {
            Add(action);
        }

        public void Add(UnityAction action)
        {
            eventAction += action;
        }

        public void Remove(UnityAction action)
        {
            eventAction -= action;
        }

        public void Execute()
        {
            if (eventAction == null)
            {
                Debug.LogWarning("要执行的事件为空");
                return;
            }

            eventAction.Invoke();
        }

        public void Clear()
        {
            eventAction = null;
        }

        public bool IsEmpty()
        {
            return eventAction == null;
        }
    }

    /// <summary>
    /// 有参事件
    /// </summary>
    /// <typeparam name="TPar">事件参数类型</typeparam>
    public class Msg<TPar> : IEvent
    {
        private UnityAction<TPar> eventAction;

        public Msg(UnityAction<TPar> action)
        {
            Add(action);
        }

        public void Add(UnityAction<TPar> action)
        {
            eventAction += action;
        }

        public void Remove(UnityAction<TPar> action)
        {
            eventAction -= action;
        }

        public void Execute(TPar data)
        {
            if (eventAction == null)
            {
                Debug.LogWarning("要执行的事件为空");
                return;
            }

            eventAction.Invoke(data);
        }

        public void Clear()
        {
            eventAction = null;
        }

        public bool IsEmpty()
        {
            return eventAction == null;
        }
    }

    #endregion
}
