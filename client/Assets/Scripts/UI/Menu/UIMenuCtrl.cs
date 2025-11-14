using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMenuCtrl : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] clickEvent;

    [SerializeField]
    UnityEvent[] exitClickEvent;

    [SerializeField]
    UIMenuItem[] ItemObj;

    [SerializeField]
    float ClickWidthScale = 1.3f;

    [SerializeField]
    float ClickHeightScale = 1.1f;

    [SerializeField]
    int initIndex = 2;

    [SerializeField]
    float stepWidth = 0.0f;

    [SerializeField]
    float stepHeight = 0.0f;

    int _curIndex = -1;

    float SrcWidth = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        SrcWidth = GetComponent<RectTransform>().sizeDelta.x;

        ClickItem(initIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickItem(int Index)
    {
        if(_curIndex != -1)
        {
            exitClickEvent[_curIndex].Invoke();
        }

        _curIndex = Index-1;

        var resetWidth = (SrcWidth - stepWidth * ClickWidthScale) / 4;

        for (int i = 0; i < ItemObj.Length; i++)
        {
            var curItem = ItemObj[i];

            if (_curIndex == i)
            {
                curItem.ResetConfig(true, stepWidth * ClickWidthScale, stepHeight * ClickHeightScale);
            }
            else
            {
                curItem.ResetConfig(false, resetWidth, stepHeight);
            }
        }

        clickEvent[_curIndex].Invoke();
    }
}
