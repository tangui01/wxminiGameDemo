/// <summary>
/// 游戏触发事件
/// </summary>
public class GameEventKey
{

    #region Gun
    public const string GunShoot = "GunShoot";

    #endregion

    #region Player

    public const string PlayerHit = "PlayerHit";
    public const string PlayerExpAdd = "PlayerExpAdd";//人物经验值增加
    
    public const string PlayerLevelVisual= "PlayerLevelVisual";//人物等级UI显示
    public const string PlayerHpVisual = "PlayerHpVisual";//人物HP 显示
    public const string PlayerExpVisual = "PlayerExpVisual";//人物经验值显示

    #endregion

    #region GameData

    public const string GameExit = "GameExit";//游戏退出时

    #endregion

    #region Monster
    public const string MonsterHit = "MonsterHit";
    public const string MonsterDie= "MonsterDie";

    #endregion
}
