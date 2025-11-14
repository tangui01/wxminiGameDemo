using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class AppConst
{
    public const string Bullet = "Bullet";
    public const string HpState = "HpState";
    public const string RedTeam = "RedTeam";
    public const string BlueTeam = "BlueTeam";
}
public class PoolMgr : MonoBehaviour
{
    public static PoolMgr Instance;
    public Dictionary<string,Pool> pools = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else if(Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        pools.Add(AppConst.Bullet, new Pool(AppConst.Bullet, transform));
        pools.Add(AppConst.RedTeam,new Pool(AppConst.RedTeam, transform));
        pools.Add(AppConst.HpState, new Pool(AppConst.HpState, transform));
        PreLoadPoolRes();
    }

    private void PreLoadPoolRes()
    {
        foreach(var pool in pools.Values)
        {
            Action<GameObject> callback = (obj) => {
                pool.pref = obj;
                Debug.Log(pool.poolName + "-----" + pool.pref.name);
            };
            PlatformMgr.Instance().LoadPrefab(pool.resName, callback);
        }
    }
    /// <summary>
    /// 获取之后需要自己激活物体
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject Get(string poolName,Transform parent)
    {
        if(pools.TryGetValue(poolName,out Pool pool))
        {
            var result = pool.Get();
            result.transform.SetParent(parent);
            return result;
        }
        return null;
    }

    public void Push(string poolName,GameObject obj)
    {
        if (pools.TryGetValue(poolName, out Pool pool))
        {
            pool.Push(obj);
        }
    }

    public void Clear()
    {
        foreach(var pool in pools.Values)
        {
            pool.Clear();
        }
        var items = transform.GetComponentsInChildren<Transform>();
        for(int i = 1;i < items.Length;i++)
        {
            Destroy(items[i].gameObject);
        }
    }
}
