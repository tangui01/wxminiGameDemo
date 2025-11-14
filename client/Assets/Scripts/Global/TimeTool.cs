using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTool
{
    public static long ConvertDateTimeToUtc_10(DateTime _time)
    {
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(timeSpan.TotalSeconds);
    }

    public static long ConvertDateTimeToUtc_10_Millis(DateTime _time)
    {
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(timeSpan.TotalMilliseconds);
    }

    /// <summary>
    /// 10位时间戳转化为时间
    /// </summary>
    /// <param name="_utcTime">时间戳</param>
    /// <returns></returns>
    public static DateTime ConvertUtcToDateTime_10(long _utcTime)
    {
        DateTime dt = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
        long lTime = long.Parse(_utcTime + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dt.Add(toNow);
    }

    /// <summary>
    /// 获取两时间的时间差
    /// </summary>
    /// <param name="_time1">时间戳1</param>
    /// <param name="_time2">时间戳2</param>
    /// <returns></returns>
    public static int GetTwoTimeDruation(long _time1, long _time2)
    {


        long m_Time = _time1 - _time2;
        return (int)m_Time;
    }

}
