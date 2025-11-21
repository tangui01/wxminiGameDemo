using System.Collections;
using System.Collections.Generic;
using sky_mirror;
using UnityEngine;

/// <summary>
/// 玩家游戏数据
/// </summary>
[System.Serializable]
public class PlayerGameData
{
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int currentLevel;

    /// <summary>
    /// 玩家上次处于位置(放大100倍后)
    /// </summary>
    public int posX;

    public int posY;
    public int posZ;

    /// <summary>
    /// 当前经验值
    /// </summary>
    public int currentExp;

    /// <summary>
    /// 角色当前等级
    /// </summary>
    public int currentPlayerLv;

    /// <summary>
    /// 玩家攻击方式
    /// </summary>
    public int playerAttackMode;

    /// <summary>
    /// 玩家武器
    /// </summary>
    public string playerWeaPon;
}

public class GameData : BassData
{
    private PlayerGameData _data = new PlayerGameData();

    public GameData()
    {
        var dataJson = this.LoadData();
        _data = JsonUtility.FromJson<PlayerGameData>(dataJson);

        GlobalFunc.Log("GameData ctor");
    }

    public override DataEnum GetId()
    {
        return DataEnum.GameData;
    }

    public PlayerGameData GetData()
    {
        return _data;
    }

    public override string InitData()
    {
        _data.currentExp = 0;
        _data.currentPlayerLv = 1;
        _data.currentLevel = 1;
        _data.posX = -2200;
        _data.posY = -1250;
        _data.posZ = 0;
        return JsonUtility.ToJson(_data);
    }

    /// <summary>
    /// 保存当前关卡进度
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public bool SetLevel(int lv)
    {
        _data.currentLevel = lv;
        return true;
    }

    public bool SetPos(Vector3 pos)
    {
        _data.posX = (int)(pos.x * 100);
        _data.posY = (int)(pos.y * 100);
        _data.posZ = (int)(pos.z * 100);
        SaveData(JsonUtility.ToJson(_data));
        return true;
    }

    public bool SetExp(int exp)
    {
        _data.currentExp = exp;
        SaveData(JsonUtility.ToJson(_data));
        return true;
    }

    /// <summary>
    /// 保存人物等级
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    public bool SetPlayerLv(int lv)
    {
        _data.currentPlayerLv = lv;
        SaveData(JsonUtility.ToJson(_data));
        return true;
    }

    public Vector3 GetPlayerPos()
    {
        return new Vector3((float)_data.posX / 100, (float)_data.posY / 100, (float)_data.posZ / 100);
    }
}