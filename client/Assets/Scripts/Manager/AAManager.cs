using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class AAManager
{
    public static AAManager _instance = null;
    private Dictionary<string, AsyncOperationHandle> _handelCacheDic = new Dictionary<string, AsyncOperationHandle>();

    public static AAManager Instance()
    {
        if(_instance == null)
        {
            _instance = new AAManager();
        }

        return _instance;
    }

    void SaveHandleCache(string name, AsyncOperationHandle handle)
    {
        if (!_handelCacheDic.ContainsKey(name))
        {
            _handelCacheDic.Add(name, handle);
        }
    }

    public void LoadTexture(string name, Action<Texture2D> callBack)
    {
        var rootName = "Assets/AddressResources/Texture/" + name;

        //先看看有没有
        if (_handelCacheDic.ContainsKey(rootName))
        {
            var obj = _handelCacheDic[rootName];
            if (obj.IsDone)
            {
                AsyncOperationHandle<Texture2D> TextureHandel = obj.Convert<Texture2D>();
                callBack(TextureHandel.Result);
                return;
            }
        }

        AsyncOperationHandle<Texture2D> handle = Addressables.LoadAssetAsync<Texture2D>(rootName);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.IsDone)
                {
                    SaveHandleCache(rootName, handle);
                    callBack(handle.Result);
                }
            }
        };
    }

    public void LoadSprite(string name, Action<Sprite> callBack)
    {
        var rootName = "Assets/AddressResources/Texture/" + name;

        //先看看有没有
        if (_handelCacheDic.ContainsKey(rootName))
        {
            var obj = _handelCacheDic[rootName];
            if (obj.IsDone)
            {
                AsyncOperationHandle<Sprite> TextureHandel = obj.Convert<Sprite>();
                callBack(TextureHandel.Result);
                return;
            }
        }

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(rootName);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.IsDone)
                {
                    SaveHandleCache(rootName, handle);
                    callBack(handle.Result);
                }
            }
        };
    }

    public void LoadJson(string name, Action<string> callBack)
    {
        //先看看有没有
        if (_handelCacheDic.ContainsKey(name))
        {
            var obj = _handelCacheDic[name];
            if (obj.IsDone)
            {
                AsyncOperationHandle<TextAsset> TextAssetHandel = obj.Convert<TextAsset>();
                callBack(TextAssetHandel.Result.text);
                return;
            }
        }

        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(name);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.IsDone)
                {
                    SaveHandleCache(name, handle);
                    callBack(handle.Result.text);
                }
            }
        };
    }

    public void LoadPrefab(string name, Action<GameObject> callBack)
    {
        //先看看有没有
        if (_handelCacheDic.ContainsKey(name))
        {
            var obj = _handelCacheDic[name];
            if (obj.IsDone)
            {
                AsyncOperationHandle<GameObject> TextAssetHandel = obj.Convert<GameObject>();
                callBack(TextAssetHandel.Result);
                return;
            }
        }

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(name);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.IsDone)
                {
                    SaveHandleCache(name, handle);
                    callBack(handle.Result);
                }
            }
        };
    }

    public void Clear()
    {
        ReleaseHandelCache();
    }

    public void ReleaseHandelCache()
    {
        foreach (var item in _handelCacheDic)
        {
            Addressables.Release(item.Value);
        }
        _handelCacheDic.Clear();
    }
}
