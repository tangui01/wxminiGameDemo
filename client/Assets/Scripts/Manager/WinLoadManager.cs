
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WinLoadManager: MonoBehaviour
{
    public static WinLoadManager _instance = null;

    public static WinLoadManager Instance()
    {
        if (_instance == null)
        {
            var PlatformObj = GameObject.Find("PlatformAddin");
            _instance = PlatformObj.AddComponent<WinLoadManager>();
        }

        return _instance;
    }

    public void LoadTexture(string name, Action<Texture2D> callBack)
    {
        var rootPath = "";

#if UNITY_EDITOR
        rootPath = "AddressResources/Texture/";
#else
#endif

        rootPath += name;

        Action<UnityWebRequest> act = (www) =>
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            callBack(texture);
        };

        StartCoroutine(WebRequestTexture(rootPath, act));
    }

    public void LoadSprite(string name, Action<Sprite> callBack)
    {
        var rootPath = "";

#if UNITY_EDITOR
        rootPath = "AddressResources/Texture/";
#else
#endif

        rootPath += name;

        Action<UnityWebRequest> act = (www) =>
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            
            var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            callBack(sprite);
        };

        StartCoroutine(WebRequestTexture(rootPath, act));
    }

    public void LoadJson(string name, Action<string> callBack)
    {
        Action<UnityWebRequest> act = (www) =>
        {
            callBack(www.downloadHandler.text);
        };

        StartCoroutine(WebRequest(name, act));
    }

    public IEnumerator WebRequest(string name, Action<UnityWebRequest> callBack)
    {
        var path = GetDataPath() + name;

        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError ||
                www.result == UnityWebRequest.Result.ConnectionError)
        {
            GlobalFunc.Log("path:" + path + " No Find");
        }
        else
        {
            callBack(www);
        }
    }

    public IEnumerator WebRequestTexture(string name, Action<UnityWebRequest> callBack)
    {
        var path = GetDataPath() + name;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError ||
                www.result == UnityWebRequest.Result.ConnectionError)
        {
            GlobalFunc.Log("path:" + path + " No Find");
        }
        else
        {
            callBack(www);
        }
    }

    public void LoadPrefab(string name, Action<GameObject> callBack)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(name);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.IsDone)
                {
                    callBack(handle.Result);
                }
            }
        };
    }


    string GetDataPath()
    {
#if UNITY_EDITOR 
        return Application.dataPath + "/";
#elif UNITY_STANDALONE_WIN
        return Application.dataPath + "/../../Assets/";
#else
        return Application.dataPath + "/"; 
#endif


    }
}
