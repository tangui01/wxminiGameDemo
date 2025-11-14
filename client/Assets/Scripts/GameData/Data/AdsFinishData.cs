using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public class AdsFinishDataItem
//{
//    public int cnt;

//    public AdsFinishDataItem()
//    {
//        cnt = 0;
//    }
//}

[Serializable]
public class AdsFinishDataData
{
    //public List<AdsFinishDataItem> data;
    public int Cnt = 0;

    public AdsFinishDataData()
    {
        Cnt = 0;
    }
}

public class AdsFinishData : BassData
{
    private AdsFinishDataData _data = new AdsFinishDataData();

    public AdsFinishData()
    {
        var dataJson = this.LoadData();
        _data = JsonUtility.FromJson<AdsFinishDataData>(dataJson);
    }

    public override DataEnum GetId()
    {
        return DataEnum.AdsFinishData;
    }

    public override string InitData()
    {
        //³õÊ¼»¯
        _data.Cnt = 0;

        return JsonUtility.ToJson(_data);
    }

    public void PlayAds(Action call)
    {
        Action finishCall = () =>
        {
            SaveData(JsonUtility.ToJson(_data));
            call.Invoke();
        };

        PlatformMgr.Instance().PlayAds(finishCall);
        
    }

}
