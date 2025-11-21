using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 时间单位
    /// </summary>
    public enum TimUnit
    {
        Millisecond,
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
    }

    /// <summary>
    /// 时间任务
    /// </summary>
    public class TimeTask
    {
        public TimeTask(TimUnit timeUnit, int count, float delay, float targetTime, Action callback)
        {
            TimeUnit = timeUnit;
            Count = count;
            Delay = delay;
            TargetTime = targetTime;
            Callback = callback;
        }

        /// <summary>
        /// 时间单位
        /// </summary>
        public TimUnit TimeUnit;

        /// <summary>
        /// 执行次数
        /// </summary>
        public int Count;

        /// <summary>
        /// 定时时间
        /// </summary>
        public float Delay;

        /// <summary>
        /// 目标时间
        /// </summary>
        public float TargetTime;

        /// <summary>
        /// 回调函数
        /// </summary>
        public Action Callback;
    }

    /// <summary>
    /// 定时回调系统
    /// </summary>
    public class TimeTaskSys : SingletonMonoBase<TimeTaskSys>
    {
        private List<TimeTask> _temptasks = new List<TimeTask>();
        private Dictionary<string, TimeTask> _tasks = new Dictionary<string, TimeTask>();

        /// <summary>
        /// 添加计时任务
        /// </summary>
        /// <param name="timeUnit"></param>
        /// <param name="delay"></param>
        /// <param name="callback"></param>
        /// <param name="count">执行次数(当Count为-1时,循环执行)</param>
        public void AddTask(Action callback, TimUnit timeUnit, float delay, int count = -1)
        {
            float targetTime = GetTargetTime(delay, timeUnit);
            string taskUid = GetTaskUid();
            
            TimeTask task = new TimeTask(timeUnit, count, delay, targetTime, callback);
            
            
        }

        private string GetTaskUid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 获取目标时间
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="timeUnit"></param>
        /// <returns></returns>
        private float GetTargetTime(float delay, TimUnit timeUnit)
        {
            float targetTime = 0;
            switch (timeUnit)
            {
                case TimUnit.Millisecond:
                    targetTime = delay / 1000;
                    break;
                case TimUnit.Second:
                    targetTime = delay;
                    break;
                case TimUnit.Minute:
                    targetTime = delay * 60;
                    break;
                case TimUnit.Hour:
                    targetTime = delay * 60* 60;
                    break;
                case TimUnit.Day:
                    targetTime = delay * 60* 60*24;
                    break;
                case TimUnit.Week:
                    targetTime =  delay * 60* 60*24 * 7;
                    break;
                case TimUnit.Month:
                    targetTime =  delay * 60* 60*24* 30;
                    break;
            }
            return targetTime;
        }

        private void Update()
        {
        }
    }
}