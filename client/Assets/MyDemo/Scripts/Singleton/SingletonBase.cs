using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例基类(不继承Mono)
/// </summary>
public class SingletonBase<T> where T : SingletonBase<T>,new()
{
      private static T instance;

      public static T Instance
      {
            get
            {
                  if (instance == null)
                  {
                        instance = new T();
                  }
                  return instance;
            }
      }
}
