using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePool
{
    public int expectedMaxSize = 10;
    public GameObject prefab;
}


namespace MyDemo
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class PoolManager : SingletonMonoBase<PoolManager>
    {
        private Dictionary<string, Queue<GameObject>> poolDict;

        private Transform _poolRoot;

        //队列的预先容量
        private readonly int _expectedMaxSize = 100;

        public PoolManager()
        {
            poolDict = new Dictionary<string, Queue<GameObject>>();
        }
        /// <summary>
        /// 进入对象池
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="poolObject"></param>
        public void EnterPool(string poolName, GameObject poolObject)
        {
            //判断是否有对象池根节点
            if (_poolRoot == null)
            {
                //从场景里找到根节点
                _poolRoot = transform.Find("PoolRoot");
                if (!_poolRoot) //如果没有 就创建一个
                {
                    _poolRoot = new GameObject("PoolRoot").GetComponent<Transform>();
                }
            }

            //判断根节点下是否有对应的目录
            Transform child = _poolRoot.Find(poolName);
            if (child == null)
            {
                GameObject obj = new GameObject(poolName);
                obj.transform.SetParent(_poolRoot);
            }

            //判断字典里是否已经注册
            if (poolDict.TryGetValue(poolName, out Queue<GameObject> pool))
            {
                pool.Enqueue(poolObject);
            }
            else
            {
                poolDict.Add(poolName, new Queue<GameObject>(_expectedMaxSize));
            }

            poolObject.SetActive(false);
            poolObject.transform.position = Vector3.one * 1000;
            poolObject.transform.SetParent(child);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="poolObject"></param>
        public GameObject FromPoolGetGameObject(string poolName, GameObject poolObject)
        {
            GameObject obj = null;
            //判断池中是否有这个键值
            if (poolDict.TryGetValue(poolName, out Queue<GameObject> pool))
            {
                //判断池中是否有这个存货
                if (pool.Count > 0)
                {
                    obj = pool.Dequeue();
                }
                else
                {
                    //如果没有就实例化一个给它
                    obj = Instantiate(poolObject);
                }
            }
            else
            {
                obj = Instantiate(poolObject);
                poolDict.Add(poolName, new Queue<GameObject>(_expectedMaxSize));
            }

            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }

        public void ClearPool()
        {
            poolDict.Clear();
        }
    }
}