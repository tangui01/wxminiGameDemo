namespace MyDemo 
{ 
// <summary>
/// 人物行走状态
// <summary>
public class PlayerRunState : IsState<Player>
{
    private StateMachine<Player> _stateMachine;
    private Player _entity;

    StateMachine<Player> IsState<Player>.StateMachine => _stateMachine;

    Player IsState<Player>.Entity => _entity;
    public void Init(StateMachine<Player> stateMachine, Player entity)
    {
        _stateMachine = stateMachine;
        _entity = entity;
    }

    public void Enter()
    {
       
    }

    public void Execute()
    {
        
    }
    public void Exit()
    {
      
    }
}
}
