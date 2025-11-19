using System.Collections;
using System.Collections.Generic;
using MyDemo;
using UnityEngine;

namespace  MyDemo
{
    public class MonsterDieState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;
        private float stateTimer;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _stateMachine = stateMachine;
            _entity = entity;
        }

        public void Enter()
        {
           _entity.EntityVisual.DeadAni(true);
        }

        public void Execute()
        {
            stateTimer+=Time.deltaTime;
            if (stateTimer>=_entity.dieTime)
            {
                stateTimer = 0;
                _stateMachine.ChangeState(_entity.IdleState);
            }
        }

        public void Exit()
        {
            _entity.Die();
            _entity.EntityVisual.DeadAni(false);
            EventManager.Execute(GameEventKey.MonsterDie);
        }
    }
}

