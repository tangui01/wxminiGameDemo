using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyDemo
{
    /// <summary>
    /// 主角攻击方式
    /// </summary>
    public enum PlayerAttackMode
    {
        /// <summary>
        /// 近战
        /// </summary>
        JinZhan,
        /// <summary>
        /// 手枪
        /// </summary>
        ShouQiang,
        /// <summary>
        /// 机枪
        /// </summary>
        JiQiang
   
    }

    public class Player : Entity
    {
        #region States

        public StateMachine<Player> MStateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        
        public PlayerAttackState AttackState { get; private set; }

        private Camera mainCamera;

        #endregion

        public int attackValue;
        private PlayerLevelSys _playerLevelSys;

        public PlayerAttackMode CurrentAttackMode { get;private set; }
        
        public PlayerVisual Visual { get;private set; }

        public void Init(PlayerAttackMode  mode)
        {
            // //获取角色游戏数据
            // var data = PlayerData.GetGameData();
            // PlayerGameData gameData = data.GetData();
            // transform.position = data.GetPlayerPos();
            _playerLevelSys = GetComponent<PlayerLevelSys>();
            _playerLevelSys.Init();
            
            CurrentAttackMode=mode;
        }



        protected override void Awake()
        {
            base.Awake();
            Visual = GetComponent<PlayerVisual>();
            Visual.Init(this);
            MStateMachine = new StateMachine<Player>();
            IdleState = new PlayerIdleState();
            RunState = new PlayerRunState();
            AttackState = new PlayerAttackState();
            IdleState.Init(MStateMachine, this);
            MStateMachine.Init(IdleState);
            RunState.Init(MStateMachine, this);
            AttackState.Init(MStateMachine, this);
        }

        private void OnEnable()
        {
            EventManager.Register(GameEventKey.GameExit, SaveData);
            EventManager.Register(GameEventKey.WeaponAttack,Attack);
            EventManager.Register(GameEventKey.PlayerAttackAniComplete,Idle);
        }

        private void OnDisable()
        {
            EventManager.Unregister(GameEventKey.GameExit, SaveData);
            EventManager.Unregister(GameEventKey.WeaponAttack,Attack);
            EventManager.Unregister(GameEventKey.PlayerAttackAniComplete,Idle);
        }

        /// <summary>
        /// 切换攻击方式
        /// </summary>
        public void SwitchAttackMode(PlayerAttackMode targetMode)
        {
            if (CurrentAttackMode.Equals(targetMode)) return;
            CurrentAttackMode=targetMode;
        }
        private void Update()
        {
            MStateMachine.CurrentState.Execute();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Attack()
        {
            MStateMachine.ChangeState(AttackState);
        }

        private void Idle()
        {
            MStateMachine.ChangeState(IdleState);
        }

        private void SaveData()
        {
            var data = PlayerData.GetGameData();
            PlayerGameData gameData = data.GetData();
        }
        public void DoMove(Vector3 direction,Action callback=null)
        {
            transform.DOMove(direction, 0.25f).onComplete +=()=>callback?.Invoke();
        }
    }
}