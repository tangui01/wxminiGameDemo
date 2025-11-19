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
        //��������ģ��
        AddData(new Currency());
        AddData(new AdsFinishData());
        AddData(new Battle());
        AddData(new GameData());
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

    public static Battle GetBattle()
    {
        return (Battle)Instance().GetDataForId(DataEnum.Battle);
    }

    public static GameData GetGameData()
    {
        return (GameData)Instance().GetDataForId(DataEnum.GameData);
    }

    public void ClearData()
    {
        foreach (var item in _dataDis)
        {
            item.Value.ClearData();
        }
    }
}
