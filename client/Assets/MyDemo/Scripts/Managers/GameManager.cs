

namespace MyDemo 
{ 
      /// <summary>
      /// 游戏管理器
      /// </summary>
public class GameManager : SingletonMonoBase<GameManager>
{
      public bool IsGameOver { get; private set; } = true;

      public void GameStart()
      {
            SceneLoadManger.Instance.LoadSceneAsync("GameApp", () =>
            {
                  IsGameOver = false;
                  PlayerManager.Instance.InitPlayer();
                  MonsterGenerateManager.Instance.Init();
            });
      }
      public void GameOver()
      {
            IsGameOver = true;
            MonsterGenerateManager.Instance.Clear();
            PoolManager.Instance.ClearPool();
            PlayerManager.Instance.GameOver();
            SceneLoadManger.Instance.LoadSceneAsync("Over", () =>
            {
                 
            });
      }
}
}
