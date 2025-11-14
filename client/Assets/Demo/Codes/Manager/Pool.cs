using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool
{
    public string resName;
    public string poolName;
    public GameObject pref;
    public List<GameObject> pool;
    private Transform parent;

    public Pool(string resName,Transform parent)
    {
        this.resName = resName;
        this.poolName = resName + "_Pool";
        this.parent = parent;
        pool = new List<GameObject>();
    }
    public GameObject Get()
    {
        //没有就创建
        if(pool.Count == 0)
        {
            return GameObject.Instantiate<GameObject>(pref,null);
        }
        //有就从池子里拿，并移除
        var result = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);
        return result;
    }
    public void Push(GameObject obj)
    {
        if (obj == null) return;
        obj.transform.SetParent(parent);
        obj.SetActive(false);
        pool.Add(obj);
    }
    public void Clear()
    {
        pool.Clear();
    }

}
