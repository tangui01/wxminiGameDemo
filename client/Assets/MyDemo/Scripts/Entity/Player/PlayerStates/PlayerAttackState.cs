using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    public class PlayerAttackState:IsState<Player>
    {
        private StateMachine<Player> _stateMachine;
        private Player _entity;

        StateMachine<Player> IsState<Player>.StateMachine => _stateMachine;

        Player IsState<Player>.Entity => _entity;
        
        public void Init(StateMachine<Player> stateMachine, Player entity)
        {
            _stateMachine = stateMachine;
            _entity = entity;
        }

        public void Enter()
        {
            _entity.Visual.AttackAni(_entity.CurrentAttackMode);
            
        }

        public void Execute()
        {
           
        }

        public void Exit()
        {
            
        }
    }
}

