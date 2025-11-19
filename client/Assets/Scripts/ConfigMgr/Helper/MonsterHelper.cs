using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct MonsterData
{
    public int index;
    public int monsterName;
    public float maxHp;
    public float attackValue;
    public float attackDis;
    public int expValue;
}
[System.Serializable]
public struct MonsterDataRoot
{
    public MonsterData[] root;
}
public class MonsterHelper : HelperBase
{
    public MonsterDataRoot config=new MonsterDataRoot();


    public override void Init(string jsonData)
    {
        config = JsonUtility.FromJson<MonsterDataRoot>(jsonData);
        GlobalFunc.Log(typeof(MonsterDataRoot) + "Init Finish");
    }
    public override string GetJsonPath()
    {
        return "Config/monster_cf.json";
    }
    public MonsterData GetMonster(int id)
    {
       
        var item = config.root[id - 1];
     
        return item;
    }
}
