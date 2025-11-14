using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using sky_mirror;

public class PlayerData
{
    private Dictionary<DataEnum, BassData> _dataDis = new Dictionary<DataEnum, BassData>();

    public static PlayerData instance = null;

    public static PlayerData Instance()
    {
        instance ??= new PlayerData();
        return instance;
    }

    public PlayerData()
    {
        //创建数据模块
        AddData(new Currency());
        AddData(new AdsFinishData());
    }

    public void AddData(BassData data)
    {
        _dataDis.Add(data.GetId(), data);
    }

    public BassData GetDataForId(DataEnum dataEnum)
    {
        return _dataDis[dataEnum];
    }

    public static T GetDataForId<T> (DataEnum dataEnum)
    {
        var data = Instance().GetDataForId(dataEnum);
        return (T)(object)data;
    }

    public static Currency GetCurrency()
    {
        return (Currency)Instance().GetDataForId(DataEnum.Currency);
    }

    public void ClearData()
    {
        foreach (var item in _dataDis)
        {
            item.Value.ClearData();
        }
    }
}
