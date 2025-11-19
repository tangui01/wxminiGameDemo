using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物体动画表现
/// </summary>
public class EntityVisual : MonoBehaviour
{
    private static readonly int IdleAniName = Animator.StringToHash("Idle");
    private static readonly int RunAniName = Animator.StringToHash("Run");
    private static readonly int AttackAniName = Animator.StringToHash("Attack"); 
    private static readonly int DieAniName = Animator.StringToHash("Die"); 
    private static readonly int HitAniName = Animator.StringToHash("Hit");
    private Animator  _animator;
    private SpriteRenderer _sr;
    
    
    public void Init()
    {
        _animator = transform.Find("Animator").GetComponent<Animator>();
        _sr= transform.Find("Animator").GetComponent<SpriteRenderer>();
    }

    public void IdleAni(bool isPlay)
    {
        _animator.SetBool(IdleAniName,isPlay);
    }

    public void RunningAni(bool isPlay)
    {
        _animator.SetBool(RunAniName,isPlay);
    }
    public void AttackAni(bool isPlay)
    {
        _animator.SetBool(AttackAniName,isPlay);
    }
    public void HitAni(bool isPlay)
    {
        _animator.SetBool(HitAniName,isPlay);
    }
    public void Initialize()
    {
        _sr.color = Color.white;
    }

    public void DeadAni(bool isPlay)
    {
        _animator.SetBool(DieAniName,isPlay);
    }
    
}
