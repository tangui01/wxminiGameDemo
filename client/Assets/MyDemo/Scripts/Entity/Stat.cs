using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性基类(攻击力,移动速度等等)
/// </summary>
[Serializable]
public class Stat
{
    private float baseValue; //基础数值
    private Dictionary<string, Buff> _buffValue;

    // 事件：当基础属性值发生变化时触发
    public event Action<float> OnBaseValueAddChanged;

    public event Action<float> OnBaseValueReduceChanged;

    //当buff部分发生改变时(如卸载装备时)
    public event Action<Buff> OnBuffValueAddChanged;
    public event Action<Buff> OnBuffValuRemoveChanged;


    public Stat(float initValue)
    {
        baseValue = initValue;
        _buffValue = new Dictionary<string, Buff>();
    }

    public void AddBuff(Buff addBuff)
    {
        if (addBuff==null) return;
        {
            Debug.LogError("添加的Buff不能为空");
        }
        if (string.IsNullOrEmpty(addBuff.ID))
        {
            Debug.LogError("Buff的ID不能为空");
            return;
        }
        if (!_buffValue.TryAdd(addBuff.ID, addBuff))
        {
            Debug.LogError($"id为{addBuff.ID}的Buff已经添加");
        }
        
    }

    private void AddBaseValue(float addValue)
    {
        if (Mathf.Approximately(addValue, 0)) return;
        
        baseValue += addValue;
        baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue);
        
        if (addValue > 0)
            OnBaseValueAddChanged?.Invoke(addValue);
        else
            OnBaseValueReduceChanged?.Invoke(addValue);
    }

    public void RemoveBuff(Buff removeBuff)
    {
        if (!_buffValue.Remove(removeBuff.ID, out removeBuff))
        {
            Debug.LogWarning($"id为{removeBuff.ID}的Buff已经移除或者未添加");
        }
    }

    public void RemoveBaseValue(float removeValue)
    {
        baseValue -= removeValue;
        baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue);
    }

    /// <summary>
    /// 获取真实数值(基础数值加buff数值)
    /// </summary>
    /// <returns></returns>
    public float GetAllValue()
    {
       
        float flatBonus = 0f;
        float percentBonus = 0f;
        float percentMultBonus = 1f;
        
        foreach (var buff in _buffValue.Values)
        {
            switch (buff.BuffType)
            {
                case BuffType.Flat:
                    flatBonus += buff.Value;
                    break;
                case BuffType.Percent:
                    percentBonus += buff.Value;
                    break;
                case BuffType.PercentMult:
                    percentMultBonus *= (1 + buff.Value);
                    break;
            }
        }
        
        float total = (baseValue + flatBonus) * (1 + percentBonus) * percentMultBonus;
        return Mathf.Max(0, total);
    }

    /// <summary>
    /// 获取基础数值
    /// </summary>
    /// <returns></returns>
    public float GetBaseValue()
    {
        return baseValue;
    }
}