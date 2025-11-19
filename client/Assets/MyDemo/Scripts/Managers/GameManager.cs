using System;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager : SingletonMonoBase<GameManager>
    {
        public bool IsGameOver { get; private set; } = true;

        private void Start()
        {
            GameStart();
        }

        public void GameStart()
        {
            IsGameOver = false;
            PlayerManager.Instance.InitPlayer();
            LevelManager.Instance.Init();
        }

        public void GameOver()
        {
            IsGameOver = true;
            PoolManager.Instance.ClearPool();
            PlayerManager.Instance.GameOver();
            SceneLoadManger.Instance.LoadSceneAsync("Over", () => { });
        }

        /// <summary>
        /// 游戏退出时
        /// </summary>
        private void OnApplicationQuit()
        {
            EventManager.Execute(GameEventKey.GameExit);
        }
    }
}