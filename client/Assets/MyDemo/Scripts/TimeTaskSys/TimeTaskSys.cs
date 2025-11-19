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
        
    }
}