# platformer-micro-game

I used DI, DOTween, State Machine pattern, and MVP for UI for this test.

# Documentations
- [VContainer](https://vcontainer.hadashikick.jp/) - DI (Dependency Injection) for Unity Game Engine
- [DOTween](https://dotween.demigiant.com/) - animation engine for Unity

# Code
# Entry point
RootLifetimeScope is the main lifetime scope of main services and models for the application
```C#
public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .Register<PlayerModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterBuildCallback(Setup);
        }
        
        private void Setup(IObjectResolver resolver)
        {
            IPlayerModel playerModel = resolver.Resolve<IPlayerModel>();
            ISaveLoadService saveLoadService = resolver.Resolve<ISaveLoadService>();
            
            PlayerData playerData = saveLoadService.Load(KeyConstants.PLAYER_DATA, new PlayerData(1,0,0));
            
            playerModel.SetHealth(playerData.Health);
            playerModel.SetTokens(playerData.Tokens);
            playerModel.SetDistance(playerData.Distance);
        }
    }
```

# Scene lifetime scopes
- LobbyLifetimeScope is lifetime scope for lobby scene
```C#
public class LobbyLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerStatsView);
            
            builder
                .Register<PlayerStatsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
```
- GameLifetimeScope is lifetime scope for game scene
```C#
public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private GameMenuView _gameMenuView;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        [SerializeField] private BoostHandler _boostHandler;
        [SerializeField] private TokenController _tokenController;
        [SerializeField] private AudioMixer _audioMixer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerStatsView);
            builder.RegisterComponent(_gameMenuView);
            
            builder
                .RegisterComponent(_joystick)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_player)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_boostHandler)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_tokenController)
                .AsImplementedInterfaces();

            builder
                .Register<PlayerStatsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<GameMenuModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<GameMenuPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<AudioService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter(_audioMixer);
        }
    }
```
# State Machine
State Machine which can create StateNode, change StateNode depending on conditions, do transactions to any state
```C#
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
```

# State
Base state interface which gives to implement it in concrete or abstract state class
```C#
public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }

public abstract class State : IState
    {
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void Exit() {}
    }
```
# Transaction
Base transaction interface which gives to implement it in concrete or abstract transaction for transaction between states
```C#
public interface ITransaction
    {
        IState To { get; }
        IPredicate Condition { get; }
    }

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
```

# Predicate
Base predicate(condition) interface which gives to us implement it in concrete or abstract predicate class for states conditions
```C#
public interface IPredicate
    {
        bool Evaluate();
    }

public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate() => _func.Invoke();
    }
```
