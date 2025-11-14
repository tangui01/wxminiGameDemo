using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum LOAD_MODEL
{
    //windows, 直接加载
    WIN,
    //AAManger,通过AA加载
    AA,
}

public class PlatformMgr
{

    //平台设置
    private LOAD_MODEL _curLoadModel = LOAD_MODEL.AA;

    public static PlatformMgr _instance = null;

    public static PlatformMgr Instance()
    {
        if(_instance == null)
        {
            _instance = new PlatformMgr();
        }

        return _instance;
    }

    public void InitPlatform(Action configCall)
    {
        switch (_curLoadModel)
        {
            case LOAD_MODEL.WIN:
                {
                    WinLoadManager.Instance();
                }
                break;
            case LOAD_MODEL.AA:
                {
                    AAManager.Instance();
                }
                break;
            default:
                break;
        }

        //初始化数据系统
        PlayerData.Instance();
        //初始化配置表系统
        HelperMgr.Instance().ConfigHelper(configCall);
    }

    public void LoadTexture(string name, Action<Texture2D> callBack)
    {
        switch (_curLoadModel)
        {
            case LOAD_MODEL.WIN:
                {
                    WinLoadManager.Instance().LoadTexture(name, callBack);
                }
                break;
            case LOAD_MODEL.AA:
                {
                    AAManager.Instance().LoadTexture(name, callBack);
                }
                break;
            default:
                break;
        }
    }

    public void LoadSprite(string name, Action<Sprite> callBack)
    {
        switch (_curLoadModel)
        {
            case LOAD_MODEL.WIN:
                {
                    WinLoadManager.Instance().LoadSprite(name, callBack);
                }
                break;
            case LOAD_MODEL.AA:
                {
                    AAManager.Instance().LoadSprite(name, callBack);
                }
                break;
            default:
                break;
        }
    }

    public void LoadJson(string name, Action<string> callBack)
    {
        switch (_curLoadModel)
        {
            case LOAD_MODEL.WIN:
                {
                    WinLoadManager.Instance().LoadJson(name, callBack);
                }
                break;
            case LOAD_MODEL.AA:
                {
                    AAManager.Instance().LoadJson(name, callBack);
                }
                break;
            default:
                break;
        }
    }

    public void LoadPrefab(string name, Action<GameObject> callBack)
    {
        switch (_curLoadModel)
        {
            case LOAD_MODEL.WIN:
                {
                    WinLoadManager.Instance().LoadPrefab(name, callBack);
                }
                break;
            case LOAD_MODEL.AA:
                {
                    AAManager.Instance().LoadPrefab(name, callBack);
                }
                break;
            default:
                break;
        }
    }

    public void PlayAds(Action call)
    {
        call.Invoke();
    }
}
