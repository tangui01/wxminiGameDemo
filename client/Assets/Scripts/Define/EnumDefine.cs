using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sky_mirror
{



    public enum PropEnum
    {
        Currency,//货币
    }

    public enum Camp
    {
        My,
        Target
    }


    public enum SM_EventType
    {
        RoleDead,
        RoleDamage,
        RemoveHPBox,
        FightStart,
        FightOver,
        FlushCurrency,
        FightPauseClickOver,

        //水果岛大战Demo
        CurrencyChange
    }

    public enum CurrencyEnum
    {
        Null,
        Gutou,
        Jin,//货币1
        Zhuan,//钻石
        GreenDiamond,//绿钻石
        SilverCoin,//银币
        Max

    }
    public enum DataEnum
    {
        Bass,
        Currency,
        MapLevel,
        RoleLv,
        Server,
        Setup,
        Rank,
        Chengjiu,
        Award,
        RoleChoose,
        Shop,
        Fuhuo,
        AdsData,
        AdsFinishData,
        Cebianlan,
    }

    public enum AITreeEnum
    {
        Mutou,//木头AI
        ATK,//进攻AI
    }

    public enum ATKBoxEnum
    {
        Dan,//单体指定角色
        AOE,//范围
    }

    public enum FightSceneState
    {
        Ready,//准备好
        Start,//开始
        Over,//结束
    }

    public enum LockEnum
    {
        None,
        Fuben,
        Tishen,
        Fight,
        Kapai,
        Shop,
    }
}