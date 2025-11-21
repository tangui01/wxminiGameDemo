using System;
using System.Collections;
using System.Collections.Generic;
using MyDemo;
using Spine;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// 玩家物体动画表现
/// </summary>
public class PlayerVisual : MonoBehaviour
{
    private SkeletonAnimation _sa;

    //主角待机动画名称
    private const string JiZhanIdleName = "daiji-jinzhan";
    private const string ShouQiangIdleName = "daiji-shouqiang";

    private const string JiQiangIdleName = "daiji-jiqiang";

    //主角攻击动画名称
    private const string JiZhanAttack1AniName = "gongji-jinzhan1";
    private const string JiZhanAttack2AniName = "gongji-jinzhan2";
    private const string JiZhanAttack3AniName = "gongji-jinzhan3";
    private const string JiQiangAttack1AniName = "gongji-jiqiang1";
    private const string ShouQiangAttack1AniName = "gongji-shouqiang1";


    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _sa = transform.Find("Animator").GetComponent<SkeletonAnimation>();
        SetAniMin();
    }

    /// <summary>
    /// 添加动画完成事件
    /// </summary>
    private void AddAniCompleteEvent(TrackEntry trackEntry)
    {
        // 根据动画名称进行不同处理
        if (trackEntry.Animation.Name == JiZhanAttack1AniName
            || trackEntry.Animation.Name == JiZhanAttack2AniName
            || trackEntry.Animation.Name == JiZhanAttack3AniName
            || trackEntry.Animation.Name == JiQiangAttack1AniName
            || trackEntry.Animation.Name == ShouQiangAttack1AniName
           )
        {
            MyDemo.EventManager.Execute(GameEventKey.PlayerAttackAniComplete);
        }
    }

    /// <summary>
    /// 添加动画关键帧事件
    /// </summary>
    /// <param name="trackEntry"></param>
    /// <param name="e"></param>
    private void AddAniEvent(TrackEntry trackEntry, Spine.Event e)
    {
        string eventName = e.Data.Name; 
        switch (eventName)
        {
            case "fire":
                 switch (trackEntry.Animation.Name)
                 {
                     case JiZhanAttack1AniName or JiZhanAttack2AniName or JiZhanAttack3AniName:
                         MyDemo.EventManager.Execute(GameEventKey.MonsterHit,_player.attackValue);
                         break;
                     case JiQiangAttack1AniName:
                         //发射子弹
                         
                         break;
                     case ShouQiangAttack1AniName:
                         //发射子弹
                         
                         break;
                 }
                break;
        }
    }

    /// <summary>
    /// 设置动画的过渡时间
    /// </summary>
    private void SetAniMin()
    {
        _sa.AnimationState.Data.SetMix(JiZhanAttack1AniName, JiZhanIdleName, 0.1f);
        _sa.AnimationState.Data.SetMix(JiZhanAttack2AniName, JiZhanIdleName, 0.1f);
        _sa.AnimationState.Data.SetMix(JiZhanAttack3AniName, JiZhanIdleName, 0.1f);

        _sa.AnimationState.Data.SetMix(JiQiangAttack1AniName, JiQiangIdleName, 0.05f);
        _sa.AnimationState.Data.SetMix(JiQiangIdleName, JiQiangAttack1AniName, 0.05f);

        _sa.AnimationState.Data.SetMix(ShouQiangIdleName, ShouQiangAttack1AniName, 0.1f);
        _sa.AnimationState.Data.SetMix(ShouQiangAttack1AniName, ShouQiangIdleName, 0.1f);
    }

    private void OnEnable()
    {
        MyDemo.EventManager.Register<PlayerAttackMode>(GameEventKey.PlayerWeaponSwitch, SwitchAni);
        _sa.AnimationState.Complete += AddAniCompleteEvent;
        _sa.AnimationState.Event += AddAniEvent;
    }

    private void OnDisable()
    {
        MyDemo.EventManager.Register<PlayerAttackMode>(GameEventKey.PlayerWeaponSwitch, SwitchAni);
        _sa.AnimationState.Complete -= AddAniCompleteEvent;
        _sa.AnimationState.Event -= AddAniEvent;
    }

    public void IdleAni(PlayerAttackMode mode)
    {
        switch (mode)
        {
            case PlayerAttackMode.JinZhan:
                _sa.AnimationState.SetAnimation(0, JiZhanIdleName, true);
                break;
            case PlayerAttackMode.ShouQiang:
                _sa.AnimationState.SetAnimation(0, ShouQiangIdleName, true);
                break;
            case PlayerAttackMode.JiQiang:
                _sa.AnimationState.SetAnimation(0, JiQiangIdleName, true);
                break;
        }
    }

    public void RunningAni(PlayerAttackMode mode)
    {
        _sa.AnimationState.SetAnimation(0, "run", true);
    }

    public void AttackAni(PlayerAttackMode mode)
    {
        switch (mode)
        {
            case PlayerAttackMode.JinZhan:
                _jiZhan++;
                if (_jiZhan >= 3)
                {
                    _jiZhan = 0;
                }

                PlayAni(GetJinZhan());
                break;
            case PlayerAttackMode.ShouQiang:
                PlayAni(ShouQiangAttack1AniName);
                break;
            case PlayerAttackMode.JiQiang:
                PlayAni(JiQiangAttack1AniName);
                break;
        }
    }

    private void PlayAni(string animationName, bool loop = false)
    {
        _sa.AnimationState.SetAnimation(0, animationName, loop);
    }

    private int _jiZhan = 0;

    /// <summary>
    /// 获取近战的动画
    /// </summary>
    private string GetJinZhan()
    {
        switch (_jiZhan)
        {
            case 0:
                return JiZhanAttack1AniName;
            case 1:
                return JiZhanAttack2AniName;
            case 2:
                return JiZhanAttack3AniName;
        }

        return "";
    }
    /// <summary>
    /// 根据人物不同状态切换动画
    /// </summary>
    private void SwitchAni(PlayerAttackMode mode)
    {
        if (!_player) return;
        switch (_player.MStateMachine.CurrentState)
        {
            case PlayerIdleState:
                IdleAni(mode);
                break;
            case PlayerRunState:
                RunningAni(mode);
                break;
            case PlayerAttackState:
                AttackAni(mode);
                break;
        }
    }
}