using System;
using DG.Tweening;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 怪物类
    /// </summary>
    public class Monster : Entity
    {
        public float targetPositionX;
        public float moveTime;
        public float hitTime;//受伤时间
        public float dieTime;//死亡时间
        public MonsterIdleState IdleState { get; set; }
        public MonsterRunState RunState { get; set; }
        public MonsterHitState HitState { get; set; }
        public MonsterDieState DieState { get; set; }
        public StateMachine<Monster> StateMachine { get; set; }
        
        private MonsterData _monsterData;

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine<Monster>();
            IdleState = new MonsterIdleState();
            IdleState.Init(StateMachine, this);
            RunState = new MonsterRunState();
            RunState.Init(StateMachine, this);
            HitState = new MonsterHitState();
            HitState.Init(StateMachine, this);
            DieState = new MonsterDieState();
            DieState.Init(StateMachine, this);
        }

        public void Init(MonsterData data,Vector3 initialPosition)
        {
            transform.position = initialPosition;
            currentHealth=data.maxHp;
            _monsterData = data;
            StateMachine.Init(RunState);
            EventManager.Register<int>(GameEventKey.MonsterHit,Hit);
        }

        private void Hit(int damage)
        {
            currentHealth-=damage;
            if (currentHealth <= 0)
            {
                StateMachine.ChangeState(DieState);
            }
            else
            {
                StateMachine.ChangeState(HitState);
            }
        }

        private void Update()
        {
            StateMachine.CurrentState?.Execute();
        }

        public void Die()
        {
            PoolManager.Instance.EnterPool(_monsterData.monsterName, gameObject);
            EventManager.Unregister<int>(GameEventKey.MonsterHit,Hit);
            EventManager.Execute(GameEventKey.PlayerExpAdd,_monsterData.expValue);
        }

    }
}