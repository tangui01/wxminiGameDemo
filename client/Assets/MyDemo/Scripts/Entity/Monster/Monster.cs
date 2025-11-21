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
       
        
        [SerializeField] protected float maxHP;
        protected float currentHealth;
        
        
        private MonsterData _monsterData;

        public MonsterVisual MonsterVisual { get; set; }
        private bool IsDie;

        protected override void Awake()
        {
            base.Awake();
            MonsterVisual = GetComponent<MonsterVisual>();
            MonsterVisual.Init(this);
        }

        public void Init(MonsterData data,Vector3 initialPosition)
        {
            transform.position = initialPosition;
            currentHealth=data.maxHp;
            _monsterData = data;
            IsDie = false;
            EventManager.Register<int>(GameEventKey.MonsterHit,Hit);
            Born();
        }

        private void Born()
        {
            MonsterVisual.BornAni();
        }

        public void Idle()
        {
            MonsterVisual.IdleAni();
        }

        private void Hit(int damage)
        {
            if(IsDie) return;
            currentHealth-=damage;
            if (currentHealth <= 0)
            {
                IsDie=true;
                MonsterVisual.DieAni();
            }
            else
            {
                MonsterVisual.HitAni();
            }
        }
        public void Die()
        {
            PoolManager.Instance.EnterPool(_monsterData.monsterName, gameObject);
            EventManager.Unregister<int>(GameEventKey.MonsterHit,Hit);
            EventManager.Execute(GameEventKey.PlayerExpAdd,_monsterData.expValue);
            EventManager.Execute(GameEventKey.MonsterDie);
        }

    }
}