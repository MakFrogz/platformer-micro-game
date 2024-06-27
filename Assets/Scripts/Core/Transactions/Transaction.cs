
using Core.Predicates;
using Core.States;

namespace Core.Transactions
{
    public class Transaction : ITransaction
    {
        public IState To { get; }
        public IPredicate Condition { get; }

        public Transaction(IState to, IPredicate condition)
        {
            To = to;
            Condition = condition;
        }
    }
}