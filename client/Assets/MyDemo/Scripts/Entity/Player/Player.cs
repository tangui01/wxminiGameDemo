using System;
using UnityEngine;
namespace MyDemo
{
    /// <summary>
    /// 主角战斗模式
    /// </summary>
    public enum PlayerBattleMode
    {
        /// <summary>
        /// 近战
        /// </summary>
        Melee,
        /// <summary>
        /// 远程
        /// </summary>
        Gun
    }

    public class Player : Entity
    {
        #region States
        public StateMachine<Player> MStateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        
        private Camera mainCamera;
        #endregion

        [SerializeField] protected int attackValue;

        public float TargetPosition { get; set; } //移动的目标位置
        

        public Gun Gun { get; private set; }
        
        private PlayerLevelSys playerLevelSys;

        

        public void Init()
        {
            //获取角色游戏数据
            var data = PlayerData.GetGameData();
            PlayerGameData gameData = data.GetData();
            transform.position = data.GetPlayerPos();
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
        }

        private void OnEnable()
        {
            EventManager.Register<int>(GameEventKey.PlayerHit, Hit);
            EventManager.Register(GameEventKey.GameExit, SaveData);
        }

        private void OnDisable()
        {
            EventManager.Unregister<int>(GameEventKey.PlayerHit, Hit);
            EventManager.Unregister(GameEventKey.GameExit, SaveData);
        }

        private void Update()
        {
            MStateMachine.CurrentState.Execute();
        }


        private void Hit(int damage)
        {
            Damage(damage);
            if (GetCurrentHealth() > 0)
            {
                EntityVisual.HitAni(true);
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
        }
    }
}