using System;
using System.Collections;
using System.Collections.Generic;
using MyDemo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 攻击模式切换面板
/// </summary>
public class AttackModelSwitchPanel : MonoBehaviour
{
    private Button  _switchBtn;
    private Text _tips;
    private void Awake()
    {
        _switchBtn=transform.Find("SwitchBtn").GetComponent<Button>();
        _switchBtn.onClick.AddListener(OnSwitchBtnClick); 
        _tips=transform.Find("Tips").GetComponent<Text>();
       
    }

    private void OnEnable()
    {
        MyDemo.EventManager.Register<PlayerAttackMode>(GameEventKey.PlayerWeaponSwitch,SetTips);
    }

    private void OnDisable()
    {
        MyDemo.EventManager.Unregister<PlayerAttackMode>(GameEventKey.PlayerWeaponSwitch,SetTips);
    }

    private void OnSwitchBtnClick()
    {
        PlayerManager.Instance.SwitchAttackModel();
    }

    private void SetTips(PlayerAttackMode mode)
    {
        int target =(int)mode+1;
        if (target>=3)
        {
            target = 0;
        }
        switch ((PlayerAttackMode)target)
        {
            case PlayerAttackMode.JinZhan:
                _tips.text = "<size=40>切换武器</size><color=red>近战</color>";
                break;
            case PlayerAttackMode.ShouQiang:
                _tips.text = "<size=40>切换武器</size><color=green>手枪</color>";
                break;
            case PlayerAttackMode.JiQiang:
                _tips.text = "<size=40>切换武器</size><color=yellow>机枪</color>";
                break;
        }
        
    }
}
