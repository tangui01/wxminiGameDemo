using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
    public TMP_Text txtCount;
    public Image imgIcon;
    public CurrencyEnum currencyType;

    private void Awake()
    {
        //GlobalFunc.LoadSpriteToImage(currencyType.ToString(), imgIcon);
        EventManager.Instance().AddEventListener(SM_EventType.CurrencyChange,Refresh);
        Refresh(currencyType.ToString());
    }

    public void Refresh(string currencyName,string arg2 = null,string arg3 = null)
    {
        var currency = PlayerData.GetCurrency();
        if (currency == null || currencyType.ToString() != currencyName) return;
        txtCount.text = currency.GetValue(currencyType).ToString();
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener(SM_EventType.CurrencyChange,Refresh);
    }
}
