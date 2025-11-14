using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MyDemo
{
    /// <summary>
    /// 场景加载
    /// </summary>
    public class SceneLoadManger : SingletonMonoBase<SceneLoadManger>
    {
        private UnityAction cb;
        public void LoadSceneAsync(string sceneName,UnityAction callback=null)
        {
            var a = SceneManager.LoadSceneAsync(sceneName);
            float progress = 0f;
            cb += () =>
            {
                progress=a.progress;
                if (progress >= 0.99f)
                {
                    callback?.Invoke();
                    cb=null;
                }
            };
        }

        private void Update()
        {
            cb?.Invoke();
        }
    }
}

