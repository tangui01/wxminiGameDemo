using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIOverFightPanel : MonoBehaviour
{
    [SerializeField]
    TMP_Text cntShow;

    [SerializeField]
    GameObject winBg;

    [SerializeField]
    GameObject loseBg;

    int curCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        winBg.SetActive(false);
        loseBg.SetActive(false);

        curCnt = FightSceneAddin.Instance().GetFightAddBox().GetValue();
        cntShow.text = curCnt.ToString();

        if(FightSceneAddin.Instance().IsWin())
        {
            winBg.SetActive(true);
        }
        else
        {
            loseBg.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAward()
    {
        var _enum = CurrencyEnum.Jin;
        var cnt = (int)curCnt;

        var pos = cntShow.transform.position;

        GameObject.Destroy(gameObject);
        FightSceneAddin.Instance().FinishFight();

        GlobalFunc.CreateAddCurrencyAnima(_enum, cnt, pos);
    }

    public void AdsGetAward()
    {
        Action adsCall = () => {
            var _enum = CurrencyEnum.Jin;
            var cnt = (int)curCnt * 2;

            var pos = cntShow.transform.position;

            GameObject.Destroy(gameObject);
            FightSceneAddin.Instance().FinishFight();

            GlobalFunc.CreateAddCurrencyAnima(_enum, cnt, pos);
        };
        
        PlayerData.GetDataForId<AdsFinishData>(DataEnum.AdsFinishData).PlayAds(adsCall);
    }

}
