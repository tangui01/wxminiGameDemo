using UnityEngine;

namespace MyDemo
{
    public class PlayerManager : SingletonMonoBase<PlayerManager>
    {
        [SerializeField] private Player player;

        [SerializeField, Header("主角攻击方式为枪时的出生位置")]
        private Vector3 playerGunSpawnPosition;

        [SerializeField, Header("主角攻击方式为近战时的出生位置")]
        private Vector3 playerMeleeSpawnPosition;

        [SerializeField, Header("主角的攻击方式")] private PlayerAttackMode currentAttackMode;
        public Player Player => player;

        public int ModeIndex = 0;

        public void InitPlayer()
        {
            PoolManager.Instance.FromPoolGetGameObject("Player", (obj) =>
            {
                obj.transform.position = currentAttackMode == PlayerAttackMode.JinZhan
                    ? playerMeleeSpawnPosition
                    : playerGunSpawnPosition;
                player = obj.GetComponent<Player>();
                player.Init(currentAttackMode);
                ModeIndex = (int)currentAttackMode;
                EventManager.Execute(GameEventKey.PlayerWeaponSwitch,currentAttackMode);
            });
        }

        public void GameOver()
        {
            player = null;
        }

        /// <summary>
        /// 切换攻击方式
        /// </summary>
        public void SwitchAttackModel()
        {
            ModeIndex++;
            if (ModeIndex >= 3)
            {
                ModeIndex = 0;
            }
            currentAttackMode = (PlayerAttackMode)ModeIndex;
            if (currentAttackMode is PlayerAttackMode.ShouQiang or PlayerAttackMode.JiQiang)
            {
                player.DoMove(playerGunSpawnPosition);
            }
            else if(currentAttackMode==PlayerAttackMode.JinZhan)
            {
                
                player.DoMove(playerMeleeSpawnPosition);
            }
            player.SwitchAttackMode(currentAttackMode);
            EventManager.Execute(GameEventKey.PlayerWeaponSwitch,currentAttackMode);
        }

        public PlayerAttackMode GetPlayerAttackMode()
        {
            return currentAttackMode;
        }
    }
}