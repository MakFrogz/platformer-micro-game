using Audio;
using Core.Predicates;
using Core.States;
using Mechanics.Player;
using Model;
using Platformer.Mechanics;
using SaveLoad;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UserInput;
using Utils;
using VContainer;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private AssetReference _congratulationsPrefab;
        [SerializeField][Min(0)] private float _respawnTime;
        [SerializeField][Min(0)] private float _congratulationsTime;
        
        private StateMachine _stateMachine;

        private IDirectionReader _joystick;
        private PlayerController _playerController;
        private PlayerModel _playerModel;
        private ITokenController _tokenController;
        private IGameMenuModel _gameMenuModel;
        private IAudioService _audioService;
        private ISaveLoadService _saveLoadService;

        private CountdownTimer _respawnTimer;
        private CountdownTimer _congratulationsTimer;
        private CountdownTimer[] _timers;
        
        [Inject]
        private void Construct(IDirectionReader joystick, PlayerController playerController, PlayerModel playerModel, ITokenController tokenController, IGameMenuModel gameMenuModel, IAudioService audioService, ISaveLoadService saveLoadService)
        {
            _joystick = joystick;
            _playerController = playerController;
            _playerModel = playerModel;
            _tokenController = tokenController;
            _gameMenuModel = gameMenuModel;
            _audioService = audioService;
            _saveLoadService = saveLoadService;
        }
        
        private void Awake()
        {
            _stateMachine = new StateMachine();
        }

        private void Start()
        {
            InitializeTimers();
            InitializeStateMachine();
        }

        private void InitializeTimers()
        {
            _respawnTimer = new CountdownTimer(_respawnTime);
            _congratulationsTimer = new CountdownTimer(_congratulationsTime);
            _timers = new [] { _respawnTimer, _congratulationsTimer };
        }

        private void InitializeStateMachine()
        {
            IState setupState = new SetupState(_playerModel, _gameMenuModel, _saveLoadService, _audioService);
            IState resetState = new ResetState(_playerModel, _tokenController);
            IState gameplayState = new GameplayState(_joystick, _playerController, _audioService);
            IState deadState = new DeadState(_virtualCamera, _playerController, _respawnTimer);
            IState teleportState = new TeleportState(_virtualCamera, _playerController, _spawnPoint);
            IState congratulationsState = new CongratulationsState(_congratulationsTimer, _congratulationsPrefab, _tokenController);
            IState pauseState = new PauseState(_gameMenuModel, _audioService);
            IState exitState = new ExitState(_playerModel, _gameMenuModel, _saveLoadService);

            _stateMachine.AddTransaction(setupState, gameplayState, new FuncPredicate(() => true));
            _stateMachine.AddTransaction(gameplayState, deadState, new FuncPredicate(() => !_playerModel.IsAlive));
            _stateMachine.AddTransaction(gameplayState, congratulationsState, new FuncPredicate(() => _tokenController.IsCongratulationsTokenCount));
            _stateMachine.AddTransaction(congratulationsState, gameplayState, new FuncPredicate(() => _congratulationsTimer.IsFinished));
            _stateMachine.AddTransaction(deadState, teleportState, new FuncPredicate(() => _respawnTimer.IsFinished));
            _stateMachine.AddTransaction(teleportState, resetState, new FuncPredicate(() => true));
            _stateMachine.AddTransaction(resetState, gameplayState, new FuncPredicate(() => true));
            _stateMachine.AddTransaction(gameplayState, pauseState, new FuncPredicate(() => _gameMenuModel.IsPause));
            _stateMachine.AddTransaction(pauseState, gameplayState, new FuncPredicate(() => !_gameMenuModel.IsPause));
            _stateMachine.AddTransaction(pauseState, exitState, new FuncPredicate(() => _gameMenuModel.IsExit));

            _stateMachine.SetState(setupState);
        }

        private void Update()
        {
            _stateMachine.Update();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            foreach (CountdownTimer timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
    }
}