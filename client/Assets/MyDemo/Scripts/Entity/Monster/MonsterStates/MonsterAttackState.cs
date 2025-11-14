using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDemo 
{
    public class MonsterAttackState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;
        private Player _player;
        private MonsterData monsterData;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _stateMachine = stateMachine;
            _entity = entity;
            monsterData = entity.MonsterData;
            _player = PlayerManager.Instance.Player;
        }

        public void Enter()
        {
            _entity.EntityVisual.AttackAni(true);
        }

        public void Execute()
        {
            //检查主角是否在附近
            if (!IsAttackPlayer())
            {
                _stateMachine.ChangeState(_entity.MonsterRunState);
            }
        }

        /// <summary>
        /// 是否攻击到主角
        /// </summary>
        private bool IsAttackPlayer()
        {
            return Vector3.Distance(_player.transform.position, _entity.transform.position) <= monsterData.attackDis;
        }

        public void Exit()
        {
            _entity.EntityVisual.AttackAni(false);
        }
    }
}
