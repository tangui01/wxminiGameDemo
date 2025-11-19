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
    private float _targetPosition;

    public void Init(StateMachine<Player> stateMachine, Player entity)
    {
        _stateMachine = stateMachine;
        _entity = entity;
    }

    public void Enter()
    {
        _entity.EntityVisual.RunningAni(true);
        _targetPosition = _entity.TargetPosition;
        SetDir();
    }

    public void Execute()
    {
        if (!Arrive())
        {
            Move();
        }
        else
        {
            _stateMachine.ChangeState(_entity.IdleState);
        }
    }

    private void Move()
    {
        if (_entity.SetDir.GetCurrentFaceDir()==FaceDirType.Right)
        {
            _entity.SetVelocity(Vector2.right*_entity.runSpeed);
        }
        else if (_entity.SetDir.GetCurrentFaceDir() == FaceDirType.Left)
        {
            _entity.SetVelocity(Vector2.left*_entity.runSpeed);
        }

    }

    private bool Arrive()
    {
        if (MathF.Abs(_entity.TargetPosition - _entity.transform.position.x) <0.1f)
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
        if (_entity.TargetPosition <= _entity.transform.position.x)
        {
            _entity.SetDir.SetFaceDir(FaceDirType.Left);
        }
        else if (_entity.TargetPosition > _entity.transform.position.x)
        {
            _entity.SetDir.SetFaceDir(FaceDirType.Right);
        }
    }

    public void Exit()
    {
        _entity.SetVelocity(Vector3.zero);
        _entity.EntityVisual.RunningAni(false);
    }
}
}
