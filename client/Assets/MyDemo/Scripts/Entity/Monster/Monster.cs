using UnityEngine;
namespace MyDemo
{
    /// <summary>
    /// 怪物类
    /// </summary>
    public class Monster : Entity
    {
        #region States

        public MonsterIdleState MonsterIdleState { get; private set; }
        public MonsterRunState MonsterRunState { get; private set; }
        public MonsterHitState MonsterHitState { get; private set; }
        public MonsterDieState MonsterDieState { get; private set; }
        public MonsterAttackState MonsterAttackState { get; private set; }
        public StateMachine<Monster> StateMachine { get; private set; }

        #endregion

        public MonsterData MonsterData { get; private set; }
        public void Init(MonsterData monsterData)
        {
            EntityVisual.Initialize();
            StateMachine.Init(MonsterRunState);
            SetDir.Initialize();
            MonsterData = monsterData;
            SetFaceDir(PlayerManager.Instance.Player.GetPosition());
            maxHP =monsterData.maxHp;
            attackValue = monsterData.attackValue;
            currentHealth = maxHP;
        }
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine<Monster>();
            MonsterIdleState = new MonsterIdleState();
            MonsterIdleState.Init(StateMachine, this);

            MonsterRunState = new MonsterRunState();
            MonsterRunState.Init(StateMachine, this);
            MonsterHitState = new MonsterHitState();
            MonsterHitState.Init(StateMachine, this);
            MonsterAttackState = new MonsterAttackState();
            MonsterAttackState.Init(StateMachine, this);
            MonsterDieState = new MonsterDieState();
            MonsterDieState.Init(StateMachine, this);
        }
        private void Update()
        {
            StateMachine.CurrentState.Execute();
        }

        public void Move(Vector3 target, Vector3 dir, float speed)
        {
            SetFaceDir(target);
            Rb.velocity = dir * speed;
        }

        private void SetFaceDir(Vector3 target)
        {
            if (target.x > transform.position.x)
            {
                SetDir.SetFaceDir(FaceDirType.Right);
            }
            else if (target.x < transform.position.x)
            {
                SetDir.SetFaceDir(FaceDirType.Left);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Bullet bullet = other.GetComponent<Bullet>();

                EntityVisual.HitAni();
                Hit(bullet.GetAttackValue());
            }
        }

        private void Hit(float damage)
        {
            if (GetCurrentHealth() <= 0)
            {
                StateMachine.ChangeState(MonsterDieState);
            }
            else
            {
                Damage(damage);
            }
        }

        public void Die()
        {
            EventManager.Execute(GameEventKey.PlayerExpAdd,MonsterData.expValue);
            PoolManager.Instance.EnterPool("Monster", gameObject);
        }
    }
}