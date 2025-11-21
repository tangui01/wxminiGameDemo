using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    public class MonsterIdleState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _entity=entity;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
           _entity.MonsterVisual.IdleAni();
        }

        public void Execute()
        {
           
        }

        public void Exit()
        {
        
        }
    }
}

