namespace Local.Features.StateMachine
{
    public interface ITransaction
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}