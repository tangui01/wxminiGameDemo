using sky_mirror;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public struct TestItem
{
    public int index;
    public string value;
    public int int_value;
}

[Serializable]
public struct TestRoot
{
    public TestItem[] root;
}

public class TestHelper: HelperBase
{
    TestRoot config = new TestRoot();

    public TestHelper()
    {
        
    }

    public override void Init(string jsonData)
    {
        config = JsonUtility.FromJson<TestRoot>(jsonData);

        GlobalFunc.Log(typeof(TestHelper) + " Init OK");
    }

    public override string GetJsonPath()
    {
        return "Config/test_cf.json";
    }

    public string GetValue(int id)
    {
        var item = config.root[id - 1];
        return item.value;
    }

    public int GetIntValue(int id)
    {
        var item = config.root[id - 1];
        return item.int_value;
    }
}
