using UnityEngine;
using System;
[Serializable]
public struct LevelGameData
{
    public int levelID;
    /// <summary>
    ///普通小怪
    /// </summary>
    public string minion;
    /// <summary>
    /// 关卡Boss
    /// </summary>
    public string boss;
}
[Serializable]
public struct LevelGameDataRoot
{
    public LevelGameData[] root;
}

/// <summary>
/// 关卡怪物数据配置
/// </summary>
public class LevelGameDataHelper:HelperBase
{
    public LevelGameDataRoot config;
    public override void Init(string jsonData)
    {
        config = JsonUtility.FromJson<LevelGameDataRoot>(jsonData);
        GlobalFunc.Log(typeof(LevelGameDataRoot) + "Init Finish");
    }
    public override string GetJsonPath()
    {
        return "Config/levelgamedata_cf.json";
    }

    public LevelGameData GetLevelGameData(int id)
    {
         
        var item = config.root[id - 1];
     
        return item;
    }
}
