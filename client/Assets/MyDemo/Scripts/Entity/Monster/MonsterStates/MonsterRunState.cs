using UnityEngine;
namespace MyDemo
{
    public class MonsterRunState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;

        private Player _player;

        private MonsterData monsterData;
        private float _sqrThreshold;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _entity = entity;
            _stateMachine = stateMachine;
            _player = PlayerManager.Instance.Player;
            monsterData = entity.MonsterData;
            _sqrThreshold = monsterData.attackDis * monsterData.attackDis;
        }

        public void Enter()
        {
            _entity.EntityVisual.RunningAni(true);
        }

        public void Execute()
        {
            if (!Arrive())
            {
                Vector3 direction = _player.transform.position - _entity.transform.position;
                _entity.Move(_player.transform.position, direction.normalized,1);
            }
            //到达主角附近攻击
            else
            {
                _entity.StateMachine.ChangeState(_entity.MonsterAttackState);
            }
        }

        //是否到达主角附近
        private bool Arrive()
        {
            // 使用平方距离避免开方运算
            Vector3 offset = _player.transform.position - _entity.transform.position;
            if (offset.sqrMagnitude <= _sqrThreshold)
            {
                return true;
            }

            return false;
        }
        public void Exit()
        {
            _entity.SetVelocity(Vector3.zero);
            _entity.EntityVisual.RunningAni(false);
        }
    }

}