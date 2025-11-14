using DG.Tweening;
using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuItem : MonoBehaviour
{
    [SerializeField]
    UIMenuCtrl menuCtrl;

    [SerializeField]
    LockEnum _lockEnum;

    [SerializeField]
    Sprite normalBG;

    [SerializeField]
    Sprite chooseBG;

    [SerializeField]
    Sprite normalIcon;

    [SerializeField]
    Sprite chooseIcon;

    [SerializeField]
    Image bgShow;

    [SerializeField]
    Image icon;

    [SerializeField]
    GameObject txt;

    bool _isChoose = false;

    bool _isLock = false;

    float srcY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        srcY = icon.GetComponent<RectTransform>().anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetConfig(bool isChoose, float width, float height)
    {
        _isChoose = isChoose;

        if (_isChoose)
        {
            bgShow.sprite = chooseBG;
            
            icon.sprite = chooseIcon;
            icon.SetNativeSize();

            txt.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            txt.SetActive(true);

            icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60);
        }
        else
        {
            bgShow.sprite = normalBG;
            
            icon.sprite = normalIcon;
            icon.SetNativeSize();

            txt.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            txt.SetActive(false);

            icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
        }


        GetComponent<RectTransform>().DOSizeDelta(new Vector2(width, height), 0.2f);
    }

    public void LockItem()
    {
        _isLock = true;

        icon.gameObject.SetActive(false);
    }

    public void ClickItem()
    {
        if(_isLock)
        {
            GetComponent<CheckLock>().ShowLockTips();
        }
        else
        {
            menuCtrl.ClickItem((int)_lockEnum);
        }
    }
}
