using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleData
{
    //public List<AdsFinishDataItem> data;
    public int lv = 0;

    public BattleData()
    {
        lv = 0;
    }
}

public class Battle : BassData
{
    private BattleData _data = new BattleData();

    public Battle()
    {
        var dataJson = this.LoadData();
        _data = JsonUtility.FromJson<BattleData>(dataJson);

        GlobalFunc.Log("Battle ctor");
    }

    public override DataEnum GetId()
    {
        return DataEnum.Battle;
    }

    public override string InitData()
    {
        //初始化
        _data.lv = 0;

        return JsonUtility.ToJson(_data);
    }

    public int GetLv()
    {
        return _data.lv;
    }

    public bool SetLv(int lv)
    {
        //是否满级
        //if(i)
        //{
        //    return false;
        //}

        _data.lv = lv;

        //保存数据
        SaveData(JsonUtility.ToJson(_data));

        return true;
    }
}
