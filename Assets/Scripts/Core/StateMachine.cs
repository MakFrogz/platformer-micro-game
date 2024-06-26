using System;
using System.Collections.Generic;
using System.Data;

namespace Local.Features.StateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new Dictionary<Type, StateNode>();
        private HashSet<ITransaction> _anyTransactions = new HashSet<ITransaction>();

        public void Update()
        {
            ITransaction transaction = GetTransaction();
            if (transaction != null)
            {
                ChangeState(transaction.To);
            }
            _current.State?.Update();
        }

        private ITransaction GetTransaction()
        {
            foreach (ITransaction transaction in _anyTransactions)
            {
                if (transaction.Condition.Evaluate())
                {
                    return transaction;
                }
            }

            foreach (ITransaction transaction in _current.Transactions)
            {
                if (transaction.Condition.Evaluate())
                {
                    return transaction;
                }
            }

            return null;
        }

        private void ChangeState(IState state)
        {
            if (state == _current.State)
            {
                return;
            }

            IState previousState = _current.State;
            IState nextState = _nodes[state.GetType()].State;
            
            previousState.Exit();
            nextState.Enter();
            _current = _nodes[state.GetType()];
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State.Enter();
        }

        public void AddTransaction(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransaction(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransaction(IState to, IPredicate condition)
        {
            _anyTransactions.Add(new Transaction(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            if (_nodes.TryGetValue(state.GetType(), out StateNode node))
            {
                return node;
            }

            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);
            return node;
        }
        
        public class StateNode
        {
            public IState State { get; }
            public HashSet<ITransaction> Transactions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transactions = new HashSet<ITransaction>();
            }

            public void AddTransaction(IState to, IPredicate condition)
            {
                Transactions.Add(new Transaction(to, condition));
            }
        }
    }
}