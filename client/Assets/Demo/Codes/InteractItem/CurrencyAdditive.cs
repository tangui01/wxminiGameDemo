using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyAdditive : InteractItem
{
    public CurrencyEnum currency;
    float addCD = 1f;
    float timer = 0;
    int addCount = 10;
    void Update()
    {
        if (!CanInteract) return;
        timer += Time.deltaTime;
        if(timer >=addCD)
        {
            timer = 0;
            PlayerData.GetCurrency().AddValue(currency, addCount,false);
            EventManager.Instance().EventTrigger(sky_mirror.SM_EventType.CurrencyChange, currency.ToString());
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        //在玩家离开区域内才保存，防止在更新中不停保存消耗性能
        PlayerData.GetCurrency().SaveMyData();
        timer = 0;
    }
}
