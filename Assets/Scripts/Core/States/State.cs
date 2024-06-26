namespace Local.Features.StateMachine
{
    public abstract class State : IState
    {
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void Exit() {}
    }
}