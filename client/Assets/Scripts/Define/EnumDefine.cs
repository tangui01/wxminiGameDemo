using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sky_mirror
{



    public enum PropEnum
    {
        Currency,//����
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

        //ˮ������սDemo
        CurrencyChange
    }

    public enum CurrencyEnum
    {
        Null,
        Gutou,
        Jin,//����1
        Zhuan,//��ʯ
        GreenDiamond,//����ʯ
        SilverCoin,//����
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
        Battle,//ս��ģ��
        GameData,
    }

    public enum AITreeEnum
    {
        Mutou,//ľͷAI
        ATK,//����AI
    }

    public enum ATKBoxEnum
    {
        Dan,//����ָ����ɫ
        AOE,//��Χ
    }

    public enum FightSceneState
    {
        Ready,//׼����
        Start,//��ʼ
        Over,//����
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