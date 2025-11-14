using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyData
{
    public int index;
    public float hp;
    public float attack;
    public float pursueDis;
    public float attackDis;
}
[Serializable]
public struct EnemyRoot
{
    public EnemyData[] root;

}


public class EnemyHelper : HelperBase
{
    public EnemyRoot config = new EnemyRoot();

    public override void Init(string jsonData)
    {
        config = JsonUtility.FromJson<EnemyRoot>(jsonData);
        GlobalFunc.Log(typeof(EnemyRoot) + "Init Finish");
    }
    public override string GetJsonPath()
    {
        return "Config/enemy_cf.json";
    }
    public EnemyData GetEnemy(int id)
    {
        var item = config.root[id - 1];
        return item;
    }
}
