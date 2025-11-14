using UnityEngine;
namespace MyDemo
{
    public class Player : Entity
    {
        #region States

        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }

        private Camera mainCamera;

        #endregion

        public StateMachine<Player> MStateMachine { get; private set; }

        public Vector3 TargetPosition { get; private set; } //移动的目标位置


        public PlayerDataConfig PlayerDataConfig { get; private set; }

        public Gun Gun { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            mainCamera = Camera.main;
            PlayerDataConfig = GetComponent<PlayerDataConfig>();
            MStateMachine = new StateMachine<Player>();
            IdleState = new PlayerIdleState();
            RunState = new PlayerRunState();
            IdleState.Init(MStateMachine, this);
            MStateMachine.Init(IdleState);
            RunState.Init(MStateMachine, this);
        }

        public void Init()
        {
            Gun = transform.Find("Gun").GetComponent<Gun>();
            Gun.Init(attackValue);
        }

        private void OnEnable()
        {
            EventManager.Register<Vector3>(GameEventKey.ScreenClick, Run);
            EventManager.Register<Vector3>(GameEventKey.GunShoot, Shoot);
            EventManager.Register<int>(GameEventKey.PlayerHit, Hit);
        }

        private void OnDisable()
        {
            EventManager.Unregister<Vector3>(GameEventKey.ScreenClick, Run);
            EventManager.Unregister<Vector3>(GameEventKey.GunShoot, Shoot);
            EventManager.Unregister<int>(GameEventKey.PlayerHit, Hit);
        }

        private void Update()
        {
            MStateMachine.CurrentState.Execute();
        }

        /// <summary>
        /// 移动函数
        /// </summary>
        /// <param name="clickScreenPosition">屏幕点击位置</param>
        private void Run(Vector3 clickScreenPosition)
        {
            Vector3 clickWorldPosition = clickScreenPosition;
            clickWorldPosition.z = 10;
            TargetPosition = mainCamera.ScreenToWorldPoint(clickWorldPosition);
            MStateMachine.ChangeState(RunState);
        }

        /// <summary>
        /// 射击时要朝向怪物那一边
        /// </summary>
        /// <param name="targetPosition"></param>
        private void Shoot(Vector3 targetPosition)
        {
            if (targetPosition.x > transform.position.x)
            {
                SetDir.SetFaceDir(FaceDirType.Right);
            }
            else if (targetPosition.x < transform.position.x)
            {
                SetDir.SetFaceDir(FaceDirType.Left);
            }
        }

        private void Hit(int damage)
        {
            Damage(damage);
            if (GetCurrentHealth() > 0)
            {
                EntityVisual.HitAni();
            }
            else
            {
                //角色死亡
                GameManager.Instance.GameOver();
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

    }
}