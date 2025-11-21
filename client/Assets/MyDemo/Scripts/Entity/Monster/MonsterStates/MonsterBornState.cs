using System.Collections;
using System.Collections.Generic;
using MyDemo;
using UnityEngine;

public class MonsterBornState : IsState<Monster>
{
    private StateMachine<Monster> _stateMachine;
    private Monster _entity;

    StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

    Monster IsState<Monster>.Entity => _entity;

    public void Init(StateMachine<Monster> stateMachine, Monster entity)
    {
        _stateMachine = stateMachine;
        _entity = entity;
    }

    public void Enter()
    {
        _entity.MonsterVisual.BornAni();
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
        
    }
}
