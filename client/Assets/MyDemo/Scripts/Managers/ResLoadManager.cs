using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyDemo
{
    /// <summary>
    /// 资源加载管理器
    /// </summary>
    public class ResLoadManager :SingletonMonoBase<ResLoadManager>
    {
        private AsyncOperationHandle<GameObject> assetHandle;
        private GameObject loadedAsset;
        public void LoadAsset(string assetKey) 
        {
             
        }
        private void OnAssetLoaded(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAsset = handle.Result;
                Instantiate(loadedAsset); // 实例化使用
            }
            else
            {
                Debug.LogError($"Failed to load asset: {handle.OperationException}");
            }
        }

        private void Instantiate(string poolName)
        {
            
        }
    }

}

