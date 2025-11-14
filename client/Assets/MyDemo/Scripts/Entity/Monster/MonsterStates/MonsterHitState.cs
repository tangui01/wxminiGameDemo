namespace MyDemo
{
    public class MonsterHitState : IsState<Monster>
    {
        private StateMachine<Monster> _stateMachine;
        private Monster _entity;

        StateMachine<Monster> IsState<Monster>.StateMachine => _stateMachine;

        Monster IsState<Monster>.Entity => _entity;

        public void Init(StateMachine<Monster> stateMachine, Monster entity)
        {
            _stateMachine = stateMachine;
            _entity = entity;
        }

        public void Enter()
        {
            _entity.EntityVisual.HitAni();
        }
        public void Execute()
        {

        }
        public void Exit()
        {

        }
    }
}