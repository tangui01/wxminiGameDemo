/// <summary>
/// 游戏触发事件
/// </summary>
public class GameEventKey
{

    #region 武器事件
    public const string WeaponAttack = "WeaponAttack";

    #endregion

    #region Player
    public const string PlayerAttackAniComplete = "PlayerAttackAniComplete";//主角攻击动画完成
    
    
    
    public const string PlayerExpAdd = "PlayerExpAdd";//人物经验值增加
    public const string PlayerWeaponSwitch = "PlayerWeaponSwitch";//角色武器切换
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
