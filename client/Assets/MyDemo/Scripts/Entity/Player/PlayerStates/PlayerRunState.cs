using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDemo 
{ 
// <summary>
/// 人物行走状态
/// </summary>
public class PlayerRunState : IsState<Player>
{
    private StateMachine<Player> _stateMachine;
    private Player _entity;

    StateMachine<Player> IsState<Player>.StateMachine => _stateMachine;

    Player IsState<Player>.Entity => _entity;
    private Vector3 _targetPosition;
    private Vector3 _moveDir; //移动方向
    private float _arrivalThreshold = 0.1f;
    private float _sqrThreshold;

    public void Init(StateMachine<Player> stateMachine, Player entity)
    {
        _stateMachine = stateMachine;
        _entity = entity;
        _sqrThreshold = _arrivalThreshold * _arrivalThreshold;
    }

    public void Enter()
    {
        _targetPosition = _entity.TargetPosition;
        _moveDir = (_targetPosition - _entity.transform.position).normalized;
        _entity.EntityVisual.RunningAni(true);
        SetDir();
    }

    public void Execute()
    {
        if (!Arrive())
        {
            _entity.SetVelocity( _moveDir * _entity.PlayerDataConfig.runSpeed);
        }
        else
        {
            _entity.MStateMachine.ChangeState(_entity.IdleState);
        }
    }

    //是否到达目的地
    private bool Arrive()
    {
        // 使用平方距离避免开方运算
        Vector3 offset = _targetPosition - _entity.transform.position;
        if (offset.sqrMagnitude <= _sqrThreshold)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 根据点击的位置设置人物的朝向
    /// </summary>
    private void SetDir()
    {
        if (_entity.TargetPosition.x <= _entity.transform.position.x)
        {
            _entity.SetDir.SetFaceDir(FaceDirType.Left);
        }
        else if (_entity.TargetPosition.x >= _entity.transform.position.x)
        {
            _entity.SetDir.SetFaceDir(FaceDirType.Right);
        }
    }

    public void Exit()
    {
        _entity.SetVelocity( Vector3.zero);
        _entity.EntityVisual.RunningAni(false);
    }
}
}
