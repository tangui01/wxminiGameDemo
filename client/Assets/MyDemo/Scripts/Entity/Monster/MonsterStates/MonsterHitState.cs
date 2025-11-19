using MyDemo;
using UnityEngine;

namespace MyDemo
{
    public class MonsterHitState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;

        private float _stateTimer;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _stateMachine = stateMachine;
            _entity = entity;
        }

        public void Enter()
        {
            _entity.EntityVisual.HitAni(true);
        }

        public void Execute()
        {
            _stateTimer+=Time.deltaTime;
            if (_stateTimer>=_entity.hitTime)
            {
                _stateMachine.ChangeState(_entity.IdleState);
            }
        }

        public void Exit()
        {
            _stateTimer = 0;
            _entity.EntityVisual.HitAni(false);
        }
    }
}

