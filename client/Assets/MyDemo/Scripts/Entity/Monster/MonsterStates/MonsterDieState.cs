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
              _entity.MonsterVisual.DieAni();
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
           
        }
    }
}

