using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物类idle状态
/// </summary>
namespace MyDemo
{ 
public class MonsterIdleState : IsState<Monster>
{
    private StateMachine<Monster> _stateMachine;
    private Monster _entity;

    StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

    Monster IsState<Monster>.Entity => _entity;
    private Player _player;

    public void Init(StateMachine<Monster> stateMachine, Monster entity)
    {
        _entity=entity;
        _stateMachine=stateMachine;
    }

    public void Enter()
    {
        _entity.EntityVisual.IdleAni(true);
    }

    public void Execute()
    {
       
    }
    public void Exit()
    {
        _entity.EntityVisual.IdleAni(false);
    }
}
}