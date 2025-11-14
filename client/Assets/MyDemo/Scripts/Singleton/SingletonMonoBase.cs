using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 单例基类(继承自Mono)
    /// </summary>
    public class SingletonMonoBase<T>:MonoBehaviour   where T : SingletonMonoBase<T>,new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    }
}