using Core.Predicates;
using Core.States;

namespace Core.Transactions
{
    public interface ITransaction
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}