using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sky_mirror;

[Serializable]
public class SetupItem
{
    public bool sound;

    public SetupItem()
    {
        sound = false;
    }
}

public class Setup : BassData
{
    public SetupItem _Data = new SetupItem();

    public Setup()
    {
        var dataJson = this.LoadData();
        _Data = JsonUtility.FromJson<SetupItem>(dataJson);
    }

    public override DataEnum GetId()
    {
        return DataEnum.Setup;
    }

    public override string InitData()
    {
        //≥ı ºªØ
        _Data.sound = true;

        return JsonUtility.ToJson(_Data);
    }

    public bool IsSound()
    {
        return _Data.sound;
    }

    public void SetSound(bool isTrue)
    {
        _Data.sound = isTrue;

        SaveData(JsonUtility.ToJson(_Data));
    }
}
