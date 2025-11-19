using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 关卡管理器
    /// </summary>
    public class LevelManager : SingletonMonoBase<LevelManager>
    {
        private LevelGameData _currentLevelData;
        private int _currentLevelId; //当前关卡

        public void Init()
        {
            _currentLevelId = PlayerData.GetGameData().GetData().currentLevel;
            GetLevelData(_currentLevelId);
        }

        private void GetLevelData(int levelid)
        {
            var le = HelperMgr.Instance().GetHelper<LevelGameDataHelper>();
            _currentLevelData = le.GetLevelGameData(levelid);
        }

        /// <summary>
        /// 关卡开始
        /// </summary>
        private void LevelStart()
        {
            
        }
        /// <summary>
        /// 关卡结束
        /// </summary>
        private void LevelEnd()
        {
            
        }

        public LevelGameData GetCurrentLevelGameData()
        {
            return _currentLevelData;
        }
    }
}