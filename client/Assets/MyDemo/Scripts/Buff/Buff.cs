using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff类型(增益buff,)
/// </summary>
public enum BuffType
{
    /// <summary>
    /// 直接相加减
    /// </summary>
    Flat,

    /// <summary>
    /// 通常是指增加基础数值的百分比。多个Percent类型的buff会相加在一起，然后与基础数值相乘。
    /// 例如：基础攻击力100，有两个Percent buff：10%和20%，那么总百分比加成为30%，最终攻击力 = 100 * (1 + 0.1 + 0.2) = 130。
    /// </summary>
    Percent,

    /// <summary>
    /// 也称为独立百分比加成，每个PercentMultbuff都是独立计算的，
    /// 它们之间是乘法关系。这种buff通常用于避免数值膨胀，或者用于特殊的效果，比如某些游戏中的“伤害提升”可能使用独立乘区。
    /// 例如：基础攻击力100，有两个PercentMult buff：10%和20%，那么最终攻击力 = 100 * (1 + 0.1) * (1 + 0.2) = 132。
    /// </summary>
    PercentMult,
}

/// <summary>
/// buff基类
/// </summary>
[System.Serializable]
public class Buff
{
    public string ID { get; private set; } // 或者使用Guid，或者使用来源对象的引用
    public float Value { get; private set; }
    public float Duration { get; private set; } //持续事件(为-1时为永久(例如装备加成攻击力))
    public BuffType BuffType { get; private set; }

    public virtual void Init(string id, BuffType type, float value, float duration)
    {
        ID = id;
        BuffType = type;
        Value = value;
        Duration = duration;
    }
    
}

