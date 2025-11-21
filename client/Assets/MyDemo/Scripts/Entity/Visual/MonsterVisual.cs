using System;
using MyDemo;
using Spine;
using UnityEngine;
using Spine.Unity;
/// <summary>
/// 怪物动画表现
/// </summary>
public class MonsterVisual : MonoBehaviour
{
    private SkeletonAnimation _sa;
    private Monster _monster;
    
    private static readonly string BornAniName = "chuchang";
    private static readonly string IdleAniName = "daiji";
    private static readonly string Hit1AniName = "shouji1";
    private static readonly string Hit2AniName = "shouji2";
    private static readonly string Hit3AniName = "shouji3";
    private static readonly string DieAniName = "siwang";
    public void Init(Monster monster)
    {
        _monster = monster;
    }

    private void Awake()
    {
        _sa = transform.Find("Animator").GetComponent<SkeletonAnimation>();
        _sa.AnimationState.Complete += AddAniCompleteEvent;
    }

    private void OnDestroy()
    {
        _sa.AnimationState.Complete -= AddAniCompleteEvent;
    }

    public void IdleAni()
    {
        _sa.AnimationState.SetAnimation(0, IdleAniName, true);
    }

    public void BornAni()
    {
        _sa.AnimationState.SetAnimation(0, BornAniName, false);
    }
    public void HitAni()
    {
        //TODO:未完成不同的受击动画
        _sa.AnimationState.SetAnimation(0, Hit1AniName, false);
    }
    public void DieAni()
    {
        _sa.AnimationState.SetAnimation(0, DieAniName, false);
    }
    /// <summary>
    /// 添加动画完成事件
    /// </summary>
    private void AddAniCompleteEvent(TrackEntry trackEntry)
    {
        // 根据动画名称进行不同处理
        if (trackEntry.Animation.Name == BornAniName
            ||trackEntry.Animation.Name == Hit1AniName
            )
        {
            _monster.Idle();
        }
        else if (trackEntry.Animation.Name == DieAniName)
        {
            _monster.Die();
        }
    }
}
