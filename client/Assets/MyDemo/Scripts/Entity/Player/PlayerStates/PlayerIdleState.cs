namespace MyDemo
{
    public class PlayerIdleState : IsState<Player>
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

        public virtual void Enter()
        {
           _entity.Visual.IdleAni(_entity.CurrentAttackMode);
        }
        public void Execute()
        {

        }
        public void Exit()
        {
           
        }
    }
}

