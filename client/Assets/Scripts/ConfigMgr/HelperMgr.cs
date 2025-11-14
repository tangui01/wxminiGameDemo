using System;
using System.Collections.Generic;
using UnityEngine;

public class HelperBase
{
    public virtual void Init(string jsonData)
    {

    }

    public virtual string GetJsonPath()
    {
        return "";
    }
}

public class HelperMgr
{

    public static HelperMgr _instance = null;

    public Dictionary<string, HelperBase> config = new Dictionary<string, HelperBase>();

    List<string> configKey;

    HelperMgr()
    {
        InputHelper();
    }

    public static HelperMgr Instance()
    {
        _instance ??= new HelperMgr();
        return _instance;
    }

    public T GetHelper<T>()
    {
        var tObj = typeof(T);
        var data = config[tObj.Name];
        return (T)(object)data;
    }

    public void InputHelper()
    {
        var _TestHelper = new TestHelper();
        var _EnemyHelper = new EnemyHelper();
        var _MonsterHelper = new MonsterHelper();
        config.Add(typeof(TestHelper).Name, _TestHelper);
        config.Add(typeof(EnemyHelper).Name, _EnemyHelper);
        config.Add(typeof(MonsterHelper).Name, _MonsterHelper);
        configKey = new List<string>(config.Keys);
    }


    public void ConfigHelper(Action finishCall)
    {

        foreach (var item in configKey)
        {
            var name = item;
            var path = config[name].GetJsonPath();

            path = path.Replace(".json", "");
            var jsonData = Resources.Load<TextAsset>(path);
            config[name].Init(jsonData.text);
        }

        finishCall.Invoke();

    }


}
