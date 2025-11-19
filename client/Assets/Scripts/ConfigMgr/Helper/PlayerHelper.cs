using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色升级数据
/// </summary>
[System.Serializable]
public struct PlayerUpLevelData
{
    public int index;
    public int expRequired;//升级所需经验值
}
[System.Serializable]
public struct PlayerUpLevelDataRoot
{
    public PlayerUpLevelData[] root;
}
public class PlayerUpLevelHelper : HelperBase
{
    public PlayerUpLevelDataRoot config;
    public override void Init(string jsonData)
    {
        config = JsonUtility.FromJson<PlayerUpLevelDataRoot>(jsonData);
        GlobalFunc.Log(typeof(PlayerUpLevelDataRoot) + "Init Finish");
    }
    public override string GetJsonPath()
    {
        return "Config/playerupleveldata_cf.json";
    }

    public PlayerUpLevelData GetPlayerUpLevel(int id)
    {
         
        var item = config.root[id - 1];
     
        return item;
    }
}
