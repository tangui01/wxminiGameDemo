using System;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 怪物管理器
    /// </summary>
    public class MonsterManager : SingletonMonoBase<MonsterManager>
    {
        private Monster _currentMonster; //当前怪物
        private MonsterData _currentMonsterData;
        [SerializeField]private Vector3 monsterSpawnPosition;
        private void Start()
        {
            LoadMonster();
        }

        private void OnEnable()
        {
            EventManager.Register(GameEventKey.MonsterDie,MonsterDie);
        }

        private void OnDisable()
        {
            EventManager.Unregister(GameEventKey.MonsterDie, MonsterDie);
        }

        /// <summary>
        /// 加载普通怪物
        /// </summary>
        private void LoadMonster()
        {
            int Levelid = LevelManager.Instance.GetCurrentLevelId();
            _currentMonsterData = HelperMgr.Instance().GetHelper<MonsterHelper>().GetMonster(Levelid);
            
            PoolManager.Instance.FromPoolGetGameObject(LevelManager.Instance.GetCurrentLevelGameData().minion,
                (monsterPrefab) =>
                {
                    _currentMonster = monsterPrefab.GetComponent<Monster>();
                    _currentMonster.Init(_currentMonsterData,monsterSpawnPosition);
                });
        }

        private void MonsterDie()
        {
            _currentMonster = null;
            LoadMonster();
        }
    }
}