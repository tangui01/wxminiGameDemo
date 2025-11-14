using DG.Tweening;
using sky_mirror;
using System;
using System.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class GlobalFunc
{
    public static GlobalFunc Instance{ get; private set; }

    public static Vector2 InputMousePositionToUGUI()
    {
        var canvas = GameObject.Find("Canvas");
        return canvas.transform.InverseTransformPoint(Input.mousePosition);
    }

    public static Vector2 PositionToUGUI(Transform objTransform)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(objTransform.position);

        var canvas = GameObject.Find("Canvas");
        return canvas.transform.InverseTransformPoint(screenPoint);
    }

    public static Vector2 PositionToUGUI(Vector3 objPos)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(objPos);

        var canvas = GameObject.Find("Canvas");
        return canvas.transform.InverseTransformPoint(screenPoint);
    }

    public static void UIFollowGameObj(GameObject follow, GameObject UI, Vector2 off)
    {
        if (UnityObjectUtility.IsUnityNull(follow))
        {
            return;
        }

        var pos = GlobalFunc.PositionToUGUI(follow.transform);

        var mathPos = pos + off;

        var hpBoxRect = UI.GetComponent<RectTransform>();

        RectTransform uiRoot = hpBoxRect.root.GetComponent<RectTransform>();

        float ScreenWidthHalf = uiRoot.sizeDelta.x / 2;
        float ScreenHeightHalf = uiRoot.sizeDelta.y / 2;
        if (mathPos.x > ScreenWidthHalf || mathPos.x < -ScreenWidthHalf ||
            mathPos.y > ScreenHeightHalf || mathPos.y < -ScreenHeightHalf)
        {
            UI.GetComponent<RectTransform>().anchoredPosition = mathPos;
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
            UI.GetComponent<RectTransform>().anchoredPosition = mathPos;
        }
    }

    //部分平台不会暂停
    public static void ADSPause()
    {
        Time.timeScale= 0;
        //CreatUIPage("UIPause");
    }

    public static void ADSResume()
    {
        Time.timeScale = 1;
        //BattleManager.Instance().FireEvent(Event.AdsResume, "");
    }

    public static void Log(string log)
    {
        Debug.Log(log);
    }

    public static void LoadTextureToImage(string texPath, Image image_)
    {
        Action<Texture2D> finish = (tex) =>
        {
            image_.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            image_.SetNativeSize();
        };

        PlatformMgr.Instance().LoadTexture(texPath, finish);
    }

    public static void LoadTextureToImage(string texPath, SpriteRenderer image_)
    {
        Action<Texture2D> finish = (tex) =>
        {
            image_.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        };

        PlatformMgr.Instance().LoadTexture(texPath, finish);
    }

    public static void LoadSpriteToImage(string texPath, Image image_)
    {
        Action<Sprite> finish = (spr) =>
        {
            image_.sprite = spr;
        };

        PlatformMgr.Instance().LoadSprite(texPath, finish);
    }

    public static void LoadSpriteToSpriteRenderer(string texPath, SpriteRenderer image_)
    {
        Action<Sprite> finish = (spr) =>
        {
            image_.sprite = spr;
        };

        PlatformMgr.Instance().LoadSprite(texPath, finish);
    }

    public static void LoadTextureToImage(string texPath, SpriteRenderer image_, Vector2 pivot)
    {
        Action<Texture2D> finish = (tex) =>
        {
            image_.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot);
            //image_.SetNativeSize();
        };

        PlatformMgr.Instance().LoadTexture(texPath, finish);
    }
        
    public static void ShowUIPanel(string name)
    {
        var Parent = GameObject.Find("Canvas").transform;
        //看是否已经弹了
        var IsHave = Parent.Find(name);
        if (IsHave)
        {
            return;
        }

        var key = "Assets/AddressResources/Prefabs/UI/Panel/" + name + ".prefab";

        Action<GameObject> callback = (obj) =>
        {
            var Obj = GameObject.Instantiate(obj, Parent);

            IsHave = Parent.Find(name);
            if (IsHave)
            {
                GameObject.Destroy(Obj);
                return;
            }

            var rect = Obj.GetComponent<RectTransform>();
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Obj.name = name;
        };

        PlatformMgr.Instance().LoadPrefab(key, callback);
    }

    public static void UICurrencyFlyToTarget(CurrencyEnum em, int cnt, Vector2 initPos)
    {
        var key = "Assets/AddressResources/Prefabs/UI/UIFlyCurrency.prefab";

        var saveEm = em;
        var saveIniPos = initPos;

        Action<GameObject> callback = (obj) =>
        {
            var Parent = GameObject.Find("Canvas");

            //Vector2 MathPos = Parent.transform.InverseTransformPoint(saveIniPos);

            var flyPrefab = obj;
            var flyObj = GameObject.Instantiate(flyPrefab, Parent.transform);

            flyObj.GetComponent<RectTransform>().anchoredPosition = initPos;

            var show = flyObj.GetComponent<Image>();

            var path = "Atlas/currency.spriteatlas" + "[" + (int)em + "]";
            LoadSpriteToImage(path, show);

            var uiFlyProp = flyObj.GetComponent<UIFlyCurrency>();

            var findPath = "MainUI/U/Currency"+ (int)em;
            GameObject flyTarget = Parent.transform.Find(findPath).gameObject;
            uiFlyProp.SetFlyTarget(flyTarget, em, cnt);

            var pos = initPos + new Vector2(UnityEngine.Random.Range(-5, 5) * 0.4f, UnityEngine.Random.Range(-5, 5) * 0.4f);
            uiFlyProp.FiyInitPos(pos);
        };

        PlatformMgr.Instance().LoadPrefab(key, callback);
    }


    public static void CreateAddCurrencyAnima(CurrencyEnum em, int cnt, Vector2 initPos)
    {
        int Max = 20;

        var step = cnt / Max;

        if (step <= 0)
        {
            //不够20个
            for (int i = 0; i < cnt; i++)
            {
                //创建UI效果
                GlobalFunc.UICurrencyFlyToTarget(em, 1, initPos);
            }
        }
        else
        {
            //先将可以整除的平均掉
            for (int i = 0; i < Max; i++)
            {
                //创建UI效果
                GlobalFunc.UICurrencyFlyToTarget(em, step, initPos);
            }

            //如果还有剩下的, 再加
            var zc_value = step * Max;

            var yushu = cnt - zc_value;

            if (yushu > 0)
            {

                //创建UI效果
                GlobalFunc.UICurrencyFlyToTarget(em, yushu, initPos);
            }
        }
    }
}
