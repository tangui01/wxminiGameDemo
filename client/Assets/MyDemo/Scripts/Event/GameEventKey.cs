/// <summary>
/// 游戏触发事件
/// </summary>
public class GameEventKey
{
    #region GameInput 
    public const string ScreenClick = "ScreenClick";//屏幕点击事件 人物的移动控制
    

    #endregion

    #region Gun
    public const string GunShoot = "GunShoot";
    public const string GunRotate = "GunRotate";

    #endregion

    #region Player

    public const string PlayerHit = "PlayerHit";
    public const string PlayerDeath = "PlayerDeath";
    public const string PlayerLevelUp = "PlayerLevelUp";//人物升级
    public const string PlayerInitLevel = "PlayerInitLevel";//人物初始化等级
    public const string PlayerExpAdd = "PlayerExpAdd";//人物经验值增加
    public const string PlayerAddHp = "PlayerLevelUpExp";
    public const string PlayerLevelUpExp = "PlayerLevelUpExp";
    
    public const string PlayerLevelVisual= "PlayerLevelVisual";//人物等级UI显示
    public const string PlayerHpVisual = "PlayerHpVisual";//人物HP 显示
    public const string PlayerExpVisual = "PlayerExpVisual";//人物经验值显示

    #endregion

    #region GameData

    public const string GameExit = "GameExit";//游戏退出时

    #endregion
}
