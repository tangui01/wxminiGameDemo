using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sky_mirror;

[Serializable]
public class CurrencyItem
{
    public int id;
    public int cnt;

    public CurrencyItem()
    {
        id = 0;
        cnt = 0;
    }
}

[Serializable]
public class CurrencyData
{
    public List<CurrencyItem> data;

    public CurrencyData()
    {
        data = new List<CurrencyItem>();
    }
}

public class Currency : BassData
{
    public CurrencyData _Data = new CurrencyData();

    public Currency()
    {
        var dataJson = this.LoadData();
        _Data = JsonUtility.FromJson<CurrencyData>(dataJson);

        for (int i = 1; i < (int)CurrencyEnum.Max; i++)
        {
            if (_Data.data.Count < i)
            {
                CurrencyItem item = new CurrencyItem();
                item.id = i;
                item.cnt = 0;

                _Data.data.Add(item);
            }
        }
    }

    public override DataEnum GetId()
    {
        return DataEnum.Currency;
    }

    public override string InitData()
    {
        _Data = new CurrencyData();

        //初始化
        for (int i = 1; i < (int)CurrencyEnum.Max; i++)
        {
            CurrencyItem item = new CurrencyItem();
            item.id = i;    
            item.cnt = 0;

            _Data.data.Add(item);
        }

        return JsonUtility.ToJson(_Data);
    }

    public int GetValue(CurrencyEnum em)
    {
        return _Data.data[(int)em-1].cnt;
    }

    public bool AddValue(CurrencyEnum em, int add, bool isSave = true)
    {
        if(add <= 0)
        {
            return false;
        }
        var cur = GetValue(em);
        cur += add;

        _Data.data[(int)em - 1].cnt = cur;

        if (isSave)
        {
            SaveData(JsonUtility.ToJson(_Data));
        }

        //刷新事件
        EventManager.Instance().EventTrigger(SM_EventType.FlushCurrency, "");
        return true;
    }

    public bool SubValue(CurrencyEnum em, int sub, bool isSave = true)
    {
        if (sub <= 0)
        {
            return false;
        }
        var cur = GetValue(em);
        cur -= sub;

        if(cur<=0)
        {
            cur = 0;
        }

        _Data.data[(int)em - 1].cnt = cur;

        if (isSave)
        {
            SaveData(JsonUtility.ToJson(_Data));
        }
        //SaveData(JsonUtility.ToJson(_Data));

        //刷新事件
        //BattleManager.Instance().FireEvent(Event.CurrencyFlush, "");
        return true;
    }

    public bool CheckCnt(CurrencyEnum em, int cnt)
    {
        var cur = GetValue(em);
        return cur >= cnt;
    }

    public void SaveMyData()
    {
        SaveData(JsonUtility.ToJson(_Data));
    }
}
