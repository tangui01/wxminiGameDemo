using System;
using UnityEngine;
namespace MyDemo
{
    public class Player : Entity
    {
        #region States
        public StateMachine<Player> MStateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        
        private Camera mainCamera;
        #endregion

     

        public float TargetPosition { get; set; } //移动的目标位置

        public float runSpeed;

        public Gun Gun { get; private set; }
        
        private PlayerLevelSys playerLevelSys;

        

        public void Init()
        {
            //获取角色游戏数据
            var data = PlayerData.GetGameData();
            PlayerGameData gameData = data.GetData();
            transform.position = data.GetPlayerPos();

            SetDir.SetFaceDir(gameData.isFaceRight ? FaceDirType.Right : FaceDirType.Left);

            playerLevelSys = GetComponent<PlayerLevelSys>();
            playerLevelSys.Init();
         
        }

        protected override void Awake()
        {
            base.Awake();
            Gun = transform.Find("Gun").GetComponent<Gun>();
            Gun.Init(attackValue);
            MStateMachine = new StateMachine<Player>();
            IdleState = new PlayerIdleState();
            RunState = new PlayerRunState();
            IdleState.Init(MStateMachine, this);
            MStateMachine.Init(IdleState);
            RunState.Init(MStateMachine, this);
            mainCamera=Camera.main;
        }

        private void OnEnable()
        {
            EventManager.Register<Vector3>(GameEventKey.ScreenClick, Run);
            EventManager.Register<int>(GameEventKey.PlayerHit, Hit);
            EventManager.Register(GameEventKey.GameExit, SaveData);
        }

        private void OnDisable()
        {
            EventManager.Unregister<Vector3>(GameEventKey.ScreenClick, Run);
            EventManager.Unregister<int>(GameEventKey.PlayerHit, Hit);
            EventManager.Unregister(GameEventKey.GameExit, SaveData);
        }

        private void Update()
        {
            MStateMachine.CurrentState.Execute();
        }

        /// <summary>
        /// 移动函数
        /// </summary>
        private void Run(Vector3 targetPosition)
        {
            targetPosition.z = 10;
            TargetPosition = mainCamera.ScreenToWorldPoint(targetPosition).x;
            MStateMachine.ChangeState(RunState);
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

        private void SaveData()
        {
            var data = PlayerData.GetGameData();
            PlayerGameData gameData = data.GetData();
            data.SetPos(transform.position);
            data.SetDir(SetDir.GetCurrentFaceDir());
        }
    }
}