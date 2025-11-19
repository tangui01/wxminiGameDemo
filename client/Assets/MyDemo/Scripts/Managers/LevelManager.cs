using UnityEngine;
using UnityEngine.Serialization;

namespace MyDemo
{
    /// <summary>
    /// 关卡管理器
    /// </summary>
    public class LevelManager : SingletonMonoBase<LevelManager>
    {
        private LevelGameData _currentLevelData;
        [SerializeField]private int currentLevelId; //当前关卡

        public void Init()
        {
            currentLevelId = PlayerData.GetGameData().GetData().currentLevel;
            SetLevelData(currentLevelId);
        }

        private void SetLevelData(int levelid)
        {
            var le = HelperMgr.Instance().GetHelper<LevelGameDataHelper>();
            _currentLevelData = le.GetLevelGameData(levelid);
        }

        public LevelGameData GetCurrentLevelGameData()
        {
            return _currentLevelData;
        }

        public int GetCurrentLevelId()
        {
            return currentLevelId;
        }
    }
}