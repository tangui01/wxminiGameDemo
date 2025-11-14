using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sky_mirror;

public class BassData
{
    public virtual string InitData()
    {
        return "";
    }

    public virtual DataEnum GetId()
    {
        return DataEnum.Bass;
    }

    public void SaveData(string DataJson)
    {
        SaveSystem.SavePlayer(GetId().ToString(), DataJson);
    }

    public string LoadData()
    {
        var data = SaveSystem.LoadPlayer(GetId().ToString());
        if(data == null)
        {
            data = InitData();
            SaveData(data);
        }

        return data;
    }

    public void ClearData()
    {
        var data = InitData();
        SaveData(data);
    }
    

}
