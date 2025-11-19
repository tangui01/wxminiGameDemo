using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyDemo;
using UnityEngine;

public class MonsterRunState : IsState<Monster>
{
    private StateMachine<Monster> _stateMachine;
    private Monster _entity;

    StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

    Monster IsState<Monster>.Entity => _entity;
    private Vector3 _moveDirection;

    public void Init(StateMachine<Monster> stateMachine, Monster entity)
    {
        _stateMachine = stateMachine;
        _entity = entity;
    }

    public void Enter()
    {
        _entity.EntityVisual.RunningAni(true);
        _entity.transform.DOMoveX(_entity.targetPositionX,_entity.moveTime).onComplete += () =>
        {
            _entity.StateMachine.ChangeState(_entity.IdleState);
        };
    }

    public void Execute()
    {
       
    }
    public void Exit()
    {
        _entity.EntityVisual.RunningAni(false);
    }
}
